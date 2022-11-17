using FluentMigrator;

namespace Todolists.Infrastructure.Versions.Migrations;

[Migration(1, "add users table")]
public class Migration1 : Migration
{
    public override void Up()
    {
        Create
            .Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("IsDeleted").AsBoolean().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Created").AsDateTime().NotNullable()
            .WithColumn("Updated").AsDateTime().Nullable()
            .WithColumn("Deleted").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete
            .Table("Users");
    }
}