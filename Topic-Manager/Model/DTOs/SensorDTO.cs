using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topic_Manager.Enum;

namespace Topic_Manager.Model.DTOs
{
    public class SensorDTO
    {
        public SensorTypeEnum Type { get; set; }

        public string Description { get; set; }

        public string TopicName { get; set; }

        public double ValueToCompare { get; set; }

        public double ActionRadius { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
