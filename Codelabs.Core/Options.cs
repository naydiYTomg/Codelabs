namespace Codelabs.Core;

public static class Options
{
    public static readonly string ConnectionString = Environment.GetEnvironmentVariable("DATABASE_ACCESS");
    public static readonly string GmailAddress = Environment.GetEnvironmentVariable("GMAIL_ADDRESS");
    public static readonly string GmailPassword = Environment.GetEnvironmentVariable("GMAIL_PASSWORD");
}
