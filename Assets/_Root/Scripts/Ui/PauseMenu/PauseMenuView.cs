using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _mainMenuButton;

        private UnityAction _continueCache;
        private UnityAction _mainMenuCache;
        
        public void Init(UnityAction continueGame, UnityAction openMainMenu)
        {
            _continueCache = continueGame;
            _mainMenuCache = openMainMenu;
            
            _continueButton.onClick.AddListener(_continueCache);
            _mainMenuButton.onClick.AddListener(_mainMenuCache);
            
        }

        public void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(_continueCache);
            _mainMenuButton.onClick.RemoveListener(_mainMenuCache);
        } 


    }
}
