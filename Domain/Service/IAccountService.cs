using Domain.dto.Account;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using RestEase;

namespace Domain.services
{
     public interface IAccountService
    {
        [Get("/api/Account")]
         Task<List<AccountDto>> GetAccounts();

        [Get("/api/Account/{accountNumber}")]
        Task<AccountDto> GetAccount([Path("accountNumber")] string accountNumber);

        [Post("/api/Account")]
        Task<HttpStatusCode> SetTransaction([Body]TransactionDto transaction);
    }
}
