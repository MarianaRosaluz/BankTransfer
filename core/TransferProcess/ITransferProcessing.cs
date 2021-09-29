using core.domain;
using System;
using System.Threading.Tasks;

namespace core.TransferProcess
{
    public interface ITransferProcessing
    {
        public  Task<bool> ProcessTransfer(Status status, Guid uuidTransfer, string message = null);
    }
}
