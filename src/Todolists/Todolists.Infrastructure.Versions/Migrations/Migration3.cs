using FluentMigrator;

namespace Todolists.Infrastructure.Versions.Migrations;

[Migration(3, "add notebases table")]
public class Migration3 : Migration
{
    public override void Up()
    {
        Create
            .Table("notebases")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable()
            .WithColumn("name").AsString().Nullable()
            .WithColumn("userid").AsGuid().NotNullable()
            .WithColumn("created").AsDateTime().NotNullable()
            .WithColumn("updated").AsDateTime().Nullable()
            .WithColumn("deleted").AsDateTime().Nullable();
        
        Create
            .ForeignKey("FK_notebases_userid")
            .FromTable("notebases").ForeignColumn("userid")
            .ToTable("users").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("notebases");
    }
}