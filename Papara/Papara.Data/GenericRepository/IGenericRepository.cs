using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace Papara.Data.GenericRepository
{
	public interface IGenericRepository<TEntity> : IQuery<TEntity> where TEntity : class
	{
		// Ödevi ekle where include 

		Task<TEntity> GetInclude(
			long id,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

		/// <summary>
		/// // Recep Hoca Where.
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="includes"></param>
		/// <returns></returns>
		Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression, params string[] includes); 

		Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> filter); // belirli bir koşula gçre <List<TEntity>> getir. Nesneleri filtrelemek için de expression kullandım.  
		Task<List<TEntity>> GetAllInclude(
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			Expression<Func<TEntity, bool>>? predicate = null
			); // Ef Core'da navigation propların yani ilişkili tabloların sorguya dahil edilmesi için (IIncludableQueryable) kullandım
			   // Ana nesnem ilk yüklendiğinde ilişkili nesneler de yüklensin (eager loading)
			   // Func kullandım çünkü nesneyi saklayıp gerektiğinde çağırmak istiyorum. IQueryable<TEntity> girdi alıp, IIncludableQueryable<TEntity, object> tipinde çıktı alacağım.
			   // include = null dedim çünkü include yapmak istemeyebilirim yani zorunda değilim
			   // TEntity şuan için customer oldu 

		Task Save();
		Task<TEntity?> GetById(long Id, params string[] includes);

		Task<TEntity> Insert(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		Task Delete(long Id);
		Task<List<TEntity>> GetAll(params string[] includes);


		Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> expression, params string[] includes);

	}
}
