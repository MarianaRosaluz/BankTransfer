using Domain.Database;
using Domain.Database.Enum;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Domain.TransferProcess
{
    public class TransferProcessing: ITransferProcessing
    {
        //private readonly ITransferRepository _transferRepository;
        IServiceScopeFactory  _serviceScopeFactory;


        public TransferProcessing(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        //public bool ChangeStatus(Status status, Guid uuidTransfer, string message = null)
        //{
        //    try
        //    {
        //        var transfer = _transferRepository.GetByUuidAsync(uuidTransfer);
        //        transfer.status = status;
        //        if (status == Status.Error)
        //            transfer.message = message;
        //       //_transferRepository.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}
        public bool ChangeStatus(Status status, Guid uuidTransfer, string message = null)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var transfer = context.transfers.Where(x => x.uuid == uuidTransfer).FirstOrDefault();
                    transfer.status = status;
                    if (status == Status.Error)
                        transfer.message = message;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
