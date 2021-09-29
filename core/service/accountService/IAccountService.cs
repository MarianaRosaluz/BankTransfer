using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Microsoft.AspNetCore.Http;
using core.dto;

namespace core.service.accountService
{
     public interface IAccountService
    {
        [Get("/api/Account")]
         Task<List<AccountDto>> GetAccounts();

        [Get("/api/Account/{accountNumber}")]
        Task<Response<AccountDto>> GetAccount([Path("accountNumber")] string accountNumber);

        [Post("/api/Account")]
        Task<HttpContext> SetTransaction([Body]TransactionDto transaction);
    }
}
