using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Topic_Manager.Enum;

namespace Topic_Manager.Model
{
    [Table("Sensor")]
    public class Sensor
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        public SensorTypeEnum Type { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double ValueToCompare { get; set; }

        [Required]
        public string TopicName { get; set; }

        [Required]
        public double ActionRadius { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }

    }
}
