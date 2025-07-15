using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.src.Models;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
  public DbSet<Session> Sessions { get; set; }
  public DbSet<CostCategory> CostCategories { get; set; }
  public DbSet<Spending> Spendings { get; set; }

  public ApplicationDbContext(DbContextOptions options)
            : base(options)
  {
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<User>().ToTable("users");

    builder.Entity<Spending>()
        .HasOne(s => s.CostCategory)
        .WithMany(c => c.Spendings)
        .HasForeignKey(s => s.CostCategoryId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
