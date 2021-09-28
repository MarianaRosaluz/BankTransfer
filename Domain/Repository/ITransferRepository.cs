using Domain.Database.Enum;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repository
{
    public interface ITransferRepository
    {
        public Transfer GetTransferbyUUID(Guid uuidTransfer);
        public void ChangeStatus(Status status, Guid uuidTransfer, string message = null);

    }
}
