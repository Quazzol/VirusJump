using System;
using UnityEngine;
using Cell;

namespace Virus
{
    public class VirusManager : MonoBehaviour
    {
        public Transform Virus => _virus.transform;
        private VirusController _virus;

        public void StartGame()
        {
            _virus = VirusFactory.GetVirus();
            _virus.Initialize();
        }

        public void SetNextCellFunction(Func<CellController, CellController> nextCell)
        {
            _virus.SetNextCellFunction(nextCell);
        }
    }
}