using UnityEngine;

public class BGCameraFollower : MonoBehaviour
{
    private Transform _transform;
    private Camera _mainCamera;

    private void Awake()
    {
        _transform = transform;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _transform.position = (Vector2)_mainCamera.transform.position;
    }

}
