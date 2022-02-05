using Views.Popups.MiniGamesPopups;

namespace Core.Interfaces
{
    public interface IMiniGame
    {
        void OnStart();
        void OnEnd();
        void OnEnable();
        void OnDisable();
        MiniGamePopup GetPopup();
    }
}