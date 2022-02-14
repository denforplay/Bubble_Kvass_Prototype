using System;

namespace Core.Interfaces
{
    public interface IDictionaryRepository<TKey, TValue>
    {
        TValue Get(TKey key);
        void Add(TKey key, TValue value);
        event Action<TKey, TValue> OnValueChanged;
        void Refresh();
        void Save();
    }
}