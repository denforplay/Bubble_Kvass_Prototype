using System;
using System.Collections.Generic;
using System.Linq;
using Configurations;
using Configurations.Info;
using Core.Interfaces;
using UnityEngine;

namespace Data.Repositories.PlayerPrefsData
{
    public class FighterPlayerPrefsRepository : IRepository<FighterInfo>
    {
        public event Action<FighterInfo> OnCurrentEntityChanged;
        
        private FightersConfiguration _fightersConfiguration;
        private FighterInfo _currentFighter;
        private string _key;
        public FighterInfo GetCurrent() => _currentFighter;
        public FighterPlayerPrefsRepository(FightersConfiguration fightersConfiguration, string key)
        {
            _fightersConfiguration = fightersConfiguration;
            _key = key;
            
            if (PlayerPrefs.HasKey(_key))
            {
                int entityId = PlayerPrefs.GetInt(_key);
                _currentFighter = _fightersConfiguration.FighterInfos.Find(x => x.Id == entityId);
            }
            else
                _currentFighter = _fightersConfiguration.FighterInfos.First(x => x.IsUnlocked);
        }

        public void Add(FighterInfo entity)
        {
            if (!entity.IsUnlocked && !PlayerPrefs.HasKey(_key + entity.Id))
            {
                PlayerPrefs.SetInt(_key + entity.Id, entity.Id);
            }
        }

        public void Refresh()
        {
            OnCurrentEntityChanged?.Invoke(_currentFighter);
        }

        public void SetCurrent(int id)
        {
            if (FindById(id) != null)
                _currentFighter = FindById(id);
            
            OnCurrentEntityChanged?.Invoke(_currentFighter);
            Save();
        }

        public FighterInfo FindById(int id)
        {
            if (PlayerPrefs.HasKey(_key + id))
            {
                int entityId = PlayerPrefs.GetInt(_key + id);
                return _fightersConfiguration.FighterInfos.Find(x => x.Id == entityId);
            }

            return _fightersConfiguration.FighterInfos.Find(x => x.Id == id && x.IsUnlocked);
        }

        public IEnumerable<FighterInfo> Get()
        {
            List<FighterInfo> fighters = new List<FighterInfo>();
            foreach (var entity in _fightersConfiguration.FighterInfos)
            {
                if (entity.IsUnlocked || PlayerPrefs.HasKey(_key + entity.Id))
                    fighters.Add(entity);
            }

            return fighters;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(_key, _currentFighter.Id);
            PlayerPrefs.Save();
        }
    }
}