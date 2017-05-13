using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Coin : ICreature
    {
        public string ImageFileName { get; set; } = "Coin.png";
        public int DrawingPriority { get; set; } = 10;
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
            return true;
        }
    }
}
