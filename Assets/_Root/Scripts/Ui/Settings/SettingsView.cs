using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;

        private UnityAction _buttonBackCache;
        
        public void Init(UnityAction backToMainMenu)
        {
            _buttonBackCache = backToMainMenu;
            _buttonBack.onClick.AddListener(_buttonBackCache);
            
        }

        public void OnDestroy() => 
            _buttonBack.onClick.RemoveListener(_buttonBackCache);
    }
}
