using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class OpenPauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _openPauseMenuButton;

        private UnityAction _openPauseMenuCache;
        
        public void Init(UnityAction openPauseMenu)
        {
            _openPauseMenuCache = openPauseMenu;
            _openPauseMenuButton.onClick.AddListener(_openPauseMenuCache);
            
        }

        public void OnDestroy() => 
            _openPauseMenuButton.onClick.RemoveListener(_openPauseMenuCache);
    
    }
}

