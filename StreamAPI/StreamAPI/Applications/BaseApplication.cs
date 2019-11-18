using StreamAPI.Interfaces;
using System;

namespace StreamAPI.Applications
{
    public class BaseApplication<T> : IBaseRepository<T> where T : class
    {
        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
