using System;
using System.Collections.Generic;
using System.Linq;
using Configurations;
using Configurations.Info;
using Core.Interfaces;

namespace Data.Repositories.PlayerPrefsData
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
            
            if (UnityEngine.PlayerPrefs.HasKey(_key))
            {
                int entityId = UnityEngine.PlayerPrefs.GetInt(_key);
                _currentBackground = _backgroundConfiguration.Backgrounds.Find(x => x.Id == entityId);
            }
            else
                _currentBackground = _backgroundConfiguration.Backgrounds.First(x => x.Cost == 0);
        }

        public void Add(BackgroundInfo entity)
        {
            if (entity.Cost > 0 && !UnityEngine.PlayerPrefs.HasKey(_key + entity.Id))
            {
                UnityEngine.PlayerPrefs.SetInt(_key + entity.Id, entity.Id);
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
            Save();
        }

        public BackgroundInfo FindById(int id)
        {
            if (UnityEngine.PlayerPrefs.HasKey(_key + id))
            {
                int entityId = UnityEngine.PlayerPrefs.GetInt(_key + id);
                return _backgroundConfiguration.Backgrounds.Find(x => x.Id == entityId);
            }

            return _backgroundConfiguration.Backgrounds.Find(x => x.Id == id && x.Cost == 0);
        }

        public IEnumerable<BackgroundInfo> Get()
        {
            List<BackgroundInfo> backgrounds = new List<BackgroundInfo>();
            foreach (var entity in _backgroundConfiguration.Backgrounds)
            {
                if (entity.Cost == 0 || UnityEngine.PlayerPrefs.HasKey(_key + entity.Id))
                    backgrounds.Add(entity);
            }

            return backgrounds;
        }

        public void Save()
        {
            UnityEngine.PlayerPrefs.SetInt(_key, _currentBackground.Id);
            UnityEngine.PlayerPrefs.Save();
        }
    }
}