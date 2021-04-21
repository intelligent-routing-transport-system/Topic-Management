﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topic_Manager.Model;
using Topic_Manager.Model.DTOs;
using Topic_Manager.Repository.UnityOfWork;

namespace Topic_Manager.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SensorController : Controller
    {
        private readonly IUnityOfWork _uof;

        public SensorController(IUnityOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public async Task<List<Sensor>> GetSensors()
        { 
            return _uof.SensorRepository.Get().ToList();
        }

        [HttpPost]
        public async Task<ActionResult> CreateSensor([FromBody] SensorDTO sensor)
        {
            Sensor sensorDatabase = new Sensor()
            {
                Id = Guid.NewGuid().ToString(),
                TopicName = sensor.TopicName,
                Description = sensor.Description,
                Type = sensor.Type,
                Latitude = sensor.Latitude,
                Longitude = sensor.Longitude,
                ValueToCompare = sensor.ValueToCompare,
                CreateAt = DateTime.Now
            };

            _uof.SensorRepository.Add(sensorDatabase);
            await _uof.Commit();

            return Ok(sensorDatabase);
        }
    }
}