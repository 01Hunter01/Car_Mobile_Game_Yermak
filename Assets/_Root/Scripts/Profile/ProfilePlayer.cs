using Features.Inventory;
using Features.Rewards.Currency;
using Game.Car;
using Tool;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly InventoryModel Inventory;
        public readonly CurrencyModel Currency;


        public ProfilePlayer(float speedCar, float jumpHeight, GameState initialState) : this(speedCar, jumpHeight)
        {
            CurrentState.Value = initialState;
        }

        private ProfilePlayer(float speedCar, float jumpHeight)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar, jumpHeight);
            Inventory = new InventoryModel();
            Currency = new CurrencyModel();
        }
    }
}
