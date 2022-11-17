using FluentMigrator;

namespace Todolists.Infrastructure.Versions.Migrations;

[Migration(4, "add checklists table")]
public class Migration4 : Migration
{
    public override void Up()
    {
        Create
            .Table("Checklists")
            .WithColumn("Id").AsGuid().PrimaryKey();
        
        Create
            .ForeignKey("FK_Checklists_NoteBases_Id")
            .FromTable("Checklists").ForeignColumn("Id")
            .ToTable("NoteBases").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete
            .Table("Checklists");
    }
}