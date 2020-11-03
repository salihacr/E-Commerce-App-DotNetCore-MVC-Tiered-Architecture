using E_Commerce.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Concrete
{
    public interface EFCoreGenericRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {

    }
}