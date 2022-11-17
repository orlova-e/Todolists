using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder
            .ToTable("Options");
        
        builder
            .HasKey(x => x.Id)
            .HasName("Id");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_Options")
            .IsUnique();
        
        builder
            .Property(x => x.Id)
            .HasColumnType(DbTypes.Uuid)
            .HasColumnName("Id")
            .IsRequired();
        
        builder
            .Property<Guid>("ChecklistId")
            .IsRequired();
        
        builder
            .Property(x => x.IsDeleted)
            .HasColumnType(DbTypes.Boolean)
            .HasColumnName("IsDeleted")
            .IsRequired();
        
        builder
            .Property(x => x.Checked)
            .HasColumnType(DbTypes.Boolean)
            .HasColumnName("Checked")
            .IsRequired();
        
        builder
            .Property(x => x.Text)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("Text")
            .IsRequired(false);

        builder
            .HasQueryFilter(x => !x.IsDeleted);
    }
}