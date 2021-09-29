using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.domain
{
    public enum Status
    {
        In_Queue,
        Processing,
        Confirmed,
        Error
    }
}
