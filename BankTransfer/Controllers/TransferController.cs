using BankTransfer.Database;
using BankTransfer.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace BankTransfer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransferController: ControllerBase 
    {

        private ILogger<TransferController> _logger;
        private readonly DataContext _dataContext;

        public TransferController(ILogger<TransferController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpPost]
        public IActionResult InsertTransfer([FromBody] TransferDTO transferDTO)
        {
            try 
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "transferQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = JsonSerializer.Serialize(transferDTO);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "transferQueue",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
               Transfer transfer =  transferDTO.ConvertTransferDtoToTransfer();

                _dataContext.Add(transfer);
                _dataContext.SaveChanges();
                var aux = transfer.uuid;
                return Accepted(transferDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar criar uma nova trnsferencia", ex);

                return new StatusCodeResult(500);
            }
        }
    }
}
