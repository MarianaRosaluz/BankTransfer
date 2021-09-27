using Domain.Database;
using Domain.Database.Enum;
using Domain.Entities;
using Domain.TransferProcess;
using Microsoft.Extensions.Hosting;
using Services.Services;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiBackgroundServices.Services
{
    public class ProcessMessageConsumer : IHostedService
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ITransferProcessing _transferProcessing;



        public ProcessMessageConsumer(IRabbitMqService rabbitMqService, ITransferProcessing transferProcessing)
        {
            _rabbitMqService = rabbitMqService;
            _transferProcessing = transferProcessing;
        }
        public  Task StartAsync(CancellationToken stoppingToken)
        {
           
            var message = _rabbitMqService.receiveMessage("transferQueue", async (ReadOnlyMemory<byte> body) => {

                var message = Encoding.UTF8.GetString(body.ToArray());
                var transfer = JsonSerializer.Deserialize<Transfer>(message);

               _transferProcessing.ChangeStatus(Status.Processing,transfer.uuid);

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
