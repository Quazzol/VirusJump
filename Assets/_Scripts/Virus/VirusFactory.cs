using System.Collections.Generic;
using UnityEngine;

namespace Virus
{
    public static class VirusFactory
    {
        private static List<VirusController> _virusPrefabs = new List<VirusController>();
        static VirusFactory()
        {
            _virusPrefabs.AddRange(Resources.LoadAll<VirusController>("Virus"));
        }

        public static VirusController GetVirus()
        {
            return _virusPrefabs[Random.Range(0, _virusPrefabs.Count)].Get<VirusController>();
        }
    }
}