using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private InitialSettingsConfig _initialSettings;
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;
    [SerializeField] private UnityAdsService _unityAdsService;
    [SerializeField] private IAPService _iapService;
    
    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(_initialSettings.InitialSpeedCar, _initialSettings.InitialJumpHeight, _initialSettings.InitialState);
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
