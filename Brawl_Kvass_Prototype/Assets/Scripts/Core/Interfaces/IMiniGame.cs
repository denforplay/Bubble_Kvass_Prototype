using System;
using Models;
using Views.Popups.MiniGamesPopups;

namespace Core.Interfaces
{
    public interface IMiniGame
    {
        public event Action<MoneySystem> OnMoneyReceived;
        public event Action OnRestart;
        void OnStart();
        void Update();
        void Restart();
        void OnEnd();
        void OnEnable();
        void OnDisable();
        MiniGamePopup GetPopup();
    }
}