using Core.PopupSystem;
using UnityEngine;
using Zenject;

namespace Resources
{
    public class PopupSystemInstaller : MonoInstaller
    {
        [SerializeField] private PopupSystem _popupSystem;
        
        public override void InstallBindings()
        {
            Container.Bind<PopupSystem>().FromInstance(_popupSystem).AsSingle();
        }
    }
}
