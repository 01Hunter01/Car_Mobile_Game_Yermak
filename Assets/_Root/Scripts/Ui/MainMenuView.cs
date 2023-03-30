using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;

        private UnityAction _startGameCache;
        private UnityAction _settingsCache;

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

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveListener(_startGameCache);
            _buttonSettings.onClick.RemoveListener(_settingsCache);
        } 
    }
}
