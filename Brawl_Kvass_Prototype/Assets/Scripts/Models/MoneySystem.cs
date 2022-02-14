using System;
using System.Collections.Generic;
using Core.Enums;

namespace Models
{
    public class MoneySystem
    {
        public event Action<MoneyType, int> OnValueChanged;

        private Dictionary<MoneyType, int> _money;

        public Dictionary<MoneyType, int> Money => _money;

        public MoneySystem()
        {
            Restart();
        }
        
        public void Restart()
        {
            _money = new Dictionary<MoneyType, int>();
            foreach (var value in Enum.GetValues(typeof(MoneyType)))
            {
                _money.Add((MoneyType)value, 0);
                OnValueChanged?.Invoke((MoneyType)value, 0);
            }
            
        }

        public void ChangeMoney(MoneyType moneyType, int value)
        {
            _money[moneyType] += value;
            OnValueChanged?.Invoke(moneyType, _money[moneyType]);
        }
        
        public void SetMoney(MoneyType moneyType, int value)
        {
            _money[moneyType] = value;
            OnValueChanged?.Invoke(moneyType, _money[moneyType]);
        }
    }
}