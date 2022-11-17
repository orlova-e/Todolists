using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolists.Domain.Core.Entities;

namespace Todolists.Infrastructure.Base.Mappings;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");

        builder
            .HasKey(x => x.Id)
            .HasName("Id");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_Users")
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
            .Property(x => x.Name)
            .HasColumnType(DbTypes.Text)
            .HasColumnName("Name")
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
            .HasOne(x => x.Account)
            .WithOne(x => x.User)
            .HasForeignKey<Account>(x => x.UserId)
            .HasConstraintName("FK_Accounts_UserId");
    }
}