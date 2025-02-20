using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SensorData.Models;

namespace SensorData.Data.Mappings
{
    public class AlertaMap
    {
        public void Configure(EntityTypeBuilder<AlertaModel> builder)
        {
            builder.ToTable("Alertas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SensorValue)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.DataHora)
                .HasColumnType("DateTime")
                .IsRequired();


            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.AlertasList)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Adicionando o mapeamento da nova propriedade SensorType
            builder.Property(x => x.SensorType)
                .HasColumnType("nvarchar(50)") // Ou "varchar(50)" dependendo do banco de dados
                .IsRequired();

        }
    }
}


