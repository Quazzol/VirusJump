using UnityEngine;
using Virus;

namespace Cell
{
    public class CellData
    {
        public int CellIndex { get; private set; }
        public float RotationAnglePerSecond { get; private set; }
        public float InfectionPeriod { get; private set; }
        public VirusController Virus { get; private set; }
        public Rigidbody2D VirusBody { get; private set; }
        private readonly float _baseRotationAngle = 30f;
        private readonly float _baseInfectionPeriod = 2f;

        public void Initialize(int cellIndex)
        {
            Virus = null;
            CellIndex = cellIndex;
            RotationAnglePerSecond = (1 + CellIndex * .1f) * _baseRotationAngle;
            InfectionPeriod = (1 + CellIndex * .1f) * _baseInfectionPeriod;
        }

        public void SetVirus(VirusController virus)
        {
            Virus = virus;
            VirusBody = virus == null ? null : virus.GetComponent<Rigidbody2D>();
        }
    }
}