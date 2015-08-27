using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Schema;
using static Sokoban.Console.Map;

namespace Sokoban.Console
{
    internal class Program
    {
        
        public static Map ThisMap { get; set; }

        public enum Directions
        {
            Up,
            Down,
            Left,
            Right,
        }

        public static void Main(string[] args)
        {
            GameStart:
            ThisMap = new Map(@"C:\Users\brgoz\Documents\GitHub\Sokoban\Sokoban.Console\maps\map1");
          
            while (!ThisMap.IsWon)
            {
                System.Console.Clear();
                System.Console.WriteLine("{0},{1}", ThisMap.PlayerTile.X, ThisMap.PlayerTile.Y);
                System.Console.Write(ThisMap.Render());
                var key = System.Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (!Move(Directions.Left))
                            System.Console.Beep();
                        break;
                    case ConsoleKey.UpArrow:
                        if (!Move(Directions.Up))
                            System.Console.Beep();
                        break;
                    case ConsoleKey.RightArrow:
                        if (!Move(Directions.Right))
                            System.Console.Beep();
                        break;
                    case ConsoleKey.DownArrow:
                        if (!Move(Directions.Down))
                            System.Console.Beep();
                        break;
                }
            }
            System.Console.WriteLine("YOU WIN!!!!! Play again (y/n)?");
            if (System.Console.ReadKey().Key == System.ConsoleKey.Y) goto GameStart;
        }

        private static bool Move(Directions dir)
        {

            Tile sourceTile = ThisMap.PlayerTile;
            Tile targetTile = GetTargetTile(sourceTile, dir);

            if (targetTile.TileChar == Tile.TileTypes["Wall"]) return false;

            if (targetTile.TileChar == Tile.TileTypes["Barrel"] || targetTile.TileChar == Tile.TileTypes["Barrel+Storage"])
            {
                var barrelTargetTile = GetTargetTile(targetTile, dir);
                if (barrelTargetTile.TileChar == Tile.TileTypes["Wall"] || barrelTargetTile.TileChar == Tile.TileTypes["Barrel"] || barrelTargetTile.TileChar == Tile.TileTypes["Barrel+Storage"]) return false;
                if (sourceTile.TileChar == Tile.TileTypes["Player"])
                {
                    sourceTile.TileChar = Tile.TileTypes["Empty"];
                }
                else if (sourceTile.TileChar == Tile.TileTypes["Player+Storage"])
                {
                    sourceTile.TileChar = Tile.TileTypes["Storage"];
                }
                if (targetTile.TileChar == Tile.TileTypes["Barrel"])
                {
                    targetTile.TileChar = Tile.TileTypes["Player"];
                }
                if (targetTile.TileChar == Tile.TileTypes["Barrel+Storage"])
                {
                    targetTile.TileChar = Tile.TileTypes["Player+Storage"];
                }
                if (barrelTargetTile.TileChar == Tile.TileTypes["Empty"])
                {
                    barrelTargetTile.TileChar = Tile.TileTypes["Barrel"];
                }
                if (barrelTargetTile.TileChar == Tile.TileTypes["Storage"])
                {
                    barrelTargetTile.TileChar = Tile.TileTypes["Barrel+Storage"];
                }

                ThisMap.GetTileAtPosition(barrelTargetTile.X, barrelTargetTile.Y).TileChar = barrelTargetTile.TileChar;
            }


            if (sourceTile.TileChar == Tile.TileTypes["Player"])
            {
                sourceTile.TileChar = Tile.TileTypes["Empty"];
                targetTile.TileChar = targetTile.TileChar == Tile.TileTypes["Storage"]
                    ? Tile.TileTypes["Player+Storage"]
                    : Tile.TileTypes["Player"];
            }

            else if (sourceTile.TileChar == Tile.TileTypes["Player+Storage"])
            {
                sourceTile.TileChar = Tile.TileTypes["Storage"];
                targetTile.TileChar = targetTile.TileChar == Tile.TileTypes["Storage"]
                    ? Tile.TileTypes["Player+Storage"]
                    : Tile.TileTypes["Player"];
            }

            ThisMap.GetTileAtPosition(sourceTile.X, sourceTile.Y).TileChar = sourceTile.TileChar;
            ThisMap.GetTileAtPosition(targetTile.X, targetTile.Y).TileChar = targetTile.TileChar;
            return true;
        }

        public static Tile GetTargetTile(Tile currentTile, Directions dir)
        {
            switch (dir)
            {
                case Directions.Left:
                    return ThisMap.GetTileAtPosition(currentTile.X - 1, currentTile.Y);
                case Directions.Right:
                    return ThisMap.GetTileAtPosition(currentTile.X + 1, currentTile.Y);
                case Directions.Down:
                    return ThisMap.GetTileAtPosition(currentTile.X, currentTile.Y + 1);
                case Directions.Up:
                    return ThisMap.GetTileAtPosition(currentTile.X, currentTile.Y - 1);
            }
            return null;
        }
    }
}
