using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class NoteBaseConfiguration : IEntityTypeConfiguration<NoteBase>
{
    public void Configure(EntityTypeBuilder<NoteBase> builder)
    {
        builder
            .ToTable("NoteBases");
        
        builder
            .HasKey(x => x.Id)
            .HasName("Id");
        
        builder
            .Property(x => x.Id)
            .HasColumnType(DbTypes.Uuid)
            .HasColumnName("Id")
            .IsRequired();
        
        builder
            .Property(x => x.IsDeleted)
            .HasColumnType(DbTypes.Boolean)
            .HasColumnName("IsDeleted")
            .IsRequired();

        builder
            .Property(x => x.Created)
            .HasColumnType(DbTypes.TimestampWithoutTimeZone)
            .HasColumnName("Created")
            .IsRequired();
        
        builder
            .Property(x => x.Updated)
            .HasColumnType(DbTypes.TimestampWithoutTimeZone)
            .HasColumnName("Updated")
            .IsRequired(false);
        
        builder
            .Property(x => x.Deleted)
            .HasColumnType(DbTypes.TimestampWithoutTimeZone)
            .HasColumnName("Deleted")
            .IsRequired(false);
        
        builder
            .Property(x => x.Name)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("Name")
            .IsRequired(false);
        
        builder
            .Property(x => x.UserId)
            .HasColumnType(DbTypes.Uuid)
            .HasColumnName("UserId")
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_NoteBases_UserId");
    }
}