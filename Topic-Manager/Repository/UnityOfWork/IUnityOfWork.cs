using Topic_Manager.Repository.Sensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topic_Manager.Repository.UnityOfWork
{
    public interface IUnityOfWork
    {
        ISensorRepository SensorRepository { get; }
        Task Commit();
    }
}
