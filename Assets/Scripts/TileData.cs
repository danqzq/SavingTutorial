using UnityEngine;

namespace Dan
{
    [System.Serializable]
    public struct TileData
    {
        public int id;
        public Vector2Int position;
        
        public TileData(int id, Vector2Int position)
        {
            this.id = id;
            this.position = position;
        }
    }
}