using System;
using Common;
using Misc;
using Pooling;
using UnityEngine;
using Virus;

namespace Cell
{
    public class CellController : PooledMonoBehaviour
    {
        public static event Action<CellController> Infected;

        public int CellIndex => _data.CellIndex;
        public Vector2 Position => _transform.position;
        public CellState State => _state;
        public int Direction => CellIndex % 2 == 0 ? -1 : 1;

        private Transform _transform;
        private CellAnimationController _animation;
        private RotationController _rotator;
        private CellMovementController _movement;
        private CellState _state;
        private CellData _data;

        private void Awake()
        {
            _transform = transform;
            _animation = new CellAnimationController(GetComponentInChildren<SpriteRenderer>());
            _rotator = new RotationController(_transform, 90f, 1);
            _movement = new CellMovementController(GetComponent<Rigidbody2D>());
            _data = new CellData();

            _animation.Infected += OnInfected;

            SetState(CellState.Normal);
        }

        private void Update()
        {
            _rotator.Rotate();
            _movement.Attract(_data.Virus);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_state != CellState.Normal)
                return;

            if (other.transform.TryGetComponent<VirusController>(out var virus))
            {
                _data.SetVirus(virus.GetComponent<Rigidbody2D>());
                SetState(CellState.Infecting);
            }
        }

        public void Initialize(int cellIndex, Vector2 coordinates)
        {
            SetState(CellState.Normal);
            _data.Initialize(cellIndex);
            _animation.Initialize(_data.InfectionPeriod);
            _rotator.Initialize(_data.RotationAnglePerSecond, Direction);
            _transform.position = coordinates;
        }

        public void Detach()
        {
            _data.SetVirus(null);
        }

        private void SetState(CellState state)
        {
            _state = state;
            _animation.ChangeMaterialState(state);
        }

        private void OnInfected()
        {
            SetState(CellState.Infected);
            Infected?.Invoke(this);
        }

        private void OnDestroy()
        {
            _animation.CancelToken = true;
        }
    }
}