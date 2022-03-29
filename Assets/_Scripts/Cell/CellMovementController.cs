using UnityEngine;

namespace Cell
{
    public class CellMovementController
    {
        private Rigidbody2D _rigidbody;

        public CellMovementController(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Attract(Rigidbody2D virus)
        {
            if (virus == null)
                return;

            Vector2 direction = _rigidbody.position - virus.position;

            float forceOfMagnitude = (_rigidbody.mass * virus.mass);
            Vector2 force = direction.normalized * forceOfMagnitude;

            virus.AddForce(force);
        }
    }
}