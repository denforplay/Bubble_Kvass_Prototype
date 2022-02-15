using Configurations;
using Data;
using UnityEngine;
using Zenject;

namespace Resources
{
    public class PlayerDataInstaller : MonoInstaller<PlayerDataInstaller>
    {
        [SerializeField] private MainMenuBackgroundConfiguration _backgroundConfiguration;
        [SerializeField] private FightersConfiguration _fightersConfiguration;
        [SerializeField] private PlayerIconsConfiguration _playerIconsConfiguration;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerDataProvider>().FromInstance(new PlayerDataProvider(_backgroundConfiguration, _playerIconsConfiguration, _fightersConfiguration)).AsSingle();
        }
    }
}