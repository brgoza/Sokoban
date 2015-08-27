using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Sokoban.Console
{
    public class Map
    {
        public List<Tile> MapTiles { get; set; }

        public Tile PlayerTile
        {
            get { return MapTiles.Find(x => x.TileChar == Tile.TileTypes["Player"] || x.TileChar==Tile.TileTypes["Player+Storage"]); }
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsWon
        {
            get { return (MapTiles.All(x => x.TileChar != Tile.TileTypes["Storage"])); }
        }

        public Map(string filename)
        {
            MapTiles = LoadFromFile(filename, this);
        }

        private static List<Tile> LoadFromFile(string filename, Map thisMap)
        {
            List<Tile> result = new List<Tile>();
            List<string> lines = System.IO.File.ReadLines(filename).ToList();
            thisMap.Width = lines.Max(x => x.Length);
            thisMap.Height = lines.Count;
            for (int y = 0; y < thisMap.Height; y++)
            {
                for (int x = 0; x < thisMap.Width; x++)
                {
                    char tileChar = lines[y][x];
                    result.Add(new Tile(x, y, tileChar));
                }
            }
            return result;
        }

        public Tile GetTileAtPosition(int x, int y)
        {
            return MapTiles.Find(tile => tile.X == x && tile.Y == y);
        }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex < Height; rowIndex++)
            {
                for (int colIndex = 0; colIndex < Width; colIndex++)
                {
                    sb.Append(GetTileAtPosition(colIndex, rowIndex).TileChar);
                }
                sb.Append("\n\r");
            }
            return sb.ToString();
        }
    }
}
