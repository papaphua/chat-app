namespace ChatApp.Server.Application.Core;

public sealed class MessageTemplate(string receiver, string content)
{
    public string Receiver { get; init; } = receiver;

    public string Content { get; init; } = content;

    public static MessageTemplate Confirmation(string receiver, string token)
    {
        var message = $"Your confirmation code: {token}";

        return new MessageTemplate(receiver, message);
    }
}