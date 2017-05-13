using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pacmam;

namespace Pacman
{
    public class Player : ICreature
    {
        public Level Level;
        public Point Location;
        public string ImageFileName { get; set; } = "Player.png";
        public int DrawingPriority { get; set; } = 15;

        public Player(Level level)
        {
            Level = level;
        }
            
        CreatureCommand ICreature.Act(int x, int y)
        {
            var width = Level.MapWidth;
            var height = Level.MapHeight;
            var key = Level.KeyPressed;
            var deltaX = GetOffX(key);
            var deltaY = GetOffY(key);
            if (x + deltaX >= width || x + deltaX < 0 || Level.Map[x + deltaX, y] is Wall)
                deltaX = 0;
            if (y + deltaY >= height || y + deltaY < 0 || Level.Map[x, y + deltaY] is Wall)
                deltaY = 0;
            
            return new CreatureCommand()
            {
                DeltaX = deltaX,
                DeltaY = deltaY,
                TransformTo = this,
            };
        }

        static int GetOffY(Keys key)
        {
            if (key == Keys.S || key == Keys.Down)
                return 1;
            else if (key == Keys.W || key == Keys.Up)
                return -1;
            else
                return 0;
        }

        static int GetOffX(Keys key)
        {
            if (key == Keys.A || key == Keys.Left)
                return -1;
            else if (key == Keys.D || key == Keys.Right)
                return 1;
            else
                return 0;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster)
                return true;
            return false;
        }
    }
}
