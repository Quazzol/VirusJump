using System;
using UnityEngine;

namespace InputSystem
{
    public class InputController : MonoBehaviour
    {
        public static event Action Click;
        public static event Action Swipe;
        public static event Action BackClicked;


        private static InputController _instance = null;
        private float _mouseClickPosition;
        private Camera _mainCamera;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackClicked?.Invoke();
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _mouseClickPosition = Input.mousePosition.x;
            }

            if (Input.GetMouseButtonUp(0))
            {
                float distanceMovedPercent = GetInWorldMovementPercent(Input.mousePosition.x - _mouseClickPosition);
                if (Mathf.Abs(distanceMovedPercent) < 0.001f)
                {
                    Click?.Invoke();
                    return;
                }

                Swipe?.Invoke();
            }
        }

        private float GetInWorldMovementPercent(float distanceMoved)
        {
            return distanceMoved / _mainCamera.scaledPixelWidth;
        }
    }
}