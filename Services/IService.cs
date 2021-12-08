using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IService<T> where T : Entity
    {
        IEnumerable<T> Read();
        T Read(int id);
        void Delete(int id);
        void Update(int id, T entity);
    }
}
