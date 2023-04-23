using UnityEngine;

namespace Features.Fight
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 5.5f;
        private const float KPower = 0.9f;
        private const float MaxHealthPlayer = 10f;

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;


        public Enemy(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            int kHealth = CalcKHealth();
            float moneyRatio = _moneyPlayer / KMoney;
            float powerRatio = _powerPlayer / KPower;

            return (int)(moneyRatio + kHealth + powerRatio);
        }

        private int CalcKHealth()
        {
            if (_healthPlayer >= MaxHealthPlayer)
            {
                _healthPlayer = 100;
            }
            else if (_healthPlayer < MaxHealthPlayer & _healthPlayer > 0)
            {
                _healthPlayer = -2;
            }
            else
            {
                _healthPlayer = 0;
            }

            return _healthPlayer;
        }
    }
}

