using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacman;

namespace Pacmam
{
    public class Wall: ICreature
    {
        public string ImageFileName { get; set; } = "Wall.png";
        public int DrawingPriority { get; set; } = 25;
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand()
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = this,
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }
}
