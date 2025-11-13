using System.Collections.Generic;

namespace Domain.Abstraction;
    
public interface IRepositoryBase<T> where T : class
{
    List<T> GetAll();
    T GetById(int id);
    bool Create(T entity);
    bool Update(T entity);
    bool Delete(int id);
}
