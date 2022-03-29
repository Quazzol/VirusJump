using UnityEngine;

namespace Manager
{
    public class CameraController : MonoBehaviour
    {
        private Camera _mainCamera;
        private readonly float _yOffset = 2.5f;
        private readonly float _cameraMovementSpeed = 3f;
        private Transform _virus;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            FollowVirus();
        }

        public void SetVirus(Transform virus)
        {
            _virus = virus;
        }

        public void StopFollow()
        {
            _virus = null;
            _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x,
                                                            -_yOffset,
                                                            _mainCamera.transform.position.z);
        }

        private void FollowVirus()
        {
            if (_virus == null)
                return;

            _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x,
                                                    Mathf.Lerp(_mainCamera.transform.position.y, _virus.position.y + _yOffset, Time.deltaTime * _cameraMovementSpeed),
                                                    _mainCamera.transform.position.z);
        }

    }
}