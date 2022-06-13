using FluentMigrator;

namespace Todolists.Infrastructure.Versions.Migrations;

[Migration(4, "add checklists table")]
public class Migration4 : Migration
{
    public override void Up()
    {
        Create
            .Table("checklists")
            .WithColumn("id").AsGuid().PrimaryKey();
        
        Create
            .ForeignKey("FK_checklists_notebases_id")
            .FromTable("checklists").ForeignColumn("id")
            .ToTable("notebases").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("checklists");
    }
}