namespace Dan
{
    [System.Serializable]
    public struct Map
    {
        public TileData[] tiles;
        
        public Map(TileData[] tiles)
        {
            this.tiles = tiles;
        }
    }
}