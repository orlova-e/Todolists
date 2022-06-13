using FluentMigrator;

namespace Todolists.Infrastructure.Versions.Migrations;

[Migration(5, "add options table")]
public class Migration5 : Migration
{
    public override void Up()
    {
        Create
            .Table("options")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable()
            .WithColumn("checked").AsBoolean().NotNullable()
            .WithColumn("text").AsString().Nullable()
            .WithColumn("checklistid").AsGuid().NotNullable();
        
        Create
            .ForeignKey("FK_options_checklistid")
            .FromTable("options").ForeignColumn("checklistid")
            .ToTable("checklists").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("options");
    }
}