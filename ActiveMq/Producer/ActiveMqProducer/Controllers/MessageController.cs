using Microsoft.AspNetCore.Mvc;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace ActiveMqProducer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    public IActionResult Send(string message)
    {
        var factory = new ConnectionFactory("tcp://localhost:61616");
        using var connection = factory.CreateConnection("admin", "admin");
        using var session = connection.CreateSession();
        var destination = session.GetQueue("test-queue");
        using var producer = session.CreateProducer(destination);
        producer.DeliveryMode = MsgDeliveryMode.Persistent;

        var textMessage = session.CreateTextMessage(message);
        producer.Send(textMessage);

        return Ok("Message sent!");
    }
}
