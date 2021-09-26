using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiBackgroundServices.dto
{
    class TransferDTO
    {
        public string accountOrigin { get; set; }
        public string accountDestination { get; set; }
        public double value { get; set; }
    }
}
