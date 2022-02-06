using System;
using Views.Popups.MiniGamesPopups;

namespace Core.Interfaces
{
    public interface IMiniGame
    {
        public event Action OnRestart;
        void OnStart();
        void Restart();
        void OnEnd();
        void OnEnable();
        void OnDisable();
        MiniGamePopup GetPopup();
    }
}