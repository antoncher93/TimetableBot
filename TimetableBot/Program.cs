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
                port: 10000,
                telegramBotToken: "5692929074:AAEiBfoSy4CndyOU5kx3XZNpNQ3sPlbyAPc",
                webhookHost: "https://6a71-94-243-175-101.ngrok-free.app",
                botFacadeFactory: client =>
                {
                    return ApplicationRoot.CreateBotFacade(
                        client: client,
                        sqlConnectionString: "Server=localhost,1433;Database=master;User Id=sa;Password=Pa23@!Ze7&;",
                        yandexDiskFolder: "https://disk.yandex.ru/d/48bm4CYQw5OTBw");
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