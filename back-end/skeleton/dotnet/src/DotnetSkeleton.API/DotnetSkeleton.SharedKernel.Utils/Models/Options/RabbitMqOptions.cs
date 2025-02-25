namespace DotnetSkeleton.SharedKernel.Utils.Models.Options;

public sealed record RabbitMqOptions()
{
    public static string JsonKey => nameof(RabbitMqOptions);

    public required string Username { get; set; }

    public required string Password { get; set; }

    public required string HostName { get; set; }

    public required int Port { get; set; }

    public required string VirtualHost { get; set; }

    public required bool DispatchConsumersAsync { get; set; }

    public required int MaxRetryCount { get; set; }

    public required string DeadLetterQueue { get; set; }

    public required string DeadLetterExchange { get; set; }

    public required EmailQueue EmailQueue { get; set; }
}

public class EmailQueue
{
    public required string IncomingEmailQueue { get; set; }

    public required string IncomingEmailExchange { get; set; }

    public required string OutgoingEmailQueue { get; set; }

    public required string OutgoingEmailExchange { get; set; }
}