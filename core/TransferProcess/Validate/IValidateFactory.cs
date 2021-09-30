using System;
using System.Collections.Generic;
using System.Text;

namespace core.TransferProcess.Validate
{
    public interface IValidateFactory
    {
        IValidateAccounts get(String validateType);
    }
}
