using Cell;
using InputSystem;
using Misc;
using UnityEngine;

namespace Virus
{
    public class VirusMovementController
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private VirusData _data;
        private bool _isGrounded = true;

        public VirusMovementController(Transform transform, Rigidbody2D rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            InputController.Click += OnClicked;
            InputController.Swipe += OnSwiped;
        }

        public void Move()
        {
            LookAt();
            Run();
        }

        public void Grounded()
        {
            _isGrounded = true;
        }

        public void Arrived()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        public void SetData(VirusData data)
        {
            _data = data;
        }

        private void LookAt()
        {
            if (_data.CurrentCell == null)
                return;

            Vector3 diff = _data.CurrentCell.transform.position - _transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        }

        private void Jump()
        {
            if (_data.CurrentCell == null)
                return;

            if (!_isGrounded)
                return;

            var verticalVector = (Vector2)_transform.position - _data.CurrentCell.Position;
            var orthagonalVector = Vector2.Perpendicular(verticalVector) / 2 * _data.CurrentCell.Direction;

            JumpTo(verticalVector - orthagonalVector, _data.JumpPower);
        }

        private void JumpToClosestCell()
        {
            var nextCell = _data.NextCell;
            if (!CanJump(nextCell))
                return;

            _data.CurrentCell?.Detach();
            _data.CurrentCell = null;

            JumpTo(nextCell.Position - (Vector2)_transform.position, _data.JumpToCellPower);
        }

        private void JumpTo(Vector2 direction, float jumpPower)
        {
            _rigidbody.AddForce(direction.normalized * jumpPower, ForceMode2D.Impulse);
            _isGrounded = false;
        }

        private RaycastHit2D[] _hitResults = new RaycastHit2D[3];
        private bool CanJump(CellController nextCell)
        {
            if (nextCell == null)
                return false;

            if (_data.CurrentCell == null || _data.CurrentCell.State != CellState.Infected)
                return false;

            var direction = nextCell.transform.position - _transform.position;

            return Physics2D.CircleCastNonAlloc(_transform.position, _data.Radius, direction.normalized, _hitResults, direction.magnitude) <= 2;
        }

        private void Run()
        {
            if (_data.CurrentCell == null)
                return;

            _transform.position += _transform.right * Time.deltaTime * _data.MovementSpeed * _data.CurrentCell.Direction;
        }

        private void OnClicked()
        {
            Jump();
        }

        private void OnSwiped()
        {
            JumpToClosestCell();
        }
    }
}