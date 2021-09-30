using core.domain;
using core.dto;
using core.service.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core.TransferProcess.Validate
{
    public class ValidateAccountOrigin : IValidateAccounts
    {
        public ITransferRepository _transferRepository;
        public bool _result;

        public ValidateAccountOrigin(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
            _result = false;
        }

        public bool validate(List<AccountDto> listaContas, Transfer transferencia)
        {
            this.checkAccount(listaContas, transferencia);

            if(!this._result)
            this.validateAccountBalance(listaContas, transferencia);

            return this._result;
        }

        private void checkAccount(List<AccountDto> listaContas, Transfer transferencia)
        {
            if (!listaContas.Where(x => x.accountNumber == transferencia.accountOrigin).Any())
            {
                this._transferRepository.ChangeStatus(Status.Error, transferencia.uuid, "A conta de Origem é invalida");
                this._result = true;
            }
        }

        private void validateAccountBalance(List<AccountDto> listaContas, Transfer transferencia)
        {
            var account = listaContas.Where(x => x.accountNumber == transferencia.accountOrigin).FirstOrDefault();
            if (account.balance - transferencia.value < 0)
            {
                this._transferRepository.ChangeStatus(Status.Error, transferencia.uuid, "A Conta de Origem não tem saldo o Sulficiente");
                this._result = true;
            }
        }
    }
}
