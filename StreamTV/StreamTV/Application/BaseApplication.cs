using StreamTV.Interfaces;
using System;
using System.Collections.Generic;

namespace StreamTV.Application
{
    public class BaseApplication<T> : IBaseRepository<T> where T : class
    {
        public virtual string Delete(T t)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetAll(int amount)
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual string Insert(T t)
        {
            throw new NotImplementedException();
        }

        public virtual string Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}
