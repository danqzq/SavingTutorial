using UnityEngine;

namespace Dan
{
    public static class Extensions
    {
        public static Vector2Int ToVector2Int(this Vector3 position) =>
            new Vector2Int((int) position.x, (int) position.y);
        
        public static Vector3 ToVector3(this Vector2Int position) =>
            new Vector3(position.x, position.y, 0);
    }
}