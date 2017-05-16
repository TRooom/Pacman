using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public class Level
    {
        public Keys KeyPressed { get; set; }
        public string Name;
        public ICreature[,] Map { get; set; }
        public static readonly int MapWidth = 30;
        public static readonly int MapHeight = 15;
        public readonly int NumberOfCoins;
        public Image LevelImage;
        private readonly Func<Level,ICreature[,]> init;

        public Level(string name, int numberOfCoins, Func<Level,ICreature[,]> init)
        {
            this.init = init;
            Map = init(this);
            Name = name;
            NumberOfCoins = numberOfCoins;
        }

        public void Reset()
        {
            Map = init(this);
        }
    }
}
