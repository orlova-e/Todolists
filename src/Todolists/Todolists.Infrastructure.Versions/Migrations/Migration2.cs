using FluentMigrator;

namespace Todolists.Infrastructure.Versions.Migrations;

[Migration(2, "add accounts table")]
public class Migration2 : Migration
{
    public override void Up()
    {
        Create
            .Table("Accounts")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("IsDeleted").AsBoolean().NotNullable().Indexed()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Hash").AsString().NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("Created").AsDateTime().NotNullable()
            .WithColumn("Updated").AsDateTime().Nullable()
            .WithColumn("Deleted").AsDateTime().Nullable();

        Create
            .ForeignKey("FK_Accounts_UserId")
            .FromTable("Accounts").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete
            .Table("Accounts");
    }
}