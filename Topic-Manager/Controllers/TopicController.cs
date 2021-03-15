using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Topic_Manager.Kafka;
using Topic_Manager.Model;
using System.Text.Json;

namespace Topic_Manager.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TopicController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMessage([FromBody] MessageBody message)
        {
            var messageSend = "";
            string kafkaConnectionString = _configuration.GetConnectionString("KafkaConnection");
            using (var producer = new ProducerMessage<string>(message.Payload, message.TopicName, kafkaConnectionString))
            {
                messageSend = await producer.Run();
                Debug.WriteLine(messageSend);
            }
            return Ok(messageSend);
        }
    }
}
