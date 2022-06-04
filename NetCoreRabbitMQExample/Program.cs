
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace NetCoreRabbitMQExample
{
    internal class Program
    {
        /// <summary>
        /// Queue write
        /// </summary>
        /// <param name="args"></param>
        //static void Main(string[] args)
        //{
        //    Customer customer = new Customer { Name = "ibrahim", Surname = "ozen" };
        //    var factory = new ConnectionFactory() { HostName = "localhost" };
        //    using (IConnection connection = factory.CreateConnection())
        //    using (IModel channel = connection.CreateModel())
        //    {
        //        // QueueDeclare methodu ile yeni bir queue tanımlanıyor.
        //        channel.QueueDeclare(queue: "ibrahim",
        //                             durable: false, // queue'nın ömrü belirleniyor.
        //                             exclusive: false, // queue farklı connection'lar ile kullanılabilir mi?
        //                             autoDelete: false, // consumer'a ulaştıktan sonra silinsin mi?
        //                             arguments: null);
        //        string message = JsonConvert.SerializeObject(customer);
        //        var body = Encoding.UTF8.GetBytes(message);// byte şeklinde gönderiliyor.

        //        channel.BasicPublish(exchange: "",
        //                             routingKey: "ibrahim", // consumer tarafından da kullanılacak. önemli !!!
        //                             basicProperties: null,
        //                             body: body);
        //    }
        //    Console.ReadLine();
        //}

        /// <summary>
        /// Queue read
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (channel, ea) => // ea => EventArg
                {
                    var body = ea.Body.ToArray();
                    var jsonString = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Json received  as {jsonString}");
                };
                // BasicConsume methodu hangi queue'ya abone olundugu bilgisini tutar.
                channel.BasicConsume(queue: "ibrahim",
                                     autoAck:true, // true olarak ayarlanirsa consumer mesaji aldiktan sonra otomatik                            olarak alindi onayi verilir
                                     consumer: consumer);
            }
            Console.ReadLine();
        }




    }
    public class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
