using System;
using System.Collections.Generic;
using Core.Enums;
using Core.Interfaces;
using UnityEngine;

namespace Data.Repositories.PlayerPrefsData
{
    public class MoneyPlayerPrefsRepository : IDictionaryRepository<MoneyType, int>
    {
        private Dictionary<MoneyType, int> _money;
        public event Action<MoneyType, int> OnValueChanged;

        public MoneyPlayerPrefsRepository()
        {
            _money = new Dictionary<MoneyType, int>();
            
            foreach (var value in Enum.GetValues(typeof(MoneyType)))
            {
                if (PlayerPrefs.HasKey(value.ToString()))
                    _money.Add((MoneyType)value, PlayerPrefs.GetInt(value.ToString()));
                else
                    _money.Add((MoneyType)value, 0);
            }
        }

        public int Get(MoneyType key) => _money[key];

        public void Add(MoneyType key, int value)
        {
            _money[key] += value;
            OnValueChanged?.Invoke(key, _money[key]);
            Save();
        }

        public void Refresh()
        {
            foreach (var pair in _money)
            {
                OnValueChanged?.Invoke(pair.Key, pair.Value);
            }
        }

        public void Save()
        {
            foreach (var pair in _money)
            {
                PlayerPrefs.SetInt(pair.Key.ToString(), pair.Value);
            }
        }
    }
}