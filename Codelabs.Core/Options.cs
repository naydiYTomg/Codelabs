namespace Codelabs.Core;

public static class Options
{
    public static readonly string ConnectionString = /*"Server=localhost;Port=5432;User Id=postgres;Password=azure;Database=Codelabs"*/Environment.GetEnvironmentVariable("DATABASE_ACCESS")!;
}
