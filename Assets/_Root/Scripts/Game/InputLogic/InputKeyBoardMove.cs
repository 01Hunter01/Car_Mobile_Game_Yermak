using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputKeyboardMove : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 0.2f;

        private void Start() => 
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

        private void Move()
        {
            float moveValue = Speed * _inputMultiplier * Time.deltaTime;
            

            if (Input.GetKey(KeyCode.RightArrow))
                OnRightMove(moveValue);
            if (Input.GetKey(KeyCode.LeftArrow))
                OnLeftMove(moveValue);
        }
    }
}
