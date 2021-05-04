using Topic_Manager.Context;
using System.Threading.Tasks;
using Topic_Manager.Repository.Sensor;

namespace Topic_Manager.Repository.UnityOfWork
{
    public class UnityOfWork: IUnityOfWork
    {
        private SensorRepository _SensorRepository;
        public AppDbContext _context;
        public UnityOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ISensorRepository SensorRepository
        {
            get
            {
                return _SensorRepository = _SensorRepository ?? new SensorRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
