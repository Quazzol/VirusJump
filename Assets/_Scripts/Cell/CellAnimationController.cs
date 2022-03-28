using UnityEngine;
using Misc;
using System.Threading.Tasks;
using System;

namespace Cell
{
    public class CellAnimationController
    {
        public event Action Infected;
        public bool CancelToken = false;

        private SpriteRenderer _renderer;
        private float _infectionPeriod = 2f;

        public CellAnimationController(SpriteRenderer renderer)
        {
            _renderer = renderer;
        }

        public void Initialize(float infectionPeriod)
        {
            _infectionPeriod = infectionPeriod;
        }

        public void ChangeMaterialState(CellState state)
        {
            switch (state)
            {
                case CellState.Normal: NormalState(); break;
                case CellState.Infecting: InfectingState(); break;
                case CellState.Infected: InfectedState(); break;
            }
        }

        private void NormalState()
        {
            SetWaveSpeed(1f);
            SetWaveInfection(0f);
        }

        private void InfectingState()
        {
            SetWaveSpeed(2f);
            GetInfectedOverTime(Color.white, Color.green, _infectionPeriod);
        }

        private void InfectedState()
        {
            SetWaveSpeed(.5f);
        }

        private void SetWaveInfection(float infection)
        {
            _renderer?.material?.SetFloat("_Infection", infection);
        }

        private void SetWaveSpeed(float speed)
        {
            _renderer?.material?.SetFloat("_Speed", speed);
        }

        private async void GetInfectedOverTime(Color startingColor, Color endingColor, float time)
        {
            // to pass first Time.fixedDeltaTime
            await Task.Yield();

            float inversedTime = 1 / time;
            for (float step = 0.0f; step <= 1.0f; step += Time.deltaTime * inversedTime)
            {
                if (CancelToken)
                    return;

                SetWaveInfection(step);
                await Task.Yield();
            }

            Infected?.Invoke();
        }
    }
}