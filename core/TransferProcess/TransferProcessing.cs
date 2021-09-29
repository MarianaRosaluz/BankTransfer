using core.domain;
using core.dto.convert;
using core.service.accountService;
using core.service.repositories;
using RestEase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace core.TransferProcess
{
    public class TransferProcessing: ITransferProcessing
    {
        ITransferRepository _transferRepository;
        CovertTransferToTransactionDTO _covertTransferToTransactionDTO;
        public TransferProcessing(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;

        }

        public async Task<bool> ProcessTransfer(Status status, Guid uuidTransfer, string message = null)
        {

            try
            {
                IAccountService _accountService = RestClient.For<IAccountService>("http://localhost:5550");
                var transfer = _transferRepository.GetTransferbyUUID(uuidTransfer);

                var responseGETlIST = await _accountService.GetAccounts();

                if (!responseGETlIST.Where(x => x.accountNumber == transfer.accountOrigin).Any())
                {
                    _transferRepository.ChangeStatus(Status.Error, uuidTransfer, "A conta de Origem é invalida");
                    return true;
                }
                else if (!responseGETlIST.Where(x => x.accountNumber == transfer.accountDestination).Any())
                {
                    _transferRepository.ChangeStatus(Status.Error, uuidTransfer, "A conta de destino é invalida");
                    return true;
                }
                var account = responseGETlIST.Where(x => x.accountNumber == transfer.accountOrigin).FirstOrDefault();
                if (account.balance - transfer.value < 0)
                {
                    _transferRepository.ChangeStatus(Status.Error, uuidTransfer, "A Conta de Origem não tem saldo o Sulficiente");
                    return true;
                }



                CovertTransferToTransactionDTO _covertTransferToTransactionDTO = new CovertTransferToTransactionDTO();
                var transactions = _covertTransferToTransactionDTO.CovertTransferToTransactionDTOList(transfer);
                foreach (var trans in transactions)
                {
                    try
                    {

                        var response = await _accountService.SetTransaction(trans);
                       

                    }
                    catch (ApiException ex)
                    {
                        return false;

                    }

                }
                _transferRepository.ChangeStatus(Status.Confirmed, uuidTransfer);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        
    }
}
