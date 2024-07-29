using AutoMapper;
using MediatR;
using Papara.Base;
using Papara.Base.Response;
using Papara.Bussiness.Cqrs;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.AccountSchema;

namespace Papara.Bussiness.Query;

public class AccountQueryHandler : 
    IRequestHandler<GetAllAccountQuery,ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetAccountByIdQuery,ApiResponse<AccountResponse>>,
    IRequestHandler<GetAccountByCustomerIdQuery, ApiResponse<List<AccountResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionContext sessionContext;

    public AccountQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,ISessionContext sessionContext)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionContext = sessionContext;
    }
    
    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
    {
        List<Account> entityList = await unitOfWork.AccountRepository.GetAll("Customer");
        var mappedList = mapper.Map<List<AccountResponse>>(entityList);
        return new ApiResponse<List<AccountResponse>>(mappedList);
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.AccountRepository.GetById(request.AccountId,"Customer");
        var mapped = mapper.Map<AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAccountByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        List<Account> entityList = await unitOfWork.AccountRepository.Where(x=> x.CustomerId == sessionContext.Session.CustomerId, "Customer");
        var mappedList = mapper.Map<List<AccountResponse>>(entityList);
        return new ApiResponse<List<AccountResponse>>(mappedList);
    }
}