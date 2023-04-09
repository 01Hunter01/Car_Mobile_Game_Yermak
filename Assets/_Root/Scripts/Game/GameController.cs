using System.Collections.Generic;
using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;
using Services.Analytics;
using UnityEngine;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly ResourcePath _abilitiesViewPath = new ("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _abilitiesDataSourcePath = new ("Configs/Ability/AbilityItemConfigDataSource");
        
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly AbilitiesController _abilitiesController;
        private readonly TapeBackgroundController _tapeBackgroundController;
        
        public GameController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _carController = CreateCarController();
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesController = CreateAbilityController(placeForUi, _carController);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff,_rightMoveDiff);

            analyticsManager.SendGameStartedEvent();
        }

        private CarController CreateCarController()
        {
            var carController = new CarController();
            AddController(carController);

            return carController;
        }

        private InputGameController CreateInputGameController( ProfilePlayer profilePlayer, 
            SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }

        private TapeBackgroundController CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private AbilitiesController CreateAbilityController(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            IAbilitiesView view = LoadAbilitiesView(placeForUi);
            IEnumerable<AbilityItemConfig> abilityItemConfigs = LoadAbilityItemConfigs();
            IAbilitiesRepository repository = CreateAbilitiesRepository(abilityItemConfigs);
            var abilitiesController = new AbilitiesController(view, repository, abilityItemConfigs, abilityActivator);
            AddController(abilitiesController);

            return abilitiesController;
        }
        
        private AbilityItemConfig[] LoadAbilityItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_abilitiesDataSourcePath);

        private AbilitiesRepository CreateAbilitiesRepository(IEnumerable<AbilityItemConfig> abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }

        private AbilitiesView LoadAbilitiesView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_abilitiesViewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
}
