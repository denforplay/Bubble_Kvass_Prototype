using Configurations;
using Models;
using UnityEngine;
using Zenject;

namespace Resources
{
    public class PlayerDataInstaller : MonoInstaller<PlayerDataInstaller>
    {
        [SerializeField] private MainMenuBackgroundConfiguration _backgroundConfiguration;
        [SerializeField] private FightersConfiguration _fightersConfiguration;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromInstance(new PlayerData(_backgroundConfiguration, _fightersConfiguration)).AsSingle();
        }
    }
}