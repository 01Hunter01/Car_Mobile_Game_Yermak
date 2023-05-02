using Features.AbilitySystem;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;
using Services.Analytics;
using Ui;
using UnityEngine;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly AbilityContext _abilityContext;
        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly OpenPauseMenuController _openPauseMenuController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();
            
            _carController = CreateCarController();
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff,_rightMoveDiff);
            _openPauseMenuController = CreateOpenPauseMenuController(placeForUi, profilePlayer);
            
            _abilityContext = CreateAbilityContext(placeForUi, _carController);

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

        private AbilityContext CreateAbilityContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            var abilityContext = new AbilityContext(placeForUi, abilityActivator);
            AddContext(abilityContext);
            
            return abilityContext;
        }

        private OpenPauseMenuController CreateOpenPauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var openPauseMenuController = new OpenPauseMenuController(placeForUi, profilePlayer);
            AddController(openPauseMenuController);

            return openPauseMenuController;
           
        }
    }
}
