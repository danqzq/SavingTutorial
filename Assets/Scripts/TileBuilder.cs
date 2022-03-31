using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dan
{
    public class TileBuilder : MonoBehaviour
    {
        [SerializeField] private CrosshairController _crosshairController;
        [SerializeField] private GameObject[] _tilePrefabs;
        
        public static bool CanManuallyBuild { private get; set; } = true;
        
        private int _currentTileIndex;

        private Dictionary<Vector2Int, Tile> _placedTiles;

        public TileData[] GetAllTileData => _placedTiles.Values.Select(tile => tile.Data).ToArray();

        public void BuildTile(TileData tileData)
        {
            var intPos = tileData.position;
            if (_placedTiles.ContainsKey(intPos)) return;
            
            var tile = Instantiate(_tilePrefabs[tileData.id], transform).GetComponent<Tile>();
            tile.transform.position = tileData.position.ToVector3();
            _placedTiles.Add(tileData.position, tile);
        }

        public void DestroyAllTiles()
        {
            foreach (var placedTile in _placedTiles) Destroy(placedTile.Value.gameObject);
            _placedTiles.Clear();
        }
        
        private void BuildTile(Vector3 position)
        {
            if (!CanManuallyBuild) return;
            BuildTile(new TileData(_currentTileIndex, position.ToVector2Int()));
        }
        
        private void DestroyTile(Vector3 position)
        {
            var intPos = position.ToVector2Int();
            if (!_placedTiles.ContainsKey(intPos)) return;
            
            Destroy(_placedTiles[intPos].gameObject);
            _placedTiles.Remove(intPos);
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0)) BuildTile(_crosshairController.GetCrosshairPosition);
            else if (Input.GetMouseButtonDown(1)) DestroyTile(_crosshairController.GetCrosshairPosition);
        }
        
        private void HandleKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                DestroyAllTiles();
            
            for (var i = 1; i <= _tilePrefabs.Length; i++)
                if (Input.GetKeyDown(i.ToString()))
                    _currentTileIndex = i - 1;
        }

        private void Awake()
        {
            _placedTiles = new Dictionary<Vector2Int, Tile>();
        }

        private void Update()
        {
            HandleMouseInput();
            HandleKeyboardInput();
        }
    }
}