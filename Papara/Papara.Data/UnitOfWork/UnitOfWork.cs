using Papara.Data.Context;
using Papara.Data.Domain;
using Papara.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly PaparaDbContext dbContext;

		public IGenericRepository<Customer> CustomerRepository { get; }
		public IGenericRepository<CustomerDetail> CustomerDetailRepository { get; }
		public IGenericRepository<CustomerAddress> CustomerAddressRepository { get; }
		public IGenericRepository<CustomerPhone> CustomerPhoneRepository { get; }

		public IGenericRepository<User> UserRepository { get; }
		public IGenericRepository<Country> CountryRepository { get; }
		public IGenericRepository<Account> AccountRepository { get; }
		public IGenericRepository<AccountTransaction> AccountTransactionRepository { get; }



		public UnitOfWork(PaparaDbContext dbContext)
		{
			this.dbContext = dbContext;

			CustomerRepository = new GenericRepository<Customer>(this.dbContext);
			CustomerDetailRepository = new GenericRepository<CustomerDetail>(this.dbContext);
			CustomerAddressRepository = new GenericRepository<CustomerAddress>(this.dbContext);
			CustomerPhoneRepository = new GenericRepository<CustomerPhone>(this.dbContext);
			UserRepository = new GenericRepository<User>(this.dbContext);
			CountryRepository = new GenericRepository<Country>(this.dbContext);
			AccountRepository = new GenericRepository<Account>(this.dbContext);
			AccountTransactionRepository = new GenericRepository<AccountTransaction>(this.dbContext);
		}

		public void Dispose()
		{
		}

		public async Task Complete()
		{
			await dbContext.SaveChangesAsync();
		}

		public async Task CompleteWithTransaction()
		{
			using (var dbTransaction = await dbContext.Database.BeginTransactionAsync())
			{
				try
				{
					await dbContext.SaveChangesAsync();
					await dbTransaction.CommitAsync();
				}
				catch (Exception ex)
				{
					await dbTransaction.RollbackAsync();
					Console.WriteLine(ex);
					throw;
				}
			}
		}
	}
}
