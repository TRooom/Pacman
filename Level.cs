using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public class Level
    {
        public Keys KeyPressed { get; set; }
        public string Name;
        public readonly Monster[] Monsters;//Информация об этом есть в Map
        public readonly Player Player;//Информация об этом есть в Map
        public ICreature[,] Map { get; set; }
        public static readonly int MapWidth = 30;
        public static readonly int MapHeight = 15;
        public readonly int NumberOfCoins;
        public Image LevelImage;
        public bool IsCompleted => NumberOfCoins == Game.Scores;

        public Level(string name, Point playerLocation, ICreature[,] map, int numberOfCoins, params Point[] monstersLocation )
        {
            Name = name;
            //LevelImage = leveImage;
            NumberOfCoins = numberOfCoins;
            Player = new Player(this) {Location = playerLocation};
            Monsters = new Monster[monstersLocation.Length];
            for (var i = 0; i < Monsters.Length; i++)
                Monsters[i] = new Monster(this) {Location = monstersLocation[i]};

            //прорисовка карты
            Map = map;
            Map[Player.Location.X, Player.Location.Y] = Player;
            for (var i = 0; i < Monsters.Length; i++)
                Map[Monsters[i].Location.X, Monsters[i].Location.Y] = Monsters[i];

        }

        public void Reset()
        {
            
        }
    }
}
