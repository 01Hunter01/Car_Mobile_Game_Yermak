using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BattleScripts
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countCrimeLevelText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton; 
        
        [Header("Crime Level Buttons")]
        [SerializeField] private Button _addCrimeLevelButton;
        [SerializeField] private Button _minusCrimeLevelButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _withoutFightButton;
        [SerializeField] private Button _checkCrimeLevelButton;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crimeLevel;

        private Enemy _enemy;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _withoutFightButton.gameObject.SetActive(false);
            
            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _crimeLevel = CreatePlayerData(DataType.CrimeLevel);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _crimeLevel);

            Unsubscribe();
        }
        
        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }
        
        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _minusMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _minusHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _minusPowerButton.onClick.AddListener(DecreasePower);
            
            _addCrimeLevelButton.onClick.AddListener(IncreaseCrimeLevel);
            _minusCrimeLevelButton.onClick.AddListener(DecreaseCrimeLevel);

            _fightButton.onClick.AddListener(Fight);
            _withoutFightButton.onClick.AddListener(WithoutFight);
            _checkCrimeLevelButton.onClick.AddListener(CheckCrimeLevel);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _minusMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _addHealthButton.onClick.RemoveListener(IncreaseHealth);
            _minusHealthButton.onClick.RemoveListener(DecreaseHealth);

            _addPowerButton.onClick.RemoveListener(IncreasePower);
            _minusPowerButton.onClick.RemoveListener(DecreasePower);
            
            _addCrimeLevelButton.onClick.RemoveListener(IncreaseCrimeLevel);
            _minusCrimeLevelButton.onClick.RemoveListener(DecreaseCrimeLevel);
            
            _fightButton.onClick.RemoveListener(Fight);
            _withoutFightButton.onClick.RemoveListener(WithoutFight);
            _checkCrimeLevelButton.onClick.RemoveListener(CheckCrimeLevel);
        }
        
        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);
        private void IncreaseCrimeLevel() => IncreaseValue(_crimeLevel);
        private void DecreaseCrimeLevel() => DecreaseValue(_crimeLevel);

        private void IncreaseValue(PlayerData playerData) => AddToValue(1, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-1, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);
        }
        
        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F}: {value}";

            int enemyPower = _enemy.CalcPower();
            _countPowerEnemyText.text = $"Enemy Power: {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.CrimeLevel => _countCrimeLevelText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };
        
        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }

        private void CheckCrimeLevel()
        {
            if (_crimeLevel.Value >= 0 & _crimeLevel.Value <= 2)
            {
                _withoutFightButton.gameObject.SetActive(true);
            }
            else if (_crimeLevel.Value >= 3 & _crimeLevel.Value <= 5)
            {
                _withoutFightButton.gameObject.SetActive(false);
                Debug.Log("Fight DWEEB!");
            }
            else
            {
                _withoutFightButton.gameObject.SetActive(false);
            }
        }
        
        private void WithoutFight()
        {
            Debug.Log("Passed without Fight. You are peaceful person! Go to next level.");
            _money.Value = 0;
            ChangeDataWindow(_money);
            _heath.Value = 0;
            ChangeDataWindow(_heath);
            _power.Value = 0;
            ChangeDataWindow(_power);
            _crimeLevel.Value = 0;
            ChangeDataWindow(_crimeLevel);
            _withoutFightButton.gameObject.SetActive(false);
        }
    }
}
