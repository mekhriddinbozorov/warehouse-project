using Telegram.Bot;
using Telegram.Bot.Polling;

namespace warehouse_project.Dtos.CategoryDto;
public class BotBackgroundService : BackgroundService
{
    private readonly ILogger<BotBackgroundService> logger;
    private readonly ITelegramBotClient botClient;
    private readonly IUpdateHandler updateHandler;

    public BotBackgroundService(
        ILogger<BotBackgroundService> logger,
        ITelegramBotClient botClient,
        IUpdateHandler updateHandler
    )
    {
        this.logger = logger;
        this.botClient = botClient;
        this.updateHandler = updateHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var bot = await botClient.GetMeAsync(stoppingToken);
            logger.LogInformation("Bot {username} started polling updates.", bot.Username);

            botClient.StartReceiving(
                updateHandler: updateHandler,
                receiverOptions: new ReceiverOptions()
                {
                    ThrowPendingUpdates = true
                },
                cancellationToken: stoppingToken
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to connect to bot server.");
        }
    }
}