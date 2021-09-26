using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Services;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebApiBackgroundServices.dto;

namespace WebApiBackgroundServices.Services
{
    public class ProcessMessageConsumer : IHostedService
    {
        private readonly IRabbitMqService _rabbitMqService;

        public ProcessMessageConsumer(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        public  Task StartAsync(CancellationToken stoppingToken)
        {
           
            var message = _rabbitMqService.receiveMessage("transferQueue", async (ReadOnlyMemory<byte> body) => {

                var message = Encoding.UTF8.GetString(body.ToArray());
                var transfer = JsonSerializer.Deserialize<TransferDTO>(message);

                Console.WriteLine($"Transfer Number {transfer.accountOrigin}|{transfer.accountDestination}|{transfer.value:N2}", message);

            });
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
