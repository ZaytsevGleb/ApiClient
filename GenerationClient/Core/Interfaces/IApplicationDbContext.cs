using GenerationClient.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace GenerationClient.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
