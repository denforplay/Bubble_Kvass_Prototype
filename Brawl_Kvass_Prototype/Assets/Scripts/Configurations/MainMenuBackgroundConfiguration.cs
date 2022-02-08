using System.Collections.Generic;
using Configurations.Info;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Main menu backgrounds configuration", order = 0)]
    public class MainMenuBackgroundConfiguration : ScriptableObject
    {
        [SerializeField] private List<BackgroundInfo> _backgrounds;

        public List<BackgroundInfo> Backgrounds => _backgrounds;
    }
}