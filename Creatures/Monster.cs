using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Monster : ICreature
    {
        public Level Level;
        public Point Location;
        public string ImageFileName { get; set; } = "Repka.png";
        public int DrawingPriority { get; set; } = 11;

        public Monster(Level level)
        {
            Level = level;
        }

        CreatureCommand ICreature.Act(int x, int y)
        {
            var map = Level.Map;
            if (FindPacman() == null)
                return GetCommand(0, 0, this);
            var diggerX = FindPacman()[0];
            var diggerY = FindPacman()[1];
            if (diggerX > x &&
                (map[x + 1, y] == null || map[x + 1, y] is Player))
                return GetCommand(1, 0, this);
            if (diggerX < x &&
                (map[x - 1, y] == null || map[x - 1, y] is Player))
                return GetCommand(-1, 0, this);
            if (diggerY > y &&
                (map[x, y + 1] == null || map[x, y + 1] is Player))
                return GetCommand(0, 1, this);
            if (diggerY < y &&
                (map[x, y - 1] == null || map[x, y - 1] is Player))
                return GetCommand(0, -1, this);
            return GetCommand(0, 0, this);
        }
        CreatureCommand GetCommand(int deltaX, int deltaY, ICreature transformTo)
        {
            return new CreatureCommand()
            {
                DeltaX = deltaX,
                DeltaY = deltaY,
                TransformTo = transformTo,
            };
        }
        int[] FindPacman()
        {
            for (var x = 0; x < Level.MapWidth; x++)
                for (var y = 0; y < Level.MapHeight; y++)
                    if (Level.Map[x, y] is Player)
                        return new int[] { x, y };
            return null;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }
}
