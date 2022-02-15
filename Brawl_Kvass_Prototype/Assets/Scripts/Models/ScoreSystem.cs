using System;
using UnityEngine;

namespace Models
{
    public class ScoreSystem
    {
        public event Action<int> OnScoreChanged;
        public event Action<int> OnBestScoreChanged;
        
        private readonly string _gameName;
        private int _maxScore;
        private int _currentScore;

        public int CurrentScore => _currentScore;

        public ScoreSystem(string gameName)
        {
            _gameName = gameName;
            Restart();
        }

        public void Restart()
        {
            _currentScore = 0;
            _maxScore = PlayerPrefs.GetInt(_gameName);
            OnScoreChanged?.Invoke(_currentScore);
            OnBestScoreChanged?.Invoke(_maxScore);
        }

        public void AddScores(int score)
        {
            _currentScore += score;
            OnScoreChanged?.Invoke(_currentScore);
            if (_currentScore > _maxScore)
            {
                _maxScore = _currentScore;
                OnBestScoreChanged?.Invoke(_maxScore);
            }
        }

        public void SaveBestScore()
        {
            PlayerPrefs.SetInt(_gameName, _maxScore);
            PlayerPrefs.Save();
        }
    }
}