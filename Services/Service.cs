﻿using Models;
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

        public void Delete(int id)
        {
            _entities.Remove(Read(id));
        }

        public T Read(int id)
        {
            return _entities.SingleOrDefault(x => x.Id == id);
        }

        public void Update(int id, T entity)
        {
            var item = Read(id);
            if (item == null)
                return;
            _entities.Remove(item);
            entity.Id = id;
            _entities.Add(entity);
        }

        public int Create(T entity)
        {
            entity.Id = !_entities.Any() ? 1 : _entities.Max(x => x.Id) + 1;
            _entities.Add(entity);

            return entity.Id;
        }
    }
}
