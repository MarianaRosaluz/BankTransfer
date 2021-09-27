using Domain.Database.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.TransferProcess
{
    public interface ITransferProcessing
    {
        public bool ChangeStatus(Status status, Guid uuidTransfer, string message = null);
    }
}
