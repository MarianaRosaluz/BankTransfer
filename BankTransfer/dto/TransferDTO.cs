using BankTransfer.Database;
using System;

namespace BankTransfer.dto
{
    public class TransferDTO
    {
        public string accountOrigin { get; set; }
        public string accountDestination { get; set; }
        public double value {get;  set;}

        public Transfer ConvertTransferDtoToTransfer()
        {
            Transfer transfer = new Transfer();
            transfer.accountOrigin = this.accountOrigin;
            transfer.accountDestination = this.accountDestination;
            transfer.value = this.value;
            transfer.status = "In Queue";
            transfer.uuid = Guid.NewGuid();

            return transfer;
        }
    }
}
