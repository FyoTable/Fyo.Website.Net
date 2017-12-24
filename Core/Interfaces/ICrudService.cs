using System.Linq;
using Fyo.Models;

namespace Fyo.Interfaces {
    public interface ICrudService<T> {
        T Get(long id);
        IQueryable<T> GetAll();
        T Create(T newEntity);
        T Update(T updatedEntity);
        void Delete(T deletedEntity);
    }
}