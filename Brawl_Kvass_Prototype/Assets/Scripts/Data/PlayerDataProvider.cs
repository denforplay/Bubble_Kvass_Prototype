using Configurations;
using Configurations.Info;
using Core.Interfaces;
using Data.Repositories;
using Models;

namespace Data
{
    public class PlayerDataProvider
    {
        private MainMenuBackgroundConfiguration _backgroundConfiguration;
        private PlayerIconsConfiguration _playerIconsConfiguration;
        private FightersConfiguration _fightersConfiguration;
        private IRepository<BackgroundInfo> _backgroundsRepository;
        private IRepository<PlayerIconInfo> _playerIconsRepository;
        private IRepository<FighterInfo> _fighterInfoRepository;
        private PlayerData _playerData;
        public PlayerData PlayerData => _playerData;
        
        public IRepository<BackgroundInfo> BackgroundsRepository => _backgroundsRepository;
        public IRepository<PlayerIconInfo> PlayerIconsRepository => _playerIconsRepository;
        public IRepository<FighterInfo> FightersRepository => _fighterInfoRepository;

        public PlayerDataProvider(MainMenuBackgroundConfiguration backgroundConfiguration,
            PlayerIconsConfiguration playerIconsConfiguration,
            FightersConfiguration fightersConfiguration)
        {
            _backgroundConfiguration = backgroundConfiguration;
            _playerIconsConfiguration = playerIconsConfiguration;
            _fightersConfiguration = fightersConfiguration;
            _backgroundsRepository = new BackgroundsPlayerPrefsRepository(backgroundConfiguration, "background");
            _playerIconsRepository = new PlayerIconsRepository(playerIconsConfiguration, "icon");
            _fighterInfoRepository = new FighterPlayerPrefsRepository(fightersConfiguration, "fighter");
            _playerData = new PlayerData();
        }

        public void Refresh()
        {
            _backgroundsRepository.Refresh();
            _playerIconsRepository.Refresh();
            _fighterInfoRepository.Refresh();
            _playerData.Refresh();
        }

        public void Save()
        {
            _backgroundsRepository.Save();
            _playerIconsRepository.Save();
            _fighterInfoRepository.Save();
            _playerData.SaveData();
        }
    }
}