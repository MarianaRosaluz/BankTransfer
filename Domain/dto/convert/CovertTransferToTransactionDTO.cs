using core.domain;
using System.Collections.Generic;

namespace core.dto.convert
{
    class CovertTransferToTransactionDTO
    {
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
