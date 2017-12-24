using System.Threading.Tasks;
using Fyo.Models;

namespace Fyo.Interfaces {
    public interface IUserService : ICrudService<User>
    {
        Task<User> GetByThirdPartyId(string thirdPartyUserId);
    }
}