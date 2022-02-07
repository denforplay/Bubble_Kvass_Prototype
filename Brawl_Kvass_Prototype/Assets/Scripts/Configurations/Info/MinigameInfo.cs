﻿using Core.Enums;
using UnityEngine;

namespace Configurations.Info
{
    [CreateAssetMenu(menuName = "Configurations/Mini game info", order = 0)]
    public class MinigameInfo : ScriptableObject
    {
        [SerializeField] private string _miniGameName;
        [SerializeField] private Sprite _miniGameMiniIcon;
        [SerializeField] private Sprite _miniGameIcon;
        [SerializeField] private MiniGameType _gameType;

        public string MiniGameName => _miniGameName;
        public Sprite MiniGameMiniIcon => _miniGameMiniIcon;
        public Sprite MiniGameIcon => _miniGameIcon;
        public MiniGameType GameType => _gameType;
    }
}