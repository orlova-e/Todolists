using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class ChecklistConfiguration : IEntityTypeConfiguration<Checklist>
{
    public void Configure(EntityTypeBuilder<Checklist> builder)
    {
        builder
            .ToTable("checklists");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_checklists")
            .IsUnique();

        builder
            .HasMany(x => x.Options)
            .WithOne()
            .HasForeignKey("checklistid");
    }
}