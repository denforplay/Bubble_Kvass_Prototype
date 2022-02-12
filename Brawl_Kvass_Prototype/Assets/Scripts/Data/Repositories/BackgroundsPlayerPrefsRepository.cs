using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using Configurations;
using Configurations.Info;
using Core.Interfaces;
using UnityEngine;

namespace Data.Repositories
{
    public class BackgroundsPlayerPrefsRepository : IRepository<BackgroundInfo>
    {
        public event Action<BackgroundInfo> OnCurrentEntityChanged;
        private MainMenuBackgroundConfiguration _backgroundConfiguration;
        private BackgroundInfo _currentBackground;
        private string _key;
        public BackgroundInfo GetCurrent() => _currentBackground;
        public BackgroundsPlayerPrefsRepository(MainMenuBackgroundConfiguration backgroundConfiguration, string key)
        {
            _backgroundConfiguration = backgroundConfiguration;
            _key = key;
            
            if (PlayerPrefs.HasKey(_key))
            {
                int entityId = PlayerPrefs.GetInt(_key);
                _currentBackground = _backgroundConfiguration.Backgrounds.Find(x => x.Id == entityId);
            }
            else
                _currentBackground = _backgroundConfiguration.Backgrounds.First(x => x.Cost == 0);
        }

        public void Add(BackgroundInfo entity)
        {
            if (entity.Cost > 0 && !PlayerPrefs.HasKey(_key + entity.Id))
            {
                PlayerPrefs.SetInt(_key + entity.Id, entity.Id);
            }
        }

        public void Refresh()
        {
            OnCurrentEntityChanged?.Invoke(_currentBackground);
        }

        public void SetCurrent(int id)
        {
            if (FindById(id) != null)
                _currentBackground = FindById(id);
            
            
            OnCurrentEntityChanged?.Invoke(_currentBackground);
        }

        public BackgroundInfo FindById(int id)
        {
            if (PlayerPrefs.HasKey(_key + id))
            {
                int entityId = PlayerPrefs.GetInt(_key + id);
                return _backgroundConfiguration.Backgrounds.Find(x => x.Id == entityId);
            }

            return _backgroundConfiguration.Backgrounds.Find(x => x.Id == id);
        }

        public IEnumerable<BackgroundInfo> Get()
        {
            List<BackgroundInfo> backgrounds = new List<BackgroundInfo>();
            foreach (var entity in _backgroundConfiguration.Backgrounds)
            {
                if (entity.Cost == 0 || PlayerPrefs.HasKey(_key + entity.Id))
                    backgrounds.Add(entity);
            }

            return backgrounds;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(_key, _currentBackground.Id);
            PlayerPrefs.Save();
        }
    }
}