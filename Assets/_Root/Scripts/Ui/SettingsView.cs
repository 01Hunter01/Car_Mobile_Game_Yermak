using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;
        
        public void Init(UnityAction settings) => 
            _buttonBack.onClick.AddListener(settings);

        public void OnDestroy() => 
            _buttonBack.onClick.RemoveAllListeners();
    }
}
