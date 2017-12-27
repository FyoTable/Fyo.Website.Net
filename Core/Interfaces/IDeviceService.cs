using Fyo.Models;

namespace Fyo.Interfaces {
    public interface IDeviceService: ICrudService<Device> {
        Device Get(string id);        
    }
}