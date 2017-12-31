using Fyo.Interfaces;
using Fyo.Models;
using Fyo.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fyo.Services {
    public class SoftwareService : BaseCrudService<Software>, ISoftwareService {
        public SoftwareService(DataContext dataContext) : base(dataContext){

        }
        
    }
}           