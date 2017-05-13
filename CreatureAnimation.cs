using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class CreatureAnimation
    {
        public ICreature Creature;
        public CreatureCommand Command;
        public Point Location;
        public Point TargetLogicalLocation;
    }
}
