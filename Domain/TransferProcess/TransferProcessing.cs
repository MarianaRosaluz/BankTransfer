using Domain.Database;
using Domain.Database.Enum;
using Domain.dto.Account;
using Domain.Entities;
using Domain.Repository;
using Domain.services;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.TransferProcess
{
    public class TransferProcessing: ITransferProcessing
    {
        IServiceScopeFactory _serviceScopeFactory;

        public TransferProcessing(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

        }

        public async Task<bool> ChangeStatus(Status status, Guid uuidTransfer, string message = null)
        {
            try
            {
                TransferRepository _transferRepository = new TransferRepository(_serviceScopeFactory);
                //verificar se conta existe 
                _transferRepository.ChangeStatus(status, uuidTransfer,message);
                     var transfer = _transferRepository.GetTransferbyUUID(uuidTransfer);
                      var transactions = CovertTransferToTransactionDTOList(transfer);
                
                
                foreach (var trans in transactions)
                {
                    try {
                        IAccountService _accountService = RestClient.For<IAccountService>("http://localhost:5550");
                        var response = await _accountService.SetTransaction(trans); 
                        
                    } catch (Exception ex) {
                    }
                    
                }
               return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<TransactionDto> CovertTransferToTransactionDTOList(Transfer transfer)
        {
            List<TransactionDto> transactions = new List<TransactionDto>();
            TransactionDto transactionDebit = new TransactionDto(transfer.accountOrigin, transfer.value, "Debit");
            transactions.Add(transactionDebit);
            TransactionDto transactionCredit = new TransactionDto(transfer.accountDestination, transfer.value, "Credit");
            transactions.Add(transactionCredit);

            return transactions;



        }
    }
}
