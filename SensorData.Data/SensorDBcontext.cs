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

            // Aplica todas as configurações do assembly (o que inclui as classes de mapeamento)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SensorDBcontext).Assembly);
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=CheiroDeProblema;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");
            }
        }
    }
}
