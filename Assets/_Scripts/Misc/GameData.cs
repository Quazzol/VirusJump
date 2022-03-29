using UnityEngine;

namespace Misc
{
    public class GameData
    {
        public int Score { get; set; }
        public Vector2 StartPosition => _startPosition;
        public float FollowDistance => 10f;

        private Vector2 _startPosition = new Vector2(0, -10f);
    }
}