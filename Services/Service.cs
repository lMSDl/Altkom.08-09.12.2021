using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Service<T> : IService<T> where T : Entity
    {
        private ICollection<T> _entities;

        public Service(ICollection<T> entities)
        {
            _entities = entities;
        }

        public Service() : this(new List<T>())
        {
        }

        public IEnumerable<T> Read()
        {
            return _entities.ToList();
        }
    }
}
