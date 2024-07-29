using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Papara.Base.Entity;
using Papara.Data.Context;
using System.Linq.Expressions;


namespace Papara.Data.GenericRepository
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly PaparaDbContext dbContext;

		public GenericRepository(PaparaDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public IQueryable<TEntity> Query() => dbContext.Set<TEntity>();

		/// <summary>
		/// Bu metot isterse include işlemi ile yapılabilir ve sadece isactive alanı true olanaları getirr
		/// </summary>
		/// <param name="id"></param>
		/// <param name="include"></param>
		/// <returns></returns>
		public async Task<TEntity> GetInclude(long id,
		   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
		{
			IQueryable<TEntity> queryable = Query().Where(x => x.IsActive);

			if (include != null)
				queryable = include(queryable);

			var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id);

			return entity;
		}


		// bu işlemde istersem include yaparım istersem predicate kullanırım fakat istemezsem doğrudan getAllInclude çağırıp da kullanabilirim
		// eğer include ve predicate null geçersem GetAll ile aynı işlemi yapabilirim tek farku Where(x => x.IsActive) olur 
		public async Task<List<TEntity>> GetAllInclude(
	   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
	   Expression<Func<TEntity, bool>>? predicate = null)
		{
			IQueryable<TEntity> queryable = Query();

			if (predicate != null)
				queryable = queryable.Where(predicate);

			if (include != null)
				queryable = include(queryable);

			return await queryable.ToListAsync();
		}

		/// <summary>
		/// parametre olarak sorgulamak için mesela name alanına göre getirmek istersek
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> filter)
		{
			return await Query().Where(filter).ToListAsync();
		}

		public async Task Save()
		{
			await dbContext.SaveChangesAsync();
		}


		public async Task<TEntity?> GetById(long Id, params string[] includes)
		{
			var query = dbContext.Set<TEntity>().AsQueryable();
			query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
			return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, x => x.Id == Id);
		}
		public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> expression, params string[] includes)
		{
			var query = dbContext.Set<TEntity>().AsQueryable();
			query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
			return query.Where(expression).FirstOrDefault();
		}

		public async Task<TEntity> Insert(TEntity entity)
		{
			entity.IsActive = true;
			entity.InsertDate = DateTime.UtcNow;
			entity.InsertUser = "System";
			await dbContext.Set<TEntity>().AddAsync(entity);
			return entity;

		}

		public void Update(TEntity entity)
		{
			dbContext.Set<TEntity>().Update(entity);
		}

		/// <summary>
		/// Entity gelirse isactive false çekilir ve güncellenir (SoftDelete)
		/// </summary>
		/// <param name="entity"></param>
		public void Delete(TEntity entity)
		{
			entity.IsActive = false;
			Update(entity);
		}

		/// <summary>
		///  Id bazlı gelirse isactive false çekilir ve güncellenir (SoftDelete)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task Delete(long id)
		{
			var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
			if (entity is not null)
			{
				entity.IsActive = false;
				Update(entity);
			}
		}



		public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression, params string[] includes)
		{
			var query = dbContext.Set<TEntity>().Where(expression).AsQueryable();
			query = includes.Aggregate(query, (current, inc) => current.Include(inc));
			return await query.ToListAsync();
		}

		public async Task<List<TEntity>> GetAll(params string[] includes)
		{
			var query = dbContext.Set<TEntity>().AsQueryable();
			query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
			return await EntityFrameworkQueryableExtensions.ToListAsync(query);
		}
	}
}
