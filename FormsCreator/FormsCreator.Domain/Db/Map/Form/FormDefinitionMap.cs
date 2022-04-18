using FormsCreator.Domain.Core.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormsCreator.Domain.Db.Map.Form
{
    public class FormDefinitionMap : IEntityTypeConfiguration<FormDefinition>
    {
        /// <summary>
        /// Metoda konfigurująca EF
        /// </summary>
        /// <param name="builder">builder konfiguracji EF</param>
        public void Configure(EntityTypeBuilder<FormDefinition> builder)
        {
            builder.ConfigureBase();

            builder.Property(x => x.Title)
                .IsRequired();
            
            builder.Property(x => x.Name)
                .IsRequired();
            
            builder.Property(x => x.FormUid)
                .IsRequired();

            builder.Property(x => x.Description);
            builder.Property(x => x.Form);
            builder.Property(x => x.Password);
            builder.Property(x => x.IsFileCountLimited);
            builder.Property(x => x.FileCount);
            builder.Property(x => x.FileCountMinimum);
            builder.Property(x => x.FileCountMaximum);

            builder.HasMany(x => x.Children);
            
            builder.HasOne(x => x.User)
                .WithMany() // If User can have multiple FormDefinitions
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}