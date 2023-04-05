using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly UnityAdsService _unityAdsService;
        private readonly IAPService _iapService;
        private readonly AnalyticsManager _analyticsManager;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager, 
            UnityAdsService unityAdsService, IAPService iapService)
        {
            _profilePlayer = profilePlayer;
            _analyticsManager = analyticsManager;
            _unityAdsService = unityAdsService;
            _iapService = iapService;

            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSettings, PlatVideo, BuyItem, OpenShed);
        }


        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;
        
        private void OpenSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void OpenShed() =>
            _profilePlayer.CurrentState.Value = GameState.Shed;
        
        private void PlatVideo() => _unityAdsService.RewardedPlayer.Play();
        
        private void BuyItem()
        {
            _iapService.Buy("extra_coins");
            _analyticsManager.OnPurchase("extra_coins", 1.99m, "USD");
        } 

    }
}
