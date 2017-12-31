using Fyo.Interfaces;
using Fyo.Models;
using Fyo.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fyo.Services {
    public class DeviceSoftwareVersionService : SimpleCrudService<DeviceSoftwareVersion>, IDeviceSoftwareVersionService {
        public DeviceSoftwareVersionService(DataContext dataContext) : base(dataContext){

        }
        
        public override DeviceSoftwareVersion Create(DeviceSoftwareVersion newEntity)
        {
            DbSet.Add(newEntity);
            _context.SaveChanges();

            return newEntity;
        }

        public override IQueryable<DeviceSoftwareVersion> GetAll(){
            return DbSet.Include(x => x.SoftwareVersion).Include(x => x.SoftwareVersion.Software);
        }

        public DbSet<DeviceSoftwareVersion> GetSet() {
            return DbSet;
        }
    }
}           