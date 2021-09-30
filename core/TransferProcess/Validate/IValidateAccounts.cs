using core.domain;
using core.dto;
using core.service.repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace core.TransferProcess.Validate
{
    public interface IValidateAccounts
    {

        bool validate(List<AccountDto> listaContas, Transfer transferencia);
    }
}
