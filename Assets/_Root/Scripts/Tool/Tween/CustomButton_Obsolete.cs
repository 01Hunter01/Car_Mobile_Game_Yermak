using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tool.Tween
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomButton_Obsolete : Button
    {
        public static string AnimationTypeName => nameof(_animationButtonType);
        public static string CurveEaseName => nameof(_curveEase);
        public static string DurationName => nameof(_duration);
        public static string StrengthName => nameof(_strength);
        public static string Vector3Name => nameof(_vector3Custom);
        

        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private Vector3 _vector3Custom = Vector3.forward;


        protected override void Awake()
        {
            base.Awake();
            InitRectTransform();
        }

        protected new void OnValidate() => InitRectTransform();

        private void InitRectTransform() =>
            _rectTransform ??= GetComponent<RectTransform>();


        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ActivateAnimation();
        }

        [ContextMenu(nameof(ActivateAnimation))]
        private void ActivateAnimation()
        {
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _rectTransform.DOShakeRotation(_duration, _vector3Custom * _strength).SetEase(_curveEase);
                    break;

                case AnimationButtonType.ChangePosition:
                    _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase);
                    break;
            }
        }

        [ContextMenu(nameof(Stop))]
        private void Stop()
        {
            _rectTransform.DOKill(true);
        }
    }
}
