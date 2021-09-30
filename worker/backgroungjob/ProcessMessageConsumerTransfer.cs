using core.domain;
using core.service.accountService;
using core.service.rabbitMQ;
using core.service.repositories;
using core.TransferProcess;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace worker.backgroundjob
{
    public class ProcessMessageConsumerTransfer : IHostedService
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ITransferProcessing _transferProcessing;
        private ITransferRepository _transferRepository;
        private ILogger<ProcessMessageConsumerTransfer> _logger;



        public ProcessMessageConsumerTransfer(IRabbitMqService rabbitMqService, ITransferProcessing transferProcessing, 
                                              ITransferRepository transferRepository)
        {
            _rabbitMqService = rabbitMqService;
            _transferProcessing = transferProcessing;
            _transferRepository = transferRepository;
        }
        public  Task StartAsync(CancellationToken stoppingToken)
        {
            try
            {
                _rabbitMqService.CreateConnection();

                var message = _rabbitMqService.receiveMessage("transferQueue", async (ReadOnlyMemory<byte> body) =>
                {

                    var message = Encoding.UTF8.GetString(body.ToArray());
                    var transfer = JsonSerializer.Deserialize<Transfer>(message);

                    _transferRepository.ChangeStatus(transfer.status, transfer.uuid);
                    var transferMade = await _transferProcessing.ProcessTransfer(Status.Processing, transfer.uuid);
                    if (!transferMade)
                        _rabbitMqService.ReQueue();

                    else
                        _rabbitMqService.FinalizaQueue();

                    Console.WriteLine($"Transfer Number {transfer.accountOrigin}|{transfer.accountDestination}|{transfer.value:N2}", message);
                    Console.ReadKey();
                });

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao consumir a mensagem", ex);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
       
    }
}
