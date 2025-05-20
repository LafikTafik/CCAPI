using Microsoft.EntityFrameworkCore;

namespace CCAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet для каждой таблицы
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Orders> Order { get; set; } = null!;
        public DbSet<Cargos> Cargo { get; set; } = null!;
        public DbSet<Transportation> Shipping { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Driver> Drivers { get; set; } = null!;

        // Конфигурация отношений
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Пример конфигурации отношения многие-ко-многим между Заказами и Перевозками
            modelBuilder.Entity<Transportation>()
                .HasKey(t => new { t.LoadId, t.VehicleId });

            modelBuilder.Entity<Transportation>()
                .HasOne(t => t.Load)
                .WithMany(l => l.Transportations)
                .HasForeignKey(t => t.LoadId);

            modelBuilder.Entity<Transportation>()
                .HasOne(t => t.Vehicle)
                .WithMany(v => v.Transportations)
                .HasForeignKey(t => t.VehicleId);

            // Другие конфигурации отношений можно добавить здесь
        }
    }
}