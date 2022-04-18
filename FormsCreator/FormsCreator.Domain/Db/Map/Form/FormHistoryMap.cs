using FormsCreator.Domain.Core.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormsCreator.Domain.Db.Map.Form
{
    public class FormHistoryMap : IEntityTypeConfiguration<FormHistory>
    {
        /// <summary>
        /// Metoda konfigurująca EF
        /// </summary>
        /// <param name="builder">builder konfiguracji EF</param>
        public void Configure(EntityTypeBuilder<FormHistory> builder)
        {
            builder.ConfigureBase();

            builder.Property(x => x.Text);
            
            builder.HasOne(x => x.Form)
                .WithMany(x => x.FormHistories)
                .HasForeignKey(x => x.FormId);

            builder.HasOne(x => x.User)
                .WithMany() // If User can have multiple FormDefinitions
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}