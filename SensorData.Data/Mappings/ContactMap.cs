using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SensorData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SensorData.Data.Mappings
{
    public class ContactMap
    {
        public void Configure(EntityTypeBuilder<ContactModel> builder)
        {
            builder.ToTable("Contacts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(160)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.ChatId)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.ContactsList)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);





        }
    }
}