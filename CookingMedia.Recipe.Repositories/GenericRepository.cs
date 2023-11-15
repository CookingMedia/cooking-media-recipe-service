using System.Linq.Expressions;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.EntityModels.Dto.Requests;
using CookingMedia.Recipe.EntityModels.Dto.Responses;
using Microsoft.EntityFrameworkCore;

namespace CookingMedia.Recipe.Repositories;

public class GenericRepository<TEntity>
    where TEntity : GenericEntity
{
    internal readonly CookingMediaRecipeDbContext Context;
    internal readonly DbSet<TEntity> DbSet;

    public GenericRepository(CookingMediaRecipeDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        IEnumerable<Expression<Func<TEntity, bool>>>? filters = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = ""
    )
    {
        IQueryable<TEntity> query = DbSet;

        if (filters != null)
            query = filters.Aggregate(query, (current, filter) => current.Where(filter));

        query = includeProperties
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }

    public virtual PageResult<TEntity> Get(
        PageRequest pageRequest,
        IEnumerable<Expression<Func<TEntity, bool>>>? filters = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = ""
    )
    {
        IQueryable<TEntity> query = DbSet;

        if (filters != null)
            query = filters.Aggregate(query, (current, filter) => current.Where(filter));

        query = includeProperties
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        query = orderBy != null ? orderBy(query) : query;

        var result = query.AddPaging(pageRequest).ToPageResult();
        return result;
    }

    public virtual TEntity? GetById(object id)
    {
        return DbSet.Find(id);
    }

    public virtual bool ExistById(object id)
    {
        return DbSet.Any(x => x.Id == (int)id);
    }

    public virtual void Insert(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        var entityToDelete = DbSet.Find(id);
        if (entityToDelete != null)
            Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }

        DbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        DbSet.Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}
