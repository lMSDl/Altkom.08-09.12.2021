using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IService<T> where T : Entity
    {
        IEnumerable<T> Read();
    }
}
