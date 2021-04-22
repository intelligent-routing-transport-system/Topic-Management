using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topic_Manager.Model
{
    public class Payload
    {
        public class Coord
        {
            public double Latitude { get; set; }

            public double Longitude { get; set; }
        }

        public string SensorId { get; set; }
        public Coord Coords { get; set; }
        public double ActionRadius { get; set; }
        public double ValueToCompare { get; set; }
        public double Value { get; set; }
    }
}
