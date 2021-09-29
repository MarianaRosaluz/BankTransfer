using core.domain;
using System;

namespace core.service.repositories
{
    public interface ITransferRepository
    {
        public Transfer GetTransferbyUUID(Guid uuidTransfer);
        public void ChangeStatus(Status status, Guid uuidTransfer, string message = null);

    }
}
