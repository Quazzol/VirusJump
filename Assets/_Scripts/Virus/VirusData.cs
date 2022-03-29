using System;
using Cell;
using UnityEngine;

namespace Virus
{
    public class VirusData
    {
        public CellController CurrentCell { get; set; }
        public CellController NextCell => _nextCellFunction(CurrentCell);
        public Vector2 InitPosition => new Vector2(-1f, -3.5f);
        public float MovementSpeed => 1f;
        public float JumpPower => 1.9f;
        public float JumpToCellPower => JumpPower * 3;
        public float Radius = .2f;
        public int CellLayerMask { get; private set; }

        public VirusData()
        {
            CellLayerMask = LayerMask.GetMask("Cell");
        }

        private Func<CellController, CellController> _nextCellFunction;

        public void SetNextCellFunction(Func<CellController, CellController> func)
        {
            _nextCellFunction = func;
        }
    }
}