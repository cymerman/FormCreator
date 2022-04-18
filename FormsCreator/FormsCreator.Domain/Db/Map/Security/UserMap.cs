using System;
using FormsCreator.Domain.Core.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormsCreator.Domain.Db.Map.Security
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Metoda konfigurująca EF
        /// </summary>
        /// <param name="builder">builder konfiguracji EF</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ConfigureBase();
            
            builder.Property(x => x.IsAdmin)
                .IsRequired();
            
            builder.Property(x => x.Login)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(x => x.Password)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(x => x.Email)
                .IsRequired();

            builder.HasData(
                new User
                {
                    Id = 1,
                    Login = "admin",
                    Email = "mateuszcymerman95@gmail.com",
                    Password = "admin",
                    IsAdmin = true,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = 1,
                    CreatedBy = 1,
                    ModifiedOn = DateTime.Now
                },
                new User
                {
                    Id = 2,
                    Login = "testowy",
                    Email = "cymekdeveloper@gmail.com",
                    Password = "admin",
                    IsAdmin = false,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
        }
    }
}