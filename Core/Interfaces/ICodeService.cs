using Fyo.Models;

namespace Fyo.Interfaces {
    public interface ICodeService: ICrudService<Code> {
        Code ByCode(string digits);
    }
}