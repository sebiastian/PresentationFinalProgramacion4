using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UserManagerDbContext : DbContext
{
    public UserManagerDbContext(DbContextOptions<UserManagerDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Photographer> Photographers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<PhotoSession> PhotoSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la herencia TPH (Table Per Hierarchy) para User y Photographer
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<User>("User")
            .HasValue<Photographer>("Photographer");

        // Configuración de PhotoSession
        modelBuilder.Entity<PhotoSession>()
            .HasOne(ps => ps.Location)
            .WithMany(l => l.Sessions)
            .HasForeignKey(ps => ps.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PhotoSession>()
            .HasOne(ps => ps.Client)
            .WithMany()
            .HasForeignKey(ps => ps.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PhotoSession>()
            .HasOne(ps => ps.Photographer)
            .WithMany()
            .HasForeignKey(ps => ps.PhotographerId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configurar enums como strings en la base de datos
        modelBuilder.Entity<PhotoSession>()
            .Property(ps => ps.SessionType)
            .HasConversion<string>();

        modelBuilder.Entity<PhotoSession>()
            .Property(ps => ps.Status)
            .HasConversion<string>();
    }
}
