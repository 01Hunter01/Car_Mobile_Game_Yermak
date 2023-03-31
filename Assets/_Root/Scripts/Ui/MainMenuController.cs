using Profile;
using Services.Ads.UnityAds;
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


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService unityAdsService)
        {
            _profilePlayer = profilePlayer;
            _unityAdsService = unityAdsService;
            _view = LoadView(placeForUi);
            _view.InitStartGame(StartGame);
            _view.InitSettings(SettingsMenu);
            _view.InitRewardedVideo(OnRewardedVideoPlay);
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
    }
}
