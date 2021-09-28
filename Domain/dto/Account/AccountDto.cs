using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.dto.Account
{
    public class AccountDto
    {
        private int id { get; set; }
        private string accountNumber { get; set; }
        private double balance { get; set; }

        public AccountDto(int id, string accountNumber, double balance)
        {
            this.id = id;
            this.accountNumber = accountNumber;
            this.balance = balance;
        }
    }
}
