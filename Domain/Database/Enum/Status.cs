using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Database.Enum
{
    public enum Status
    {
        In_Queue,
        Processing,
        Confirmed,
        Error
    }
}
