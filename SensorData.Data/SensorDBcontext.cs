using Microsoft.EntityFrameworkCore;
using SensorData.Models;

namespace SensorData.Data
{
    public class SensorDBcontext : DbContext
    {
        public SensorDBcontext()
        {
        }

        public SensorDBcontext(DbContextOptions<SensorDBcontext> options)
            : base(options) { }
        public DbSet<UserModel> Users { get; set; }

        public DbSet<ContactModel> Contacts { get; set; }

        public DbSet<AlertaModel> Alertas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().HasIndex(x => x.Email).IsUnique();
            // Aplica todas as configurações do assembly (o que inclui as classes de mapeamento)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SensorDBcontext).Assembly);
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("Cheiro de Problema");
            }
        }
    }
}
