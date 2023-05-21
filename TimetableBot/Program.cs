using Telegram.Bot.Hosting;
using TimetableBot;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Application started");
        
        var cts = new CancellationTokenSource();

        Console.CancelKeyPress += (sender, eventArgs) => cts.Cancel();

        BotHost.StartAsync(
                port: 5010,
                telegramBotToken: "5692929074:AAEiBfoSy4CndyOU5kx3XZNpNQ3sPlbyAPc",
                webhookHost: "https://f86c-94-243-175-101.ngrok-free.app",
                botFacadeFactory: client =>
                {
                    return ApplicationRoot.CreateBotFacade(
                        client: client);
                })
            .GetAwaiter()
            .GetResult();

        BotHost
            .WaitForShutdownAsync(cts.Token)
            .GetAwaiter()
            .GetResult();
        
        Console.WriteLine("Application stopped");
    }
}