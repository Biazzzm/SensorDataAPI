using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SensorData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorData.Data.Mappings
{
    public class SensorMap
    {
        public void Configure(EntityTypeBuilder<SensorModel> builder)
        {
            builder.ToTable("Sensors");

            // Configuração da chave primária
            builder.HasKey(x => x.Id); // Definindo a chave primária

            // Configuração da coluna SensorValue
            builder.Property(x => x.SensorValue)
                .HasColumnType("int")
                .IsRequired();

            // Configuração da coluna Timestamp
            builder.Property(x => x.Timestamp)
                .HasColumnType("DateTime")
                .IsRequired();

            // Configuração da coluna UserId
            builder.Property(x => x.UserId)
                .IsRequired();

            // Relacionamento com UserModel
            builder.HasOne(x => x.User) // Cada Sensor tem um User
                .WithMany() // Não estamos mantendo uma coleção de sensores no UserModel
                .HasForeignKey(x => x.UserId) // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Se o User for deletado, o Sensor também será

            // Caso queira configurar o nome da coluna UserId especificamente
            builder.Property(x => x.UserId)
                .HasColumnName("UserId"); // Nome da coluna no banco de dados

        }
    }
}

