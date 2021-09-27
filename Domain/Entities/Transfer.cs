using Domain.Database.Enum;
using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Transfer
    {
        [Key]
        public long id { get; set; }
        public Guid uuid { get; set; }
        public string accountOrigin { get; set; }
        public string accountDestination { get; set; }
        public double value { get; set; }
        public Status status { get; set; }
        public string message { get; set; }

       
    }
}
