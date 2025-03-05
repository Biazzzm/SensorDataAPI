using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SensorData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorData.Data.Mappings
{
    internal class UserMap
    {
        public void Configure(EntityTypeBuilder<SensorData.Models.UserModel> builder)
        {
            builder.ToTable("Users");

            // Configuração da chave primária
            builder.HasKey(x => x.Id);

            // Configuração das propriedades de Name, Email e Password
            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(100)") // Exemplo de tamanho de campo
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();    

            builder.Property(x => x.Password)
                .HasColumnType("nvarchar(255)") // Tamanho do campo para a senha
                .IsRequired();

            // Relacionamento com a coleção de Alertas (já configurado anteriormente)
            builder.HasMany(x => x.AlertasList)
                .WithOne(x => x.User) // Um Alerta está associado a um User
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento 1:1 com Sensor (um usuário tem um único sensor)
            builder.HasOne(x => x.Sensors) // Um usuário tem um sensor
                .WithOne(x => x.User) // Um sensor tem um único usuário
                .HasForeignKey<SensorModel>(x => x.UserId) // A chave estrangeira é no Sensor
                .OnDelete(DeleteBehavior.Cascade); // Deleção em cascata
        }
    }
}
