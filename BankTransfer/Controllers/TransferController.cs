using BankTransfer.Database;
using BankTransfer.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Services.Services;
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
        private readonly IRabbitMqService _rabbitMqService;

        public TransferController(ILogger<TransferController> logger, DataContext dataContext, IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _dataContext = dataContext;
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public IActionResult InsertTransfer([FromBody] TransferDTO transferDTO)
        {
            try
            {
                _rabbitMqService.sendMessage("transferQueue", transferDTO);
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
