using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder
            .ToTable("options");
        
        builder
            .HasKey(x => x.Id)
            .HasName("id");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_options")
            .IsUnique();
        
        builder
            .Property(x => x.Id)
            .HasColumnType(DbTypes.Uuid)
            .HasColumnName("id")
            .IsRequired();
        
        builder
            .Property<Guid>("checklistid")
            .IsRequired();
        
        builder
            .Property(x => x.IsDeleted)
            .HasColumnType(DbTypes.Boolean)
            .HasColumnName("isdeleted")
            .IsRequired();
        
        builder
            .Property(x => x.Checked)
            .HasColumnType(DbTypes.Boolean)
            .HasColumnName("checked")
            .IsRequired();
        
        builder
            .Property(x => x.Text)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("text")
            .IsRequired(false);

        builder
            .HasQueryFilter(x => !x.IsDeleted);
    }
}