﻿using Domain.Database.Enum;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Repository
{
    public class TransferRepository :ITransferRepository
    {
        IServiceScopeFactory _serviceScopeFactory;

        public TransferRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

        }

        public Transfer GetTransferbyUUID(Guid uuidTransfer){
          
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var transfer = context.transfers.Where(x => x.uuid == uuidTransfer).FirstOrDefault();

                    return transfer;
                
                }                

           
        }

        public void ChangeStatus(Status status, Guid uuidTransfer, string message = null)
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
                }
                   
            }
            catch (Exception ex)
            {
            }

        }
        

    }
}
