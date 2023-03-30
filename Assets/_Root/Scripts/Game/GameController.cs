using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;
using Tool.Analytics;

namespace Game
{
    internal class GameController : BaseController
    {
        public GameController(ProfilePlayer profilePlayer, AnalyticsManager analyticsManager)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController();
            AddController(carController);
            
            analyticsManager.SendGameStartedEvent();
        }
    }
}
