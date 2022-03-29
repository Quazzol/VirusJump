using System;
using UnityEngine;
using Cell;

namespace Virus
{
    public class VirusManager : MonoBehaviour
    {
        public event Action VirusKilled;

        public Transform Virus => _virus.transform;
        private VirusController _virus;

        public void StartGame()
        {
            if (_virus != null)
            {
                _virus.Killed -= OnKilled;
            }
            _virus = VirusFactory.GetVirus();
            _virus.Initialize();
            _virus.Killed += OnKilled;
        }

        public void SetNextCellFunction(Func<CellController, CellController> nextCell)
        {
            _virus.SetNextCellFunction(nextCell);
        }

        private void OnKilled()
        {
            VirusKilled?.Invoke();
        }
    }
}