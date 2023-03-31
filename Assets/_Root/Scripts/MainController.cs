using Ui;
using Game;
using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly AnalyticsManager _analyticsManager;
    private readonly UnityAdsService _unityAdsService;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsController _settingsController;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, 
        AnalyticsManager analyticsManager, UnityAdsService unityAdsService)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _analyticsManager = analyticsManager;
        _unityAdsService = unityAdsService;

        _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _unityAdsService);
                _settingsController?.Dispose();
                _gameController?.Dispose();
                break;
            case GameState.Settings:
                _settingsController = new SettingsController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer, _analyticsManager);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _settingsController.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
