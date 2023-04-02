using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;
    [SerializeField] private UnityAdsService _unityAdsService;
    [SerializeField] private IAPService _iapService;
    
    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsManager, _unityAdsService, _iapService);
        
        if(_unityAdsService.IsInitialized) OnAdsInitialized();
        else _unityAdsService.Initialized.AddListener(OnAdsInitialized);
    }

    private void OnDestroy()
    {
        _unityAdsService.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }

    private void OnAdsInitialized() => _unityAdsService.InterstitialPlayer.Play();
}
