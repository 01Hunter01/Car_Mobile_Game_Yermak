using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    internal class TweenAbilityButton: MonoBehaviour
    {

        [Header("Components")] 
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")] 
        [SerializeField] private Ease _curveEase;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 20f;

        private void Awake() => InitRenderer();
        private void OnValidate() => InitRenderer();

        private void Start() => _button.onClick.AddListener(ActivateAnimation);
        private void OnDestroy() => _button.onClick.RemoveListener(ActivateAnimation);

        private void InitRenderer()
        {
            _button ??= GetComponent<Button>();
            _rectTransform ??= GetComponent<RectTransform>();  
        } 

        private void ActivateAnimation()
        {
            _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase);
        }
    }
}