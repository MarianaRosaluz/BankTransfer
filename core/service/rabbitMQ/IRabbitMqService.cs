using System;


namespace core.service.rabbitMQ
{
     public interface IRabbitMqService
    {
        void sendMessage(string queue, object body);
        void CloseConnection();
        string receiveMessage(string queue, Action<ReadOnlyMemory<byte>> action);
        void ReQueue();
        void FinalizaQueue();
    }
}
