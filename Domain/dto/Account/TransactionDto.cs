using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.dto.Account
{
    public class TransactionDto
    {
        public string accountNumber { get; set; }
        public double value { get; set; }
        public string Type { get; set; }

        public TransactionDto(string accountNumber, double value, string type)
        {
            this.accountNumber = accountNumber;
            this.value = value;
            this.Type = type;
        }
        
    }
}
