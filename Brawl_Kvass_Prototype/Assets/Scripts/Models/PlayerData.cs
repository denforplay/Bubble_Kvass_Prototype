using System;
using System.Linq;
using Configurations;
using Configurations.Info;
using UnityEngine;

namespace Models
{
    public class PlayerData
    {
        public event Action<BackgroundInfo> OnBackgroundChanged;
        public event Action<FighterInfo> OnFighterChanged;
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnGemsChanged;
        private readonly MainMenuBackgroundConfiguration _backgroundsConfiguration;
        private readonly FightersConfiguration _fightersConfiguration;
        private BackgroundInfo _currentBackground;
        private FighterInfo _currentFighter;
        private int _coins;
        private int _gems;
        private int _coinsForAllTime;
        private int _gemsForAllTime;
        public BackgroundInfo CurrentBackground => _currentBackground;
        public FighterInfo CurrentFighter => _currentFighter;
        public int Coins => _coins;
        public int Gems => _gems;
        public int CoinsForAllTime => _coinsForAllTime;
        public int GemsForAllTime => _gemsForAllTime;

        public PlayerData(MainMenuBackgroundConfiguration backgroundConfiguration,
            FightersConfiguration fightersConfiguration)
        {
            _backgroundsConfiguration = backgroundConfiguration;
            _fightersConfiguration = fightersConfiguration;
            if (PlayerPrefs.HasKey(nameof(CurrentBackground)))
            {
                int id = PlayerPrefs.GetInt(nameof(CurrentBackground));
                _currentBackground = _backgroundsConfiguration.Backgrounds.Find(x => x.Id == id);
            }
            else
                _currentBackground = _backgroundsConfiguration.Backgrounds.First(x => x.Cost == 0);

            if (PlayerPrefs.HasKey(nameof(CurrentFighter)))
            {
                int id = PlayerPrefs.GetInt(nameof(CurrentFighter));
                _currentFighter = _fightersConfiguration.FighterInfos.Find(x => x.Id == id);
            }
            else
                _currentFighter = _fightersConfiguration.FighterInfos.First();

            if (PlayerPrefs.HasKey(nameof(Coins)))
                _coins = PlayerPrefs.GetInt(nameof(Coins));
            if (PlayerPrefs.HasKey(nameof(Gems)))
                _gems = PlayerPrefs.GetInt(nameof(Gems));
            if (PlayerPrefs.HasKey(nameof(CoinsForAllTime)))
                _coinsForAllTime = PlayerPrefs.GetInt(nameof(CoinsForAllTime));
            if (PlayerPrefs.HasKey(nameof(GemsForAllTime)))
                _gemsForAllTime = PlayerPrefs.GetInt(nameof(GemsForAllTime));
            Debug.Log("Loaded");
            Debug.Log(_coins);
        }

        public void Refresh()
        {
            if (_currentBackground != null)
                OnBackgroundChanged?.Invoke(_currentBackground);
            if (_currentFighter != null)
                OnFighterChanged?.Invoke(_currentFighter);
            
            OnMoneyChanged?.Invoke(_coins);
            OnGemsChanged?.Invoke(_gems);
        }

        public void SetBackground(BackgroundInfo backgroundInfo)
        {
            _currentBackground = backgroundInfo;
            OnBackgroundChanged?.Invoke(_currentBackground);
        }

        public void SetFighter(FighterInfo fighterInfo)
        {
            _currentFighter = fighterInfo;
            OnFighterChanged?.Invoke(_currentFighter);
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

        public void SaveData()
        {
            if (CurrentBackground != null)
                PlayerPrefs.SetInt(nameof(CurrentBackground), _currentBackground.Id);
            if (_currentFighter != null)
                PlayerPrefs.SetInt(nameof(CurrentFighter), _currentFighter.Id);
            PlayerPrefs.SetInt(nameof(Coins), _coins);
            PlayerPrefs.SetInt(nameof(Gems), _gems);
            PlayerPrefs.SetInt(nameof(CoinsForAllTime), _coinsForAllTime);
            PlayerPrefs.SetInt(nameof(GemsForAllTime), _gemsForAllTime);
            PlayerPrefs.Save();
        }
    }
}