using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public interface ICreature
    {
        string ImageFileName { get; set; }
        int DrawingPriority { get; set; }
        CreatureCommand Act( int x, int y);
        bool DeadInConflict(ICreature conflictedObject);
    }
}
