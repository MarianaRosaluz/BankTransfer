using core.service.accountService;
using core.service.rabbitMQ;
using core.service.repositories;
using core.TransferProcess;
using Microsoft.Extensions.DependencyInjection;


namespace BankTransferApi.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureService(this IServiceCollection service)
        {
            service.AddTransient<IRabbitMqService, RabbitMqService>();
            service.AddTransient<ITransferProcessing, TransferProcessing>();
            service.AddTransient<ITransferRepository, TransferRepository>();
        }
    }
}
