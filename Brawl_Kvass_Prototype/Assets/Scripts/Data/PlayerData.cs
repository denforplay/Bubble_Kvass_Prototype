using System;
using Core.Enums;
using Models;
using UnityEngine;

namespace Data
{
    public class PlayerData
    {
        private const string _key = "best";
        public event Action<string> OnNameChanged;
        private MoneySystem _bestMoney;
        private string _playerNickname;
        public string PlayerNickname => _playerNickname;
        public MoneySystem BestMoney => _bestMoney;
        
        public PlayerData()
        {
            _bestMoney = new MoneySystem();
            if (PlayerPrefs.HasKey(nameof(PlayerNickname)))
                _playerNickname = PlayerPrefs.GetString(nameof(PlayerNickname));

            foreach (var value in Enum.GetValues(typeof(MoneyType)))
            {
                if (PlayerPrefs.HasKey(_key + value))
                    _bestMoney.ChangeMoney((MoneyType)value, PlayerPrefs.GetInt(_key + value));
                else
                    _bestMoney.ChangeMoney((MoneyType)value, 0);
            }
        }

        public void TrySetBestMoney(MoneyType moneyType, int newMoney)
        {
            if (newMoney > _bestMoney.Money[moneyType])
            {
                _bestMoney.SetMoney(moneyType, newMoney);
                SaveData();
            }
        }

        public void SetName(string name)
        {
            _playerNickname = name;
            OnNameChanged?.Invoke(_playerNickname);
        }

        public void SaveData()
        {
            foreach (var pair in _bestMoney.Money)
            {
                PlayerPrefs.SetInt(_key + pair.Key, pair.Value);
            }

            PlayerPrefs.Save();
        }
    }
}