using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Donators.Entites.Context;

public class DonatorsDBContext(DbContextOptions<DonatorsDBContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Donator> Donators { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);  
    }
}
