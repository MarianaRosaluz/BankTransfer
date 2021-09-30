using core.service.repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace core.TransferProcess.Validate
{
    public class ValidateFactory : IValidateFactory
    {
        public ITransferRepository _transferRepository;
        Dictionary<string, IValidateAccounts> factory;
        public ValidateFactory(ITransferRepository transferRepository )
        {
            _transferRepository = transferRepository;

            factory = new Dictionary<string, IValidateAccounts>()  {
                { "ACCOUNT_ORIGIN", new ValidateAccountOrigin(_transferRepository) },
                { "ACCOUNT_DESTINATION", new ValidateAccountDestination(_transferRepository) }
            };
        }

        public IValidateAccounts get(String validateType)
        {
            return this.factory[validateType];
        }
}
}
