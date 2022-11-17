namespace Todolists.Infrastructure.Base.Mappings;

internal static class DbTypes
{
    public const string TimestampWithoutTimeZone = "datetime2";
    public const string Uuid = "int";
    public const string Boolean = "bit";
    public const string Text = "ntext";
    public const string Integer = "int";
    public const string Decimal = "money";

    public static string NVarChar(uint capacity)
        => $"nvarchar({capacity})";
}