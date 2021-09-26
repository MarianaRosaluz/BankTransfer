using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiBackgroundServices.Services
{
    public class RabbitMqConfiguration
    {
        public string Host { get; set; }
        public string Queue { get; set; }
    }
}
