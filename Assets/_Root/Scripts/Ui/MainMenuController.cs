using Profile;
using Services.Ads.UnityAds;
using Services.IAP;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly UnityAdsService _unityAdsService;
        private readonly IAPService _iapService;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, 
            UnityAdsService unityAdsService, IAPService iapService)
        {
            _profilePlayer = profilePlayer;
            _unityAdsService = unityAdsService;
            _iapService = iapService;
            _view = LoadView(placeForUi);
            _view.InitStartGame(StartGame);
            _view.InitSettings(SettingsMenu);
            _view.InitRewardedVideo(OnRewardedVideoPlay);
            _view.InitPurchase(OnPurchaseBuy);
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
        
        private void SettingsMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;
        
        private void OnRewardedVideoPlay() => _unityAdsService.RewardedPlayer.Play();
        
        private void OnPurchaseBuy() => _iapService.Buy("extra_coins");

    }
}
