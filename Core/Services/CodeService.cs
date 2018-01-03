using Fyo.Interfaces;
using Fyo.Models;
using Fyo.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fyo.Services {
    public class CodeService : BaseCrudService<Code>, ICodeService {
        public CodeService(DataContext dataContext) : base(dataContext){

        }
        public Code ByCode(string digits) {
            return DbSet.FirstOrDefault(x => x.Digits == digits);
        }
    }
}           