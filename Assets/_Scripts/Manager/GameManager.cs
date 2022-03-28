using UnityEngine;
using Cell;
using Virus;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private CellManager _cellManager;
        private VirusManager _virusManager;
        private CameraController _cameraController;

        private void Awake()
        {
            _cellManager = GetComponent<CellManager>();
            _virusManager = GetComponent<VirusManager>();
            _cameraController = GetComponent<CameraController>();
        }

        private void Start()
        {
            _cellManager.StartGame();
            _virusManager.StartGame();
            _virusManager.SetNextCellFunction(_cellManager.GetNextCell);
            _cameraController.SetVirus(_virusManager.Virus);
        }
    }
}