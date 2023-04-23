using System;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Fight
{
    internal class FightController: BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/FightView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightView _view;
        private readonly Enemy _enemy;

        
        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crimeLevel;



        public FightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            
            _enemy = new Enemy("Enemy Car");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _crimeLevel = CreatePlayerData(DataType.CrimeLevel);

            Subscribe(_view);
        }

        protected override void OnDispose()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _crimeLevel);

            Unsubscribe(_view);
        }

        private FightView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightView>();
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
        
        private void Subscribe(FightView view)
        {
            view.AddMoneyButton.onClick.AddListener(IncreaseMoney);
            view.MinusMoneyButton.onClick.AddListener(DecreaseMoney);

            view.AddHealthButton.onClick.AddListener(IncreaseHealth);
            view.MinusHealthButton.onClick.AddListener(DecreaseHealth);

            view.AddPowerButton.onClick.AddListener(IncreasePower);
            view.MinusPowerButton.onClick.AddListener(DecreasePower);
            
            view.AddCrimeLevelButton.onClick.AddListener(IncreaseCrimeLevel);
            view.MinusCrimeLevelButton.onClick.AddListener(DecreaseCrimeLevel);

            view.FightButton.onClick.AddListener(Fight);
            view.EscapeButton.onClick.AddListener(Escape);
        }

        private void Unsubscribe(FightView view)
        {
            view.AddMoneyButton.onClick.RemoveListener(IncreaseMoney);
            view.MinusMoneyButton.onClick.RemoveListener(DecreaseMoney);

            view.AddHealthButton.onClick.RemoveListener(IncreaseHealth);
            view.MinusHealthButton.onClick.RemoveListener(DecreaseHealth);

            view.AddPowerButton.onClick.RemoveListener(IncreasePower);
            view.MinusPowerButton.onClick.RemoveListener(DecreasePower);
            
            view.AddCrimeLevelButton.onClick.RemoveListener(IncreaseCrimeLevel);
            view.MinusCrimeLevelButton.onClick.RemoveListener(DecreaseCrimeLevel);

            view.FightButton.onClick.RemoveListener(Fight);
            view.EscapeButton.onClick.RemoveListener(Escape);
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
            UpdateEscapeButtonVisibility();
        }
        
        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F}: {value}";

            int enemyPower = _enemy.CalcPower();
            _view.CountPowerEnemyText.text = $"Enemy Power: {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _view.CountMoneyText,
                DataType.Health => _view.CountHealthText,
                DataType.Power => _view.CountPowerText,
                DataType.CrimeLevel => _view.CountCrimeLevelText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private void UpdateEscapeButtonVisibility()
        {
            const int minCrimeToUse = 0;
            const int maxCrimeToUse = 2;
            const int minCrimeToShow = 0;
            const int maxCrimeToShow = 5;

            int crimeValue = _crimeLevel.Value;
            bool canUse = minCrimeToUse <= crimeValue && crimeValue <= maxCrimeToUse;
            bool canShow = minCrimeToShow <= crimeValue && crimeValue <= maxCrimeToShow;

            _view.EscapeButton.interactable = canUse;
            _view.EscapeButton.gameObject.SetActive(canShow);
        }
        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }

        private void Escape()
        {
            string color = "#FFB202";
            string message = "Escaped";
            
            Debug.Log($"<color={color}>{message}!!!</color>");

            Close();
        }

        private void Close() => _profilePlayer.CurrentState.Value = GameState.Game;
    }
}

