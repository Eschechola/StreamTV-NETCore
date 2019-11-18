using System;

namespace StreamAPI.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {

    }
}
