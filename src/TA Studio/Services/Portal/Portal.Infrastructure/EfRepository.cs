using Microsoft.EntityFrameworkCore;
using Portal.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Infrastructure
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T GetById(int id);

        //IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        List<T> GetAll();

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        //void BulkInsert(List<T> entities);

        //void BulkUpdate(List<T> entities);

        //void BulkDelete(List<T> entities);

        //Task<T> GetByIdAsync(int id);

        //Task<List<T>> GetAllAsync();

        //Task CreateAsync(T entity);

        //Task UpdateAsync(T entity);

        //Task DeleteAsync(T entity);

        //Task BulkInsertAsync(List<T> entities);

        //Task BulkUpdateAsync(List<T> entities);

        //Task BulkDeleteAsync(List<T> entities);
    }

    public class EfRepository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly PortalContext Context;
        protected readonly DbSet<T> DbSet;

        //protected readonly ICacheManager CacheManager;
        public EfRepository(
            PortalContext portalContext)
        {
            Context = portalContext ?? throw new ArgumentNullException(nameof(portalContext));
            DbSet = Context.Set<T>();
            //CacheManager = cacheManager;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        //public virtual IEnumerable<T> Get(
        //    Expression<Func<T, bool>> filter = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    string includeProperties = "")
        //{
        //    IQueryable<T> query = DbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);// Filter a sequence of values
        //    }

        //    if (includeProperties != null)// Include Entity
        //    {
        //        foreach (var includeProperty in includeProperties.Split
        //            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProperty);
        //        }
        //    }

        //    if (orderBy != null)// Sort values
        //    {
        //        return orderBy(query).ToList();
        //    }
        //    else
        //    {
        //        return query.ToList();
        //    }
        //}

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        //public virtual void BulkInsert(List<T> entities)
        //{
        //    var bulkOptions = new BulkConfig();

        //    //Check FK
        //    bulkOptions.SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints;
        //    //Get returned Ids
        //    bulkOptions.SetOutputIdentity = true;

        //    Context.BulkInsert(entities, bulkConfig: bulkOptions);
        //}

        //public virtual void BulkUpdate(List<T> entities)
        //{
        //    Context.BulkUpdate(entities);
        //}

        //public virtual void BulkDelete(List<T> entities)
        //{
        //    Context.BulkDelete(entities);
        //}
        //public async Task<T> GetByIdAsync(int id)
        //{
        //    return await DbSet.FindAsync(id);
        //}
        //public async Task<List<T>> GetAllAsync()
        //{
        //    return await DbSet.ToListAsync();
        //}
        //public virtual async Task CreateAsync(T entity)
        //{
        //    await DbSet.AddAsync(entity);
        //}

        //public virtual Task UpdateAsync(T entity)
        //{
        //    Context.Entry(entity).State = EntityState.Modified;
        //    return Task.FromResult(0);
        //}

        //public virtual Task DeleteAsync(T entity)
        //{
        //    DbSet.Remove(entity);
        //    return Task.FromResult(0);
        //}
        //public virtual async Task BulkInsertAsync(List<T> entities)
        //{
        //    var bulkOptions = new BulkConfig();

        //    //Check FK
        //    bulkOptions.SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints;
        //    //Get returned Ids
        //    bulkOptions.SetOutputIdentity = true;

        //    await Context.BulkInsertAsync(entities, bulkConfig: bulkOptions);
        //}

        //public virtual async Task BulkUpdateAsync(List<T> entities)
        //{
        //    await Context.BulkUpdateAsync(entities);
        //}

        //public virtual async Task BulkDeleteAsync(List<T> entities)
        //{
        //    await Context.BulkDeleteAsync(entities);
        //}
    }
}