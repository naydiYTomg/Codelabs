namespace Codelabs.Core;

public static class Options
{
    public static readonly string ConnectionString = Environment.GetEnvironmentVariable("DATABASE_ACCESS");
}
