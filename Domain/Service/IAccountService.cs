using Domain.dto.Account;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using RestEase;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Domain.services
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
