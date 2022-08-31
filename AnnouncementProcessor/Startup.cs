using System;
using AnnouncementProcessor.Options;
using AnnouncementProcessor.Proxies;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(AnnouncementProcessor.Startup))]
namespace AnnouncementProcessor
{
	public class Startup : FunctionsStartup
	{
        private IConfigurationRoot _functionConfig;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _functionConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Configure<BotOptions>(_functionConfig.GetSection("BotOptions"));
            builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
                new TelegramBotClient(provider.GetRequiredService<IOptions<BotOptions>>().Value.Token));
            builder.Services.AddTransient<ITelegramBotMessageProxy, TelegramBotMessageProxy>();
        }
    }
}

