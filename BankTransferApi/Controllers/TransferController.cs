using BankTransferApi.dto;
using core.domain;
using core.service.rabbitMQ;
using core.service.repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace BankTransferApi.Controllers
{

    [Route("api/fund-transfer/")]
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
            Transfer transfer = new Transfer();
            try
            {
                 transfer = transferDTO.ConvertTransferDtoToTransfer();
                _dataContext.Add(transfer);
                _dataContext.SaveChanges();
                _rabbitMqService.sendMessage("transferQueue", transfer);
                
                return Accepted(new { transactionId = transfer.uuid });


            }
            catch (Exception ex)
            {
                _dataContext.Remove(transfer);
                _logger.LogError("Erro ao tentar criar uma nova transferencia", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
            }
        }
        
        [HttpGet]
        [Route("{transactionId}")]
        public IActionResult GetStatusTransfer(Guid transactionId)
        {
            try
            {

                var transfer = _dataContext.transfers.Where(x => x.uuid == transactionId).FirstOrDefault();

                if (transfer.status == Status.Error)
                {
                    return Ok(new {Status = Enum.GetName(typeof(Status), transfer.status), Message = transfer.message});
                }
                return Ok(new { Status = Enum.GetName(typeof(Status), transfer.status)});
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar Buscar o status de uma transferencia", ex);
                return new StatusCodeResult(500);
            }
        }
    }
}
