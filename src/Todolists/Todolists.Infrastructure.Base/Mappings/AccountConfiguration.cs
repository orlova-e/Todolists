using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ToTable("Accounts");

        builder
            .HasKey(x => x.Id)
            .HasName("Id");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_Accounts")
            .IsUnique();
        
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
            .Property(x => x.Email)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("Email")
            .IsRequired();
        
        builder
            .Property(x => x.Hash)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("Hash")
            .IsRequired();
        
        builder
            .Property(x => x.UserId)
            .HasColumnType(DbTypes.Uuid)
            .HasColumnName("UserId")
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
    }
}