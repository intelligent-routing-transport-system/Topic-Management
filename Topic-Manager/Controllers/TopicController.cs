﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Topic_Manager.Kafka;
using Topic_Manager.Model;
using System.Text.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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
            try
            {
                var messageSend = "";
                string kafkaConnectionString = _configuration.GetConnectionString("KafkaConnection");

                byte[] messages = Encoding.ASCII.GetBytes(message.Payload);

                using (var producer = new ProducerMessage<byte[]>(messages, message.TopicName, kafkaConnectionString))
                {
                    messageSend = await producer.Run();
                    Debug.WriteLine(messageSend);
                }
                return Ok(message.Payload);
            }
            catch (System.Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("helthcheck");
        }
    }
}
