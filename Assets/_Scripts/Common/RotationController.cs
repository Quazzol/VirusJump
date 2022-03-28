using UnityEngine;

namespace Common
{
    public class RotationController
    {
        private readonly Transform _transform;
        private float _rotationAnglePerSecond;

        public RotationController(Transform transform, float rotationAnglePerSecond, int direction)
        {
            _transform = transform;
            Initialize(rotationAnglePerSecond, direction);
        }

        public void Initialize(float rotationAnglePerSecond, int direction)
        {
            _rotationAnglePerSecond = direction * rotationAnglePerSecond;
        }

        public void Rotate()
        {
            float angle = Time.deltaTime / _rotationAnglePerSecond * 360;
            _transform.Rotate(0f, 0f, angle);
        }
    }
}