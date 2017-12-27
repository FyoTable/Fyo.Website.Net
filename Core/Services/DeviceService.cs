using Fyo.Interfaces;
using Fyo.Models;
using Fyo.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fyo.Services {
    public class DeviceService : BaseCrudService<Device>, IDeviceService {
        public DeviceService(DataContext dataContext) : base(dataContext){

        }
        public Device Get(string id) {
            return DbSet.FirstOrDefault(c => c.UniqueIdentifier.ToString() == id);
            
        }
    }
}           