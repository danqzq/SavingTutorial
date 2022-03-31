using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dan
{
    public class MapManager : MonoBehaviour
    {
        private const string MapFolder = "Maps";
        
        [SerializeField] private TileBuilder _tileBuilder;
        
        [SerializeField] private GameObject _mapButtonPrefab;
        [SerializeField] private Transform _mapListScrollRectTransform;
        
        public string MapNameInput { get; set; }

        public void SaveMap()
        {
            var map = new Map(_tileBuilder.GetAllTileData);
            SaveManager.Save(MapFolder, MapNameInput, map);
            LoadAllMaps();
            TextLogger.Log("Map saved", LogType.Success);
        }

        public void SaveMapExternally()
        {
            var map = new Map(_tileBuilder.GetAllTileData);
            if (SaveManager.SaveToSelectedPath(map))
            {
                LoadAllMaps();
                TextLogger.Log("Map saved", LogType.Success);
                return;
            }
            TextLogger.Log("Failed to save map", LogType.Error);
        }
        
        public void LoadExternally()
        {
            var map = SaveManager.LoadSelectedFile<Map>(out var mapName);
            if (string.IsNullOrEmpty(mapName))
            {
                TextLogger.Log("Failed to load map", LogType.Error);
                return;
            }
            BuildMap(map, mapName);
        }

        private void LoadAllMaps()
        {
            CleanUpMapList();
            var (names, maps) = SaveManager.LoadAllFromDirectoryWithNames<Map>(MapFolder);
            for (var i = 0; i < maps.Length; i++)
            {
                var map = maps[i];
                var mapName = names[i];
                var button = Instantiate(_mapButtonPrefab, _mapListScrollRectTransform);

                button.GetComponentInChildren<TextMeshProUGUI>().text = mapName;
                button.GetComponent<Button>().onClick.AddListener(() => BuildMap(map, mapName));
            }
        }
        
        private void BuildMap(Map map, string mapName)
        {
            _tileBuilder.DestroyAllTiles();
            foreach (var tileData in map.tiles) _tileBuilder.BuildTile(tileData);
            TextLogger.Log($"Loaded map: {mapName}", LogType.Success);
        }
        
        private void CleanUpMapList()
        {
            foreach (Transform child in _mapListScrollRectTransform) Destroy(child.gameObject);
        }

        private void Start()
        {
            LoadAllMaps();
            TextLogger.Log("Loaded all map files from Maps", LogType.Success);
        }
    }
}