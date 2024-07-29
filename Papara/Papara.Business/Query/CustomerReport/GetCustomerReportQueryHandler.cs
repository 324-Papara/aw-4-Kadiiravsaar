using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Papara.Base.Response;
using Papara.Data.Domain;


namespace Papara.Business.Query.CustomerReport
{
    public record GetCustomerReportQuery() : IRequest<ApiResponse<List<Customer>>>;

    public class GetCustomerReportQueryHandler : IRequestHandler<GetCustomerReportQuery, ApiResponse<List<Customer>>>
    {
        private readonly IConfiguration _configuration;

        public GetCustomerReportQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<Customer>>> Handle(GetCustomerReportQuery request, CancellationToken cancellationToken)
        {
            var connectionString = _configuration.GetConnectionString("MsSqlConnection");

            using var connection = new SqlConnection(connectionString);
            var sql = @"
            SELECT * FROM dbo.Customers c
            LEFT JOIN dbo.CustomerDetails cd ON c.Id = cd.CustomerId
            LEFT JOIN dbo.CustomerAddresses ca ON c.Id = ca.CustomerId
            LEFT JOIN dbo.CustomerPhones cp ON c.Id = cp.CustomerId
			WHERE c.IsActive = 1";

            var customerDictionary = new Dictionary<long, Customer>();

            var customers = await connection.QueryAsync<Customer, CustomerDetail, CustomerAddress, CustomerPhone, Customer>(
                sql,
                (customer, detail, address, phone) =>
                {
                    if (!customerDictionary.TryGetValue(customer.Id, out var currentCustomer))
                    {
                        currentCustomer = customer;
                        currentCustomer.CustomerDetail = detail; // Detail nesnesi her zaman tek olacağı için direkt atama yapılabilir
                        currentCustomer.CustomerAddresses = new List<CustomerAddress>();
                        currentCustomer.CustomerPhones = new List<CustomerPhone>();
                        customerDictionary.Add(customer.Id, currentCustomer);
                    }

                    if (address != null && !currentCustomer.CustomerAddresses.Any(a => a.Id == address.Id))
                    {
                        currentCustomer.CustomerAddresses.Add(address);
                    }

                    if (phone != null && !currentCustomer.CustomerPhones.Any(p => p.Id == phone.Id))
                    {
                        currentCustomer.CustomerPhones.Add(phone);
                    }

                    return currentCustomer;
                },
                splitOn: "Id,Id,Id,Id"
            );

            return new ApiResponse<List<Customer>>(customers.Distinct().ToList());
        }
    }
}
