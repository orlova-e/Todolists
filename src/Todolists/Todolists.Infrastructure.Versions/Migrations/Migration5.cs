using FluentMigrator;

namespace Todolists.Infrastructure.Versions.Migrations;

[Migration(5, "add options table")]
public class Migration5 : Migration
{
    public override void Up()
    {
        Create
            .Table("Options")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("IsDeleted").AsBoolean().NotNullable()
            .WithColumn("Checked").AsBoolean().NotNullable()
            .WithColumn("Text").AsString().Nullable()
            .WithColumn("ChecklistId").AsGuid().NotNullable();
        
        Create
            .ForeignKey("FK_Options_ChecklistId")
            .FromTable("Options").ForeignColumn("ChecklistId")
            .ToTable("Checklists").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete
            .Table("Options");
    }
}