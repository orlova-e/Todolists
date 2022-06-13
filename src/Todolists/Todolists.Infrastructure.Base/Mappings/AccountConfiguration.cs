using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ToTable("accounts");

        builder
            .HasKey(x => x.Id)
            .HasName("id");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_accounts")
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
            .Property(x => x.Email)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("email")
            .IsRequired();
        
        builder
            .Property(x => x.Hash)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("hash")
            .IsRequired();
        
        builder
            .Property(x => x.UserId)
            .HasColumnType(DbTypes.Uuid)
            .HasColumnName("userid")
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
    }
}