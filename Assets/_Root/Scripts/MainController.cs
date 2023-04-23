using Features.Fight;
using Features.Shed;
using Ui;
using Game;
using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    
    private readonly AnalyticsManager _analyticsManager;
    private readonly UnityAdsService _unityAdsService;
    private readonly IAPService _iapService;

    private MainMenuController _mainMenuController;
    private SettingsController _settingsController;
    private GameController _gameController;
    private FightController _fightController;
    private StartFightController _startFightController;
    
    private ShedContext _shedContext;
    
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager,
        UnityAdsService unityAdsService, IAPService iapService)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _analyticsManager = analyticsManager;
        _unityAdsService = unityAdsService;
        _iapService = iapService;

        _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        DisposeChildObjects();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }
    
    private void OnChangeGameState(GameState state)
    {
        DisposeChildObjects();
        
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, 
                    _analyticsManager, _unityAdsService, _iapService);
                break;
            case GameState.Settings:
                _settingsController = new SettingsController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedContext = new ShedContext(_placeForUi, _profilePlayer);
                AddContext(_shedContext);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer, _analyticsManager);
                _startFightController = new StartFightController(_placeForUi, _profilePlayer);
                break;
            case GameState.Fight:
                _fightController = new FightController(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeChildObjects()
    {
        _gameController?.Dispose();
        _mainMenuController?.Dispose();
        _settingsController?.Dispose();
        _fightController?.Dispose();
        _startFightController?.Dispose();
        
        _shedContext?.Dispose();
    }
}
