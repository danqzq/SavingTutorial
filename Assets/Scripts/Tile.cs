using UnityEngine;

namespace Dan
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private int _id;
        
        public TileData Data => new TileData
        {
            id = _id,
            position = transform.position.ToVector2Int()
        };
    }
}