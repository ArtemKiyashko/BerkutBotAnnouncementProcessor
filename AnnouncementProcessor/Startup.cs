using System;
using AnnouncementProcessor.Managers;
using AnnouncementProcessor.Options;
using AnnouncementProcessor.Proxies;
using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
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

            builder.Services.AddAzureClients(clients => {
                clients.UseCredential(new DefaultAzureCredential());
                clients.AddBlobServiceClient(_functionConfig.GetSection("Storage"));
            });

            builder.Services.AddScoped<IBlobServiceManager, BlobServiceManager>();
        }
    }
}

