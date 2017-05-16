using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using Pacmam;
using Pacman;

namespace Pacman
{
    public class GameState
    {
        public int Scores;
        public Level currentLevel;
        public bool IsPlayerDead;
        public List<CreatureAnimation> Animations = new List<CreatureAnimation>();

        public GameState(Level level)
        {
            currentLevel = level;
        }

        public void BeginAct()
        {
            Animations.Clear();
            for (int x = 0; x < Level.MapWidth; x++)
                for (int y = 0; y < Level.MapHeight; y++)
                {
                    var creature = currentLevel.Map[x, y];
                    if (creature == null) continue;
                    var command = creature.Act(x, y);

                    if (x + command.DeltaX < 0 || x + command.DeltaX >= Level.MapWidth || y + command.DeltaY < 0 ||
                        y + command.DeltaY >= Level.MapHeight)
                        throw new Exception($"The object {creature.GetType()} falls out of the game field");

                    Animations.Add(new CreatureAnimation
                    {
                        Command = command,
                        Creature = creature,
                        Location = new Point(x*50 , y*50),
                        TargetLogicalLocation = new Point(x + command.DeltaX, y + command.DeltaY)
                    });
                }
            Animations = Animations.OrderByDescending(z => z.Creature.DrawingPriority).ToList();
        }

        public void EndAct()
        {
            for (int x = 0; x < Level.MapWidth; x++)
                for (int y = 0; y < Level.MapHeight; y++)
                    currentLevel.Map[x, y] = null;
            foreach (var e in Animations)
            {
                var x = e.TargetLogicalLocation.X;
                var y = e.TargetLogicalLocation.Y;
                var nextCreature = e.Command.TransformTo ?? e.Creature;
                if (currentLevel.Map[x, y] == null) currentLevel.Map[x, y] = nextCreature;               
                else
                {
                    if ((e.Creature is Player && currentLevel.Map[x, y] is Monster) || (e.Creature is Monster && currentLevel.Map[x,y] is Player))
                        IsPlayerDead = true;
                    bool newDead = nextCreature.DeadInConflict(currentLevel.Map[x, y]);
                    bool oldDead = currentLevel.Map[x, y].DeadInConflict(nextCreature);
                    if (currentLevel.Map[x, y] is Player && nextCreature is Coin)
                        Scores += 1;
                    if (newDead && oldDead)
                        currentLevel.Map[x, y] = null;
                    else if (!newDead && oldDead)
                        currentLevel.Map[x, y] = nextCreature;
                    else if (!newDead && !oldDead)
                        throw new Exception(
                            $"Существа {nextCreature.GetType().Name} и {currentLevel.Map[x, y].GetType().Name} претендуют на один и тот же участок карты");
                }
            }
        }
    }
}
