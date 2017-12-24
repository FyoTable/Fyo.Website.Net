using Fyo.Interfaces;
using Fyo.Models;
using Fyo.Services;

namespace Fyo.Services {
    public class DeviceService : BaseCrudService<Device>, IDeviceService {
        public DeviceService(DataContext dataContext) : base(dataContext){

        }
    }
}           