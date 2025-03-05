using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ulf
{
    public class InputControl : MonoBehaviour
    {
        [SerializeField] private InputAction moveAction;
        [SerializeField] private InputAction standAction;
        [SerializeField] private InputAction attackAction;

        public Action<int> OnMove { get; set; }
        public Action<int> OnStand { get; set; }
        public Action<int> OnAttack { get; set; }

        private void Start()
        {
            moveAction.canceled += MoveActionCancel;
            moveAction.started += MoveActionStart;

            standAction.canceled += StandActionCancel;
            standAction.started += StandActionStart;

            attackAction.started += AttackActionStart;
        }

        private void MoveActionCancel(InputAction.CallbackContext context)
        {
            //Debug.Log("Canceled: " + context.ReadValue<float>());
            OnMove?.Invoke((int)context.ReadValue<float>());
        }

        private void MoveActionStart(InputAction.CallbackContext context)
        {
            //Debug.Log("Started: " + context.ReadValue<float>());
            OnMove?.Invoke((int)context.ReadValue<float>());
            
        }

        private void StandActionCancel(InputAction.CallbackContext context)
        {
            //Debug.Log("Canceled: " + context.ReadValue<float>());
            OnStand?.Invoke((int)context.ReadValue<float>());
        }

        private void StandActionStart(InputAction.CallbackContext context)
        {
            //Debug.Log("Started: " + context.ReadValue<float>());
            OnStand?.Invoke((int)context.ReadValue<float>());

        }

        private void AttackActionStart(InputAction.CallbackContext context)
        {
            int attack = (int)context.ReadValue<float>();
            OnAttack?.Invoke(attack);
        }

        public void OnEnable()
        {
            standAction.Enable();
            moveAction.Enable();
            attackAction.Enable();
        }

        public void OnDisable()
        {
            standAction.Disable();
            moveAction.Disable();
            attackAction.Disable();
        }
    }
}
