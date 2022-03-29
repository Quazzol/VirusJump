using UnityEngine;
using Misc;
using System.Threading.Tasks;
using System;

namespace Cell
{
    public class CellAnimationController
    {
        public event Action Infected;
        public event Action Blown;
        public bool CancelToken { get; set; }
        public float InfectionPercent { get; private set; }
        public float BlowPercent { get; private set; }

        private SpriteRenderer _renderer;
        private float _infectionPeriod = 2f;
        private float _blowUpPeriod = 5f;

        public CellAnimationController(SpriteRenderer renderer)
        {
            _renderer = renderer;
        }

        public void Initialize(float infectionPeriod)
        {
            CancelToken = false;
            InfectionPercent = 0;
            BlowPercent = 0;
            _infectionPeriod = infectionPeriod;
            if (_blowUpPeriod < _infectionPeriod + 5f)
                _blowUpPeriod = _infectionPeriod + 5f;
        }

        public void ChangeMaterialState(CellState state)
        {
            switch (state)
            {
                case CellState.Normal: NormalState(); break;
                case CellState.Infecting: InfectingState(); break;
                case CellState.Infected: InfectedState(); break;
                case CellState.Blown: BlownState(); break;
            }
        }

        private void NormalState()
        {
            SetWaveSpeed(1f);
            SetWaveInfection(0f);
        }

        private void InfectingState()
        {
            SetWaveSpeed(1.5f);
            GetInfectedOverTime(_infectionPeriod);
        }

        private void InfectedState()
        {
            BlowOverTime(_blowUpPeriod);
        }

        private void BlownState()
        {

        }

        private void SetWaveInfection(float infection)
        {
            _renderer?.material?.SetFloat("_Infection", infection);
        }

        private void SetWaveSpeed(float speed)
        {
            _renderer?.material?.SetFloat("_Speed", speed);
        }

        private async void GetInfectedOverTime(float time)
        {
            // to pass first Time.fixedDeltaTime
            await Task.Yield();

            float inversedTime = 1 / time;
            for (float step = 0.0f; step <= 1.0f; step += Time.deltaTime * inversedTime)
            {
                if (CancelToken)
                    return;

                InfectionPercent = step;
                SetWaveInfection(step);
                await Task.Yield();
            }

            Infected?.Invoke();
        }

        private async void BlowOverTime(float time)
        {
            // to pass first Time.fixedDeltaTime
            await Task.Yield();

            float maxValue = 5f;
            float setStep = 0f;
            float inversedTime = 1 / time;

            for (float step = 0.0f; step <= 1.0f; step += Time.deltaTime * inversedTime)
            {
                if (CancelToken)
                    return;

                setStep += Time.deltaTime * inversedTime;
                BlowPercent = step;
                if (setStep >= .5f)
                {
                    SetWaveSpeed(step * maxValue);
                    setStep = 0;
                }

                await Task.Yield();
            }

            Blown?.Invoke();
        }
    }
}