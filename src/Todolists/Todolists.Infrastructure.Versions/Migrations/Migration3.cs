using FluentMigrator;

namespace Todolists.Infrastructure.Versions.Migrations;

[Migration(3, "add notebases table")]
public class Migration3 : Migration
{
    public override void Up()
    {
        Create
            .Table("NoteBases")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("IsDeleted").AsBoolean().NotNullable()
            .WithColumn("Name").AsString().Nullable()
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("Created").AsDateTime().NotNullable()
            .WithColumn("Updated").AsDateTime().Nullable()
            .WithColumn("Deleted").AsDateTime().Nullable();
        
        Create
            .ForeignKey("FK_NoteBases_UserId")
            .FromTable("NoteBases").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete
            .Table("NoteBases");
    }
}