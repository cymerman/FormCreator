using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormsCreator.Domain.Db.Map.Form
{
    public class FormMap : IEntityTypeConfiguration<Core.Forms.Form>
    {
        /// <summary>
        /// Metoda konfigurująca EF
        /// </summary>
        /// <param name="builder">builder konfiguracji EF</param>
        public void Configure(EntityTypeBuilder<Core.Forms.Form> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedOn)
                .IsRequired();
            
            builder.Property(x => x.Data)
                .IsRequired();

            builder.HasOne(x => x.FormDefinition);
        }
    }
}