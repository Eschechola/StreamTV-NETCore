using System;
using System.Collections.Generic;

namespace StreamTV.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        string Insert(T t);
        string Update(T t);
        string Delete(T t);
        T GetById(int id);
        List<T> GetAll(int amount);
    }
}
