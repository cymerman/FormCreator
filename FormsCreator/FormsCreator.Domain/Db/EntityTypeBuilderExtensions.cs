using FormsCreator.Domain.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormsCreator.Domain.Db
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : Entity
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            ConfigureAuditable(builder);
        }

        public static void ConfigureAuditable<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IAuditableEntity
        {
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Property(x => x.ModifiedBy);
            builder.Property(x => x.ModifiedOn)
                .IsRequired();
        }
    }
}