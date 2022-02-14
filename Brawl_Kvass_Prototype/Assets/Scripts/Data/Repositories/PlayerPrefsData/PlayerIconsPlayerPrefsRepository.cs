using System;
using System.Collections.Generic;
using System.Linq;
using Configurations;
using Configurations.Info;
using Core.Interfaces;
using UnityEngine;

namespace Data.Repositories.PlayerPrefsData
{
    public class PlayerIconsRepository : IRepository<PlayerIconInfo>
    {
        public event Action<PlayerIconInfo> OnCurrentEntityChanged;
        
        private PlayerIconsConfiguration _playerIconsConfiguration;
        private PlayerIconInfo _currentIcon;
        private string _key;
        public PlayerIconInfo GetCurrent() => _currentIcon;
        public PlayerIconsRepository(PlayerIconsConfiguration playerIconsConfiguration, string key)
        {
            _playerIconsConfiguration = playerIconsConfiguration;
            _key = key;
            
            if (PlayerPrefs.HasKey(_key))
            {
                int entityId = PlayerPrefs.GetInt(_key);
                _currentIcon = _playerIconsConfiguration.PlayerIconInfos.Find(x => x.Id == entityId);
            }
            else
                _currentIcon = _playerIconsConfiguration.PlayerIconInfos.First(x => x.IsUnlocked);
        }

        public void Add(PlayerIconInfo entity)
        {
            if (!entity.IsUnlocked && !PlayerPrefs.HasKey(_key + entity.Id))
            {
                PlayerPrefs.SetInt(_key + entity.Id, entity.Id);
            }
        }

        public void Refresh()
        {
            OnCurrentEntityChanged?.Invoke(_currentIcon);
        }

        public void SetCurrent(int id)
        {
            if (FindById(id) != null)
                _currentIcon = FindById(id);
            
            OnCurrentEntityChanged?.Invoke(_currentIcon);
            Save();
        }

        public PlayerIconInfo FindById(int id)
        {
            if (PlayerPrefs.HasKey(_key + id))
            {
                int entityId = PlayerPrefs.GetInt(_key + id);
                return _playerIconsConfiguration.PlayerIconInfos.Find(x => x.Id == entityId);
            }

            return _playerIconsConfiguration.PlayerIconInfos.Find(x => x.Id == id && x.IsUnlocked);
        }

        public IEnumerable<PlayerIconInfo> Get()
        {
            List<PlayerIconInfo> playerIcons = new List<PlayerIconInfo>();
            foreach (var entity in _playerIconsConfiguration.PlayerIconInfos)
            {
                if (entity.IsUnlocked || PlayerPrefs.HasKey(_key + entity.Id))
                    playerIcons.Add(entity);
            }

            return playerIcons;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(_key, _currentIcon.Id);
            PlayerPrefs.Save();
        }
    }
}