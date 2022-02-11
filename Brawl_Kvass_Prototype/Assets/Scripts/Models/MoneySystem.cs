using System;

namespace Models
{
    public class MoneySystem
    {
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnGemsChanged;
        private int _coins;
        private int _gems;

        public int CurrentCoins => _coins;
        public int CurrentGems => _gems;

        public void Restart()
        {
            _coins = 0;
            _gems = 0;
            OnMoneyChanged?.Invoke(_coins);
            OnGemsChanged?.Invoke(_gems);
        }
        
        public void ChangeCoins(int value)
        {
            _coins += value;
            OnMoneyChanged?.Invoke(_coins);
        }

        public void ChangeGems(int value)
        {
            _gems += value;
            OnGemsChanged?.Invoke(_gems);
        }
    }
}