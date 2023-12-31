using Log.Models;
using Setup.Models;
using Microsoft.EntityFrameworkCore;
using Users.Models;

public class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LogEntity> LogEntities { get; set; } = null!;
    public virtual DbSet<SetupEntity> SetupEntities { get; set; } = null!;
    public virtual DbSet<UsersEntity> UsersEntities { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEntity>(entity =>
        {
            entity.ToTable("Log");
        });

        modelBuilder.Entity<SetupEntity>(entity =>
        {
            entity.ToTable("Setup");
        });

        modelBuilder.Entity<UsersEntity>(entity =>
        {
            entity.ToTable("Users");
        });
    }
}
