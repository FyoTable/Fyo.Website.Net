using Fyo.Interfaces;
using Fyo.Models;
using Fyo.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fyo.Services {
    public class SoftwareVersionService : BaseCrudService<SoftwareVersion>, ISoftwareVersionService {
        public SoftwareVersionService(DataContext dataContext) : base(dataContext){

        }
        
    }
}           