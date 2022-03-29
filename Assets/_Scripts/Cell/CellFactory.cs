using System.Collections.Generic;
using UnityEngine;

namespace Cell
{
    public class CellFactory
    {
        private static Dictionary<int, CellController> _cellPrefabs = new Dictionary<int, CellController>();

        static CellFactory()
        {
            var prefabs = Resources.LoadAll<CellController>("Cell");
            foreach (var prefab in prefabs)
            {
                var parse = prefab.name.Split('-');
                if (parse.Length != 2)
                    continue;
                _cellPrefabs.Add(int.Parse(parse[1]), prefab);
            }
        }

        public static CellController GetRandomCell()
        {
            return _cellPrefabs[Random.Range(0, _cellPrefabs.Count)].Get<CellController>(null, true);
        }

        public static CellController GetCell(int difficulty)
        {
            difficulty = Mathf.Clamp(difficulty, 0, _cellPrefabs.Count - 1);
            return _cellPrefabs[difficulty].Get<CellController>(null, true);
        }
    }
}