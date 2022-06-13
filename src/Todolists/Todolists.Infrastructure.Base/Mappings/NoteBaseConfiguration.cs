using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class NoteBaseConfiguration : IEntityTypeConfiguration<NoteBase>
{
    public void Configure(EntityTypeBuilder<NoteBase> builder)
    {
        builder
            .ToTable("notebases");
        
        builder
            .HasKey(x => x.Id)
            .HasName("id");
        
        builder
            .Property(x => x.Id)
            .HasColumnType(DbTypes.Uuid)
            .HasColumnName("id")
            .IsRequired();
        
        builder
            .Property(x => x.IsDeleted)
            .HasColumnType(DbTypes.Boolean)
            .HasColumnName("isdeleted")
            .IsRequired();

        builder
            .Property(x => x.Created)
            .HasColumnType(DbTypes.TimestampWithoutTimeZone)
            .HasColumnName("created")
            .IsRequired();
        
        builder
            .Property(x => x.Updated)
            .HasColumnType(DbTypes.TimestampWithoutTimeZone)
            .HasColumnName("updated")
            .IsRequired(false);
        
        builder
            .Property(x => x.Deleted)
            .HasColumnType(DbTypes.TimestampWithoutTimeZone)
            .HasColumnName("deleted")
            .IsRequired(false);
        
        builder
            .Property(x => x.Name)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("name")
            .IsRequired(false);
        
        builder
            .Property(x => x.UserId)
            .HasColumnType(DbTypes.Uuid)
            .HasColumnName("userid")
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_notebases_userid");
    }
}