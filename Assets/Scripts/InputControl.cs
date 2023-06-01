using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ulf
{
    public class InputControl : MonoBehaviour, IControl
    {
        [SerializeField] private InputAction moveAction;
        [SerializeField] private InputAction attackAction;

        public Action<int> OnMove { get; set; }

        private void Start()
        {
            moveAction.canceled += MoveActionCancel;
            moveAction.started += MoveActionStart;
        }

        private void MoveActionCancel(InputAction.CallbackContext context)
        {
            Debug.Log("Canceled: " + context.ReadValue<float>());
            OnMove?.Invoke((int)context.ReadValue<float>());
        }

        private void MoveActionStart(InputAction.CallbackContext context)
        {
            Debug.Log("Started: " + context.ReadValue<float>());
            OnMove?.Invoke((int)context.ReadValue<float>());
            
        }

        public void OnEnable()
        {
            moveAction.Enable();
            attackAction.Enable();
        }

        public void OnDisable()
        {
            moveAction.Disable();
            attackAction.Disable();
        }
    }
}
