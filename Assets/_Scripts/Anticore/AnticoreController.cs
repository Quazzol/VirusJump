using Pooling;
using UnityEngine;
using Virus;

namespace Anticore
{
    public class AnticoreController : PooledMonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.TryGetComponent<VirusController>(out var virus))
                return;

            virus.Kill();
        }
    }
}