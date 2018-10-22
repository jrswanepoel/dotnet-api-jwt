using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Starter.Data.Context
{
    public interface IDataContext
    {
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        //EF Core method, not to be confused with DbSet Entries
        EntityEntry Entry(object entity);

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
