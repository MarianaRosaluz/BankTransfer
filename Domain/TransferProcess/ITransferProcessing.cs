using Domain.Database.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TransferProcess
{
    public interface ITransferProcessing
    {
        public  Task<bool> ChangeStatus(Status status, Guid uuidTransfer, string message = null);
    }
}
