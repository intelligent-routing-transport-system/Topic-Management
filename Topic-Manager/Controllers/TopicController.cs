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
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                Console.WriteLine("Final Version");
                string kafkaConnectionString = _configuration.GetConnectionString("KafkaConnection");

                Console.WriteLine("### Start get sensor by id in Database - " + DateTime.Now.ToString() + "###");
                var sensorDatabase = await _uof.SensorRepository.Get().Where(x => x.Id == payloadDTO.SensorId).FirstOrDefaultAsync();
                Console.WriteLine("### Finish get sensor by id in Database - " + DateTime.Now.ToString() + "###");

                if (sensorDatabase == null)
                {
                    Console.WriteLine("Sensor not found");
                    return Ok("Sensor not found");
                }

                Payload payload = new Payload()
                {
                    Coords = new Payload.Coord { Latitude = sensorDatabase.Latitude, Longitude = sensorDatabase.Longitude },
                    SensorId = sensorDatabase.Id,
                    ActionRadius = sensorDatabase.ActionRadius,
                    ValueToCompare = sensorDatabase.ValueToCompare,
                    Value = payloadDTO.Value,
                };

                byte[] messages = JsonSerializer.SerializeToUtf8Bytes(payload);

                Console.WriteLine("### Start producer message for Kafka - " + DateTime.Now.ToString() + "###");
                using (var producer = new ProducerMessage<byte[]>(messages, sensorDatabase.TopicName, kafkaConnectionString))
                {
                    var messageInformations = await producer.Run();
                    Console.WriteLine($"### message send: {messageInformations} - " + DateTime.Now.ToString() + "###");
                    producer.Dispose();
                }
                Console.WriteLine("### Finish producer message for Kafka - " + DateTime.Now.ToString() + "###");
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
