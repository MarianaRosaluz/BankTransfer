using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
     public interface IRabbitMqService
    {
        void sendMessage(string queue, object body);
        //void CreateConnection();
        void CloseConnection();
        string receiveMessage(string queue, Action<ReadOnlyMemory<byte>> action);
        void ReQueue();
        void FinalizaQueue();
    }
}
