using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.dto.Account
{
    public class TransactionDto
    {
        private string accountNumber { get; set; }
        private double value { get; set; }
        private string Type { get; set; }

        public TransactionDto(string accountNumber, double value, string type)
        {
            this.accountNumber = accountNumber;
            this.value = value;
            this.Type = type;
        }
        
    }
}
