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
        public float InfectionPercent => _activeCell == null ? 0 : _activeCell.InfectionPercent;
        public float BlowPercent => _activeCell == null ? 0 : _activeCell.BlowPercent;

        private List<CellController> _cells = new List<CellController>();
        private Transform _transform;
        private readonly int _activeCellCount = 7;
        private CellController _activeCell;

        private void Awake()
        {
            _transform = transform;
            CellController.Activated += OnCellActivated;
            CellController.Infected += OnCellInfected;
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
            DeactivateExistingCells();
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
            var cell = CellFactory.GetCell(CalculateCellDifficulty(cellIndex));
            cell.Initialize(cellIndex, GetCellPositionByIndex(cellIndex));
            _cells.Add(cell);
        }

        private int CalculateCellDifficulty(int cellIndex)
        {
            if (cellIndex == 0)
                return 0;

            var difficulty = (cellIndex / 4) + 1;
            difficulty += (Random.value > .3f ? 1 : 0);
            difficulty += (Random.value > .3f ? -1 : 0);
            return difficulty;
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

        private void DeactivateExistingCells()
        {
            foreach (var cell in _cells)
                cell.Disable();
            _cells.Clear();
        }

        private void OnCellActivated(CellController cell)
        {
            _activeCell = cell;
        }

        private void OnCellInfected(CellController cell)
        {
            CellInfected?.Invoke(cell.CellIndex);
        }
    }
}