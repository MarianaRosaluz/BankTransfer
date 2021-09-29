using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace core.service.rabbitMQ
{
    public class RabbitMqService : IRabbitMqService
    {
        private IModel channel;
        private IConnection connection;
        private BasicDeliverEventArgs ea;

      
        public void CreateConnection()
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
        }

        public void CloseConnection()
        {
            this.channel.Close();
            this.connection.Close();
        }

        public void sendMessage(string queue, object menssage )
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(menssage);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "transferQueue",
                                     basicProperties: null,
                                     body: body);
            }
        }
        public void ReQueue()
        {
            this.channel.BasicNack(this.ea.DeliveryTag, false, true);
        }

        public void FinalizaQueue()
        {
            this.channel.BasicAck(this.ea.DeliveryTag, false);
            
        }
        public string receiveMessage(string queue, Action<ReadOnlyMemory<byte>> action)
        {
            string message = "";
            this.channel.BasicQos(0,1,true);
            this.channel.QueueDeclare(queue: queue,
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null
                                            );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    this.ea = ea;
                    action(ea.Body);

                }
                catch (Exception)
                {
                    //logger
                    this.ReQueue();
                }
                
            };
            this.channel.BasicConsume(queue: queue,
                                 autoAck: false,
                                 consumer: consumer);
         return message;
        }
    }

}


