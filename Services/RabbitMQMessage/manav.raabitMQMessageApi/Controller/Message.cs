using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace manav.raabitMQMessageApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateMessage()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
        };

         var connection = await factory.CreateConnectionAsync();
         var channel = await connection.CreateChannelAsync();

         await channel.QueueDeclareAsync(
             queue: "Kuyruk1",
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null);

         var messageContent = "Kuyruk mesajıdır";
         var byteMessageContent = Encoding.UTF8.GetBytes(messageContent);

         await channel.BasicPublishAsync(
             exchange: "",
             routingKey: "Kuyruk1",
             mandatory: false,
             basicProperties: new BasicProperties(),
             body: byteMessageContent);
         return Ok();
    }
}