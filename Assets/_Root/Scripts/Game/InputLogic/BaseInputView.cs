using JoostenProductions;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        protected float Speed;

        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;

        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);

        protected abstract void Move();

        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            float speed)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            Speed = speed;
        }
        
        protected void OnLeftMove(float value) =>
            _leftMove.Value = value;

        protected void OnRightMove(float value) =>
            _rightMove.Value = value;
    }
}
