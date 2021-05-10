using TestIARA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace TestIARA.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TCotacao> TCotacao { get; set; }
        DbSet<TCotacaoItem> TCotacaoItem { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
