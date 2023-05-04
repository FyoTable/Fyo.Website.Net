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

        public SoftwareVersion[] SoftwareVersions(long id) {
            var device = Get(id);
            var deviceSoftwareVersionSet = _context.Set<DeviceSoftwareVersion>();

            var mappings = deviceSoftwareVersionSet.Where(dsv => dsv.DeviceId == id).Include(dsv => dsv.SoftwareVersion).Include(dsv => dsv.SoftwareVersion.Software);
            return mappings.Select(dsv => dsv.SoftwareVersion).ToArray();
        }
    }
}           