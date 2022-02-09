﻿using UnityEngine;

namespace Configurations.Info
{
    [CreateAssetMenu(menuName = "Configurations/Background info", order = 0)]
    public class BackgroundInfo : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _cost;

        public int Id => _id;
        public int Cost => _cost;
        public Sprite Sprite => _sprite;
    }
}