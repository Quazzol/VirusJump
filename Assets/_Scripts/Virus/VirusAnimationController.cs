using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Virus
{
    public class VirusAnimationController
    {
        public event Action Dissolved;

        public bool CancelToken;
        private readonly Material _material;
        private readonly Animator _animator;

        public VirusAnimationController(Animator animator, Material material)
        {
            _animator = animator;
            _material = material;
        }

        public void Initialize()
        {
            SetFadeValue(1);
        }

        public async void Dissolve()
        {
            float fade = 1;

            while (fade > 0)
            {
                if (CancelToken)
                    return;

                fade -= Time.deltaTime;
                SetFadeValue(fade);
                await Task.Yield();
            }

            Dissolved?.Invoke();
        }

        private void SetFadeValue(float fade)
        {
            _material?.SetFloat("_Fade", fade);
        }
    }
}