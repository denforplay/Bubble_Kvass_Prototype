using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetCurrent();
        event Action<TEntity> OnCurrentEntityChanged;
        void Add(TEntity entity);
        void Refresh();
        void SetCurrent(int id);
        TEntity FindById(int id);
        IEnumerable<TEntity> Get();
        void Save();
    }
}