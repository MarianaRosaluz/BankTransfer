using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.dto.Account
{
    public class AccountDto
    {
        public int id { get; set; }
        public string accountNumber { get; set; }
        public double balance { get; set; }

        public AccountDto(int id, string accountNumber, double balance)
        {
            this.id = id;
            this.accountNumber = accountNumber;
            this.balance = balance;
        }
    }
}
