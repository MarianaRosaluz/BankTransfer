using BankTransfer.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransfer.Database
{
    public class Transfer
    {
        [Key]
        public long id { get; set; }
        public Guid uuid { get; set; }
        public string accountOrigin { get; set; }
        public string accountDestination { get; set; }
        public double value { get; set; }
        public string status { get; set; }
        public string message { get; set; }

       
    }
}
