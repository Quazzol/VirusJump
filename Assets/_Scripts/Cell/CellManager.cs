using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cell
{
    public class CellManager : MonoBehaviour
    {
        public event Action<int> CellInfected;

        private List<CellController> _cells = new List<CellController>();
        private Camera _mainCamera;
        private Transform _transform;
        private readonly int _activeCellCount = 7;
        private float _followDistance;

        private void Awake()
        {
            _transform = transform;
            _mainCamera = Camera.main;
            _followDistance = _mainCamera.transform.position.y - _transform.position.y;
            CellController.Infected += OnCellInfected;
        }

        private void Update()
        {
            _transform.position = _mainCamera.transform.position + Vector3.down * _followDistance;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<CellController>(out var cell))
                return;

            cell.Disable();
            InitializeCell(_cells.Count);
        }

        public void StartGame()
        {
            InitializeCells();
        }

        private void InitializeCells()
        {
            for (int i = 0; i < _activeCellCount; i++)
            {
                InitializeCell(i);
            }
        }

        private void InitializeCell(int cellIndex)
        {
            CellController cell = null;
            if (cellIndex == 0)
            {
                cell = CellFactory.GetCell(0);
            }
            else
            {
                cell = CellFactory.GetRandomCell();
            }

            cell.Initialize(cellIndex, GetCellPositionByIndex(cellIndex));
            _cells.Add(cell);
        }

        private Vector2 GetCellPositionByIndex(int cellIndex)
        {
            float yOffset = -3f;
            var xPos = (cellIndex % 2 == 0 ? -1 : 1) * Random.Range(0.75f, 1.25f);
            var yPos = yOffset + cellIndex * 4f + Random.Range(-0.25f, 0.25f);

            return new Vector2(xPos, yPos);
        }

        public CellController GetNextCell(CellController cell)
        {
            if (cell == null)
                return null;
            return _cells[cell.CellIndex + 1];
        }

        private void OnCellInfected(CellController cell)
        {
            CellInfected?.Invoke(cell.CellIndex);
        }
    }
}