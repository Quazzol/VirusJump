using UnityEngine;
using Cell;
using Virus;
using InputSystem;
using Misc;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private CellManager _cellManager;
        private VirusManager _virusManager;
        private CameraController _cameraController;
        private GameUIManager _uiManager;
        private GameState _state;
        private GameData _data;
        private Transform _transform;
        private Camera _mainCamera;

        private void Awake()
        {
            _cellManager = GetComponent<CellManager>();
            _virusManager = GetComponent<VirusManager>();
            _cameraController = GetComponent<CameraController>();
            _uiManager = GetComponent<GameUIManager>();
            _transform = transform;
            _mainCamera = Camera.main;

            InputController.Click += OnClicked;
            InputController.BackClicked += OnBackClicked;

            _cellManager.CellInfected += OnCellInfected;
            _virusManager.VirusKilled += OnVirusKilled;

            _data = new GameData();
        }

        private void Start()
        {
            _state = GameState.Ready;
            _cellManager.StartGame();
        }

        private void Update()
        {
            if (_state != GameState.Playing)
                return;

            _transform.position = _mainCamera.transform.position + Vector3.down * _data.FollowDistance;
            _uiManager.UpdateInfection(_cellManager.InfectionPercent);
            _uiManager.UpdateBlow(_cellManager.BlowPercent);
        }

        private void StartGame()
        {
            _data.Score = 0;
            _uiManager.UpdateScore(_data.Score);
            _virusManager.StartGame();
            _virusManager.SetNextCellFunction(_cellManager.GetNextCell);
            _cameraController.SetVirus(_virusManager.Virus);
            _state = GameState.Playing;
        }

        private void OnCellInfected(int cellIndex)
        {
            _data.Score += cellIndex;
            _uiManager.UpdateScore(_data.Score);
        }

        private void OnVirusKilled()
        {
            _state = GameState.Stopped;
            _cameraController.StopFollow();
            _transform.position = _data.StartPosition;
            _cellManager.StartGame();
        }

        private void OnClicked()
        {
            if (_state == GameState.Playing)
                return;

            StartGame();
        }

        private void OnBackClicked()
        {
            Application.Quit();
        }
    }
}