using TestIARA.Application.Common.Interfaces;
using TestIARA.Domain.Common;
using TestIARA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace TestIARA.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContext;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime, IHttpContextAccessor httpContext)
            : base(options)
        {
            _dateTime = dateTime;
            _httpContext = httpContext;
        }

        public DbSet<TCotacao> TCotacao { get; set; }
        public DbSet<TCotacaoItem> TCotacaoItem { get; set; }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            string user = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (user == null) user = "admin";
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy= user;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.State = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy= user;
                        entry.Entity.Modified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=testiara;user=dev1;password=dev1password");
        }*/
    }
}
