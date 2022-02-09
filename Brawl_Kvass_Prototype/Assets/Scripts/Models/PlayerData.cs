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
        
        private readonly MainMenuBackgroundConfiguration _backgroundsConfiguration;
        private readonly FightersConfiguration _fightersConfiguration;
        private BackgroundInfo _currentBackground;
        private FighterInfo _currentFighter;

        public BackgroundInfo CurrentBackground => _currentBackground;
        public FighterInfo CurrentFighter => _currentFighter;

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
        }

        public void Refresh()
        {
            if (_currentBackground != null)
                OnBackgroundChanged?.Invoke(_currentBackground);
            if (_currentFighter != null)
                OnFighterChanged?.Invoke(_currentFighter);
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

        public void SaveData()
        {
            if (CurrentBackground != null)
                PlayerPrefs.SetInt(nameof(CurrentBackground), _currentBackground.Id);
            if (_currentFighter != null)
                PlayerPrefs.SetInt(nameof(CurrentFighter), _currentFighter.Id);
            PlayerPrefs.Save();
        }
    }
}