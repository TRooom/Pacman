using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public class Game
    {
        //TODO Перенести информацию об очках и уровне в GameSate, информацию об уровнях - в LevelFactory, от класса Game избавиться
        public static int Scores { get; set; }
        // public bool IsOver { get; set; }
        // public static Keys KeyPressed { get; set; }       
        public Level Level { get; set; }

        public Game(IEnumerable<Level> levels)
        {
            Level = levels.ToArray()[0];
        }
        
    }
}
