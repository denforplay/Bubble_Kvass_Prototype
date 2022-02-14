using Configurations;
using Configurations.Info;
using Core.Enums;
using Core.Interfaces;
using Data.Repositories.PlayerPrefsData;
using Models;
using UnityEngine;

namespace Data
{
    public class PlayerDataProvider
    {
        private readonly IRepository<BackgroundInfo> _backgroundsRepository;
        private readonly IRepository<PlayerIconInfo> _playerIconsRepository;
        private readonly IRepository<FighterInfo> _fighterInfoRepository;
        private readonly IDictionaryRepository<MoneyType, int> _moneyRepository;
        private readonly PlayerData _playerData;
        public PlayerData PlayerData => _playerData;
        
        public IRepository<BackgroundInfo> BackgroundsRepository => _backgroundsRepository;
        public IRepository<PlayerIconInfo> PlayerIconsRepository => _playerIconsRepository;
        public IRepository<FighterInfo> FightersRepository => _fighterInfoRepository;
        public IDictionaryRepository<MoneyType, int> MoneyRepository => _moneyRepository;
        public PlayerDataProvider(MainMenuBackgroundConfiguration backgroundConfiguration,
            PlayerIconsConfiguration playerIconsConfiguration,
            FightersConfiguration fightersConfiguration)
        {
            _backgroundsRepository = new BackgroundsPlayerPrefsRepository(backgroundConfiguration, "background");
            _playerIconsRepository = new PlayerIconsRepository(playerIconsConfiguration, "icon");
            _fighterInfoRepository = new FighterPlayerPrefsRepository(fightersConfiguration, "fighter");
            _moneyRepository = new MoneyPlayerPrefsRepository();
            _playerData = new PlayerData();
        }

        public void Refresh()
        {
            _backgroundsRepository.Refresh();
            _playerIconsRepository.Refresh();
            _fighterInfoRepository.Refresh();
            _moneyRepository.Refresh();
        }

        public void Save()
        {
            _backgroundsRepository.Save();
            _playerIconsRepository.Save();
            _fighterInfoRepository.Save();
            _playerData.SaveData();
            _moneyRepository.Save();
        }
    }
}