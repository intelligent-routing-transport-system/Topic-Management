using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Topic_Manager.Kafka;
using Topic_Manager.Model;
using System.Text.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System;
using Topic_Manager.Repository.UnityOfWork;
using Topic_Manager.Model.DTOs;

namespace Topic_Manager.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUnityOfWork _uof;

        public TopicController(IConfiguration configuration, IUnityOfWork uof)
        {
            _configuration = configuration;
            _uof = uof;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMessage([FromBody] PayloadDTO payloadDTO)
        {
            try
            {
                string kafkaConnectionString = _configuration.GetConnectionString("KafkaConnection");

                var sensorDatabase = _uof.SensorRepository.GetById(x => x.Id == payloadDTO.SensorId).Result;

                Payload payload = new Payload()
                {
                    Coords = new Payload.Coord { Latitude = sensorDatabase.Latitude, Longitude = sensorDatabase.Longitude },
                    SensorId = sensorDatabase.Id,
                    ValueToCompare = sensorDatabase.ValueToCompare,
                    Value = payloadDTO.Value,
                };

                byte[] messages = JsonSerializer.SerializeToUtf8Bytes(payload);

                using (var producer = new ProducerMessage<byte[]>(messages, sensorDatabase.TopicName, kafkaConnectionString))
                {
                    var messageInformations = await producer.Run();
                    Debug.WriteLine(messageInformations);
                }
                return Ok(payload);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("helthcheck: " + DateTime.Now.ToString());
        }
    }
}
