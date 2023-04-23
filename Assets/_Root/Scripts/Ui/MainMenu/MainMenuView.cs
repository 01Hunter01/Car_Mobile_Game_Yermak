using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAdsReward;
        [SerializeField] private Button _buttonBuyProduct;
        [SerializeField] private Button _buttonDailyReward;
        [SerializeField] private Button _buttonExitGame;

        private UnityAction _startGameCache;
        private UnityAction _settingsCache;
        private UnityAction _rewardedVideoCache;
        private UnityAction _iapCache;
        private UnityAction _shedCache;
        private UnityAction _dailyRewardCache;
        private UnityAction _exitGameCache;

        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction playVideo,
            UnityAction buyItem, UnityAction openShed, UnityAction dailyReward, UnityAction exitGame)
        {
            _startGameCache = startGame;
            _settingsCache = openSettings;
            _rewardedVideoCache = playVideo;
            _iapCache = buyItem;
            _shedCache = openShed;
            _dailyRewardCache = dailyReward;
            _exitGameCache = exitGame;
            
            _buttonStart.onClick.AddListener(_startGameCache);
            _buttonSettings.onClick.AddListener(_settingsCache);
            _buttonAdsReward.onClick.AddListener(_rewardedVideoCache);
            _buttonBuyProduct.onClick.AddListener(_iapCache);
            _buttonShed.onClick.AddListener(_shedCache);
            _buttonDailyReward.onClick.AddListener(_dailyRewardCache);
            _buttonExitGame.onClick.AddListener(_exitGameCache);
        }
        
        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveListener(_startGameCache);
            _buttonSettings.onClick.RemoveListener(_settingsCache);
            _buttonAdsReward.onClick.RemoveListener(_rewardedVideoCache);
            _buttonBuyProduct.onClick.RemoveListener(_iapCache);
            _buttonShed.onClick.RemoveListener(_shedCache);
            _buttonDailyReward.onClick.RemoveListener(_dailyRewardCache);
            _buttonExitGame.onClick.RemoveListener(_exitGameCache);
        } 
    }
}
