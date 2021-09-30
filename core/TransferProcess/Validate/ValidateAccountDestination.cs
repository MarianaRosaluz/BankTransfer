using core.domain;
using core.dto;
using core.service.repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core.TransferProcess.Validate
{
    public class ValidateAccountDestination : IValidateAccounts
    {
        public ITransferRepository _transferRepository;

        public ValidateAccountDestination(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public bool validate(List<AccountDto> listaContas, Transfer transferencia)
        {
            bool result = false;
            if (!listaContas.Where(x => x.accountNumber == transferencia.accountDestination).Any())
            {
                this._transferRepository.ChangeStatus(Status.Error, transferencia.uuid, "A conta de destino é invalida");
                result = true;
            }
            return result;
        }
    }
}
