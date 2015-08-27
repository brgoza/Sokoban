using System.Collections.Generic;

namespace Sokoban.Console
{
    public class Tile
    {

        public static Dictionary<string, char> TileTypes = new Dictionary<string, char>
        {
            {"Empty", ' '},
            {"Wall", '#'},
            {"Barrel", 'O'},
            {"Storage", '.'},
            {"Player+Storage", '*'},
            {"Barrel+Storage", '+'},
            {"Player",'@' }
        };
        public int X { get; set; }
        public int Y { get; set; }
        public char TileChar { get; set; }

        public Tile(int x, int y, char ch)
        {
            X = x;
            Y = y;
            TileChar = ch;
        }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
            TileChar = TileTypes["Empty"];
        }
    }
}