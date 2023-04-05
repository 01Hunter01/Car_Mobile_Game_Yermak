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
        [SerializeField] private Button _buttonWatchVideo;
        [SerializeField] private Button _buttonPurchase;

        private UnityAction _startGameCache;
        private UnityAction _settingsCache;
        private UnityAction _rewardedVideoCache;
        private UnityAction _iapCache;
        private UnityAction _shedCache;

        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction playVideo,
            UnityAction buyItem, UnityAction openShed)
        {
            _startGameCache = startGame;
            _settingsCache = openSettings;
            _rewardedVideoCache = playVideo;
            _iapCache = buyItem;
            _shedCache = openShed;
            
            _buttonStart.onClick.AddListener(_startGameCache);
            _buttonSettings.onClick.AddListener(_settingsCache);
            _buttonWatchVideo.onClick.AddListener(_rewardedVideoCache);
            _buttonPurchase.onClick.AddListener(_iapCache);
            _buttonShed.onClick.AddListener(_shedCache);
        }
        
        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveListener(_startGameCache);
            _buttonSettings.onClick.RemoveListener(_settingsCache);
            _buttonWatchVideo.onClick.RemoveListener(_rewardedVideoCache);
            _buttonPurchase.onClick.RemoveListener(_iapCache);
            _buttonShed.onClick.RemoveListener(_shedCache);
        } 
    }
}
