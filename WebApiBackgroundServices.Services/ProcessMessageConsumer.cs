using Domain.Database;
using Domain.Database.Enum;
using Domain.Entities;
using Domain.Repository;
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
        private ITransferRepository _transferRepository;



        public ProcessMessageConsumer(IRabbitMqService rabbitMqService, ITransferProcessing transferProcessing, ITransferRepository transferRepository)
        {
            _rabbitMqService = rabbitMqService;
            _transferProcessing = transferProcessing;
            _transferRepository = transferRepository;
        }
        public  Task StartAsync(CancellationToken stoppingToken)
        {
            
            var message = _rabbitMqService.receiveMessage("transferQueue", async (ReadOnlyMemory<byte> body) => {

                var message = Encoding.UTF8.GetString(body.ToArray());
                var transfer = JsonSerializer.Deserialize<Transfer>(message);

                _transferRepository.ChangeStatus(transfer.status, transfer.uuid);
                var transferMade =  await _transferProcessing.ProcessTransfer(Status.Processing,transfer.uuid);
                if (!transferMade)
                    _rabbitMqService.ReQueue();
                else
                    _rabbitMqService.FinalizaQueue();

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
