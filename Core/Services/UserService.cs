using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fyo.Interfaces;
using Fyo.Models;

namespace Fyo.Services {
    public class UserService : BaseCrudService<User>, IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context): base(context){
            _context = context;
        }

        public override User Create(User newEntity)
        {
            //throw new NotImplementedException();

            return base.Create(newEntity);
        }

        public override void Delete(User deletedEntity)
        {
            //throw new NotImplementedException();

            base.Delete(deletedEntity);
        }

        public async Task<User> GetByThirdPartyId(string thirdPartyUserId)
        {
            Console.WriteLine("ThirdPartyId: ", thirdPartyUserId);
            return await _context.Users.FirstOrDefaultAsync(u => u.ThirdPartyUserId == thirdPartyUserId);
        }
    }
}