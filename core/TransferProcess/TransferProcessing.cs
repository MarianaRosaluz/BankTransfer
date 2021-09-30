using core.domain;
using core.dto.convert;
using core.service.accountService;
using core.service.repositories;
using core.TransferProcess.Validate;
using Microsoft.Extensions.Logging;
using RestEase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace core.TransferProcess
{
    public class TransferProcessing: ITransferProcessing
    {
        ITransferRepository _transferRepository;
        IAccountService _accountService;
        private ILogger<TransferProcessing> _logger;

        public TransferProcessing(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
            _accountService = RestClient.For<IAccountService>("http://localhost:5550");
        }

        public async Task<bool> ProcessTransfer(Status status, Guid uuidTransfer, string message = null)
        {

            try
            {
              
                var transfer = _transferRepository.GetTransferbyUUID(uuidTransfer);

                var responseGETlIST = await _accountService.GetAccounts();

                IValidateFactory factory = new ValidateFactory(_transferRepository);
                if (factory.get("ACCOUNT_ORIGIN").validate(responseGETlIST, transfer) ||
                    factory.get("ACCOUNT_DESTINATION").validate(responseGETlIST, transfer))
                    return true;


                CovertTransferToTransactionDTO _covertTransferToTransactionDTO = new CovertTransferToTransactionDTO();
                var transactions = _covertTransferToTransactionDTO.CovertTransferToTransactionDTOList(transfer);

                if (transactions.Count == 0)
                    return false;

                foreach (var trans in transactions)
                {
                    try
                    {
                      var response = await _accountService.SetTransaction(trans);
                       
                    }
                    catch (ApiException ex)
                    {
                        _logger.LogError("Erro ao fazer a transferencia", ex);
                        return false;

                    }

                }
                _transferRepository.ChangeStatus(Status.Confirmed, uuidTransfer);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro no processamento da mensagem", ex);
                return false;
            }

        }
        
    }
}
