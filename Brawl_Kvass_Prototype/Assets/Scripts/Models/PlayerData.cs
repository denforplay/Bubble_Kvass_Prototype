using System;
using Configurations;
using UnityEngine;

namespace Models
{
    public class PlayerData
    {
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnGemsChanged;
        public event Action<string> OnNameChanged;

        private int _coins;
        private int _gems;
        private int _coinsForAllTime;
        private int _gemsForAllTime;
        private string _playerNickname;
        public int Coins => _coins;
        public int Gems => _gems;
        public int CoinsForAllTime => _coinsForAllTime;
        public int GemsForAllTime => _gemsForAllTime;
        public string PlayerNickname => _playerNickname;
        
        public PlayerData()
        {
            if (PlayerPrefs.HasKey(nameof(PlayerNickname)))
                _playerNickname = PlayerNickname;

            if (PlayerPrefs.HasKey(nameof(Coins)))
                _coins = PlayerPrefs.GetInt(nameof(Coins));
            if (PlayerPrefs.HasKey(nameof(Gems)))
                _gems = PlayerPrefs.GetInt(nameof(Gems));
            if (PlayerPrefs.HasKey(nameof(CoinsForAllTime)))
                _coinsForAllTime = PlayerPrefs.GetInt(nameof(CoinsForAllTime));
            if (PlayerPrefs.HasKey(nameof(GemsForAllTime)))
                _gemsForAllTime = PlayerPrefs.GetInt(nameof(GemsForAllTime));
        }

        public void Refresh()
        {
            OnMoneyChanged?.Invoke(_coins);
            OnGemsChanged?.Invoke(_gems);
        }

        public void ChangeMoney(int value)
        {
            _coins += value;
            if (value > 0)
                _coinsForAllTime += value;
            OnMoneyChanged?.Invoke(_coins);
            SaveData();
        }

        public void ChangeGems(int value)
        {
            _gems += value;
            if (value > 0)
                _gemsForAllTime += value;
            OnGemsChanged?.Invoke(_gems);
            SaveData();
        }

        public void SetName(string name)
        {
            _playerNickname = name;
            OnNameChanged?.Invoke(_playerNickname);
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt(nameof(Coins), _coins);
            PlayerPrefs.SetInt(nameof(Gems), _gems);
            PlayerPrefs.SetInt(nameof(CoinsForAllTime), _coinsForAllTime);
            PlayerPrefs.SetInt(nameof(GemsForAllTime), _gemsForAllTime);
            PlayerPrefs.Save();
        }
    }
}