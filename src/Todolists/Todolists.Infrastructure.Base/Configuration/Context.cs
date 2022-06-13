using Microsoft.EntityFrameworkCore;
using Todolists.Domain.Core.Entities;
using Todolists.Infrastructure.Base.Mappings;

namespace Todolists.Infrastructure.Base.Configuration;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}