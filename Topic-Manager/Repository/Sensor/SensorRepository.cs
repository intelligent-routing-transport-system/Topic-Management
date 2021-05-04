using Topic_Manager.Context;
using Topic_Manager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topic_Manager.Repository.Sensor
{
    public class SensorRepository:Repository<Model.Sensor>,ISensorRepository
    {
        public SensorRepository(AppDbContext context): base(context)
        {

        }
    }
}
