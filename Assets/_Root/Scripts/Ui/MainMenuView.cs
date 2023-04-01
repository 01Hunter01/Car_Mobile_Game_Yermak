using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewardedVideo;
        [SerializeField] private Button _buttonIAP;

        private UnityAction _startGameCache;
        private UnityAction _settingsCache;
        private UnityAction _rewardedVideoCache;
        private UnityAction _iapCache;

        public void InitStartGame(UnityAction startGame)
        {
            _startGameCache = startGame;
            _buttonStart.onClick.AddListener(_startGameCache);
        }

        public void InitSettings(UnityAction settings)
        {
            _settingsCache = settings;
            _buttonSettings.onClick.AddListener(_settingsCache);
        }

        public void InitRewardedVideo(UnityAction rewardedVideo)
        {
            _rewardedVideoCache = rewardedVideo;
            _buttonRewardedVideo.onClick.AddListener(rewardedVideo);
        }
        
        public void InitPurchase(UnityAction purchase)
        {
            _iapCache = purchase;
            _buttonIAP.onClick.AddListener(_iapCache);
        }


        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveListener(_startGameCache);
            _buttonSettings.onClick.RemoveListener(_settingsCache);
            _buttonRewardedVideo.onClick.RemoveListener(_rewardedVideoCache);

        } 
    }
}
