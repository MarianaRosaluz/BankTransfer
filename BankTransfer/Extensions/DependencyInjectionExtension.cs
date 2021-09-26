using Microsoft.Extensions.DependencyInjection;
using Services.Services;


namespace BankTransfer.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureService(this IServiceCollection service)
        {
            service.AddTransient<IRabbitMqService, RabbitMqService>();
        }
    }
}
