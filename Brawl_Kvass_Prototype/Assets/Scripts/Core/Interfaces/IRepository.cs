using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository<TValue>
    {
        TValue GetCurrent();
        void SetCurrent(int id);
        event Action<TValue> OnCurrentEntityChanged;
        void Add(TValue entity);
        void Refresh();
        TValue FindById(int id);
        IEnumerable<TValue> Get();
        void Save();
    }
}