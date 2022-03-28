using System;
using Pooling;
using UnityEngine;
using Misc;
using Cell;

namespace Virus
{
    public class VirusController : PooledMonoBehaviour
    {
        public event Action Killed;

        public override int InitialPoolSize => 1;

        private Transform _transform;
        private Material _material;
        private VirusAnimationController _animation;
        private VirusMovementController _movement;
        private VirusData _data;

        private void Awake()
        {
            _transform = transform;
            _animation = new VirusAnimationController(null, GetComponentInChildren<SpriteRenderer>().material);
            _movement = new VirusMovementController(_transform, GetComponent<Rigidbody2D>());
            _data = new VirusData();

            _movement.SetData(_data);
            _animation.Dissolved += OnDissolved;
        }

        private void Update()
        {
            _movement.Move();
        }

        public void Initialize()
        {
            _transform.position = _data.InitPosition;
            _transform.rotation = Quaternion.identity;
            _animation.Initialize();
        }

        public void SetNextCellFunction(Func<CellController, CellController> nextCell)
        {
            _data.SetNextCellFunction(nextCell);
        }

        public void Kill()
        {
            _animation.Dissolve();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.TryGetComponent<CellController>(out var cell))
                return;

            if (_data.CurrentCell != cell)
            {
                _data.CurrentCell = cell;
                _movement.Arrived();
            }

            _movement.Grounded();
        }

        private void OnDestroy()
        {
            _animation.CancelToken = true;
        }

        private void OnDissolved()
        {
            Killed?.Invoke();
            this.Disable();
        }

    }
}