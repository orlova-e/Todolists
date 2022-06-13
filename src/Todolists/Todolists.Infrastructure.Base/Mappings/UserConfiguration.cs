using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("users");

        builder
            .HasKey(x => x.Id)
            .HasName("id");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_users")
            .IsUnique();
        
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
            .Property(x => x.Name)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("name")
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
            .HasOne(x => x.Account)
            .WithOne(x => x.User)
            .HasForeignKey<Account>(x => x.UserId)
            .HasConstraintName("FK_accounts_userid");
    }
}