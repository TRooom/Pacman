using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pacmam;
using Pacman;

namespace Pacman
{
    public class GameState
    {
        public Game Game;//Лишнее
        public int ElementSize = 50; //Перенести в GameForm
        public List<CreatureAnimation> Animations = new List<CreatureAnimation>();

        public GameState(Game game) //Лишнее
        {
            Game = game;
        }

        public void BeginAct()// Метод слишком большой, подумать как разбить на разные
        {
            Animations.Clear();
            for (int x = 0; x < Level.MapWidth; x++)
                for (int y = 0; y < Level.MapHeight; y++)
                {
                    var creature = Game.Level.Map[x, y];
                    if (creature == null) continue;
                    var command = creature.Act(x, y);

                    if (x + command.DeltaX < 0 || x + command.DeltaX >= Level.MapWidth || y + command.DeltaY < 0 ||
                        y + command.DeltaY >= Level.MapHeight)
                        throw new Exception($"The object {creature.GetType()} falls out of the game field");

                    Animations.Add(new CreatureAnimation//Реализовать конструктор
                    {
                        Command = command,
                        Creature = creature,
                        Location = new Point(x * ElementSize, y * ElementSize),
                        TargetLogicalLocation = new Point(x + command.DeltaX, y + command.DeltaY)
                    });
                }
            Animations = Animations.OrderByDescending(z => z.Creature.DrawingPriority).ToList();
        }

        public void EndAct()//Слишком большой метод, подумать как разбить на несколько малньких
        {
            for (int x = 0; x < Level.MapWidth; x++)
                for (int y = 0; y < Level.MapHeight; y++)
                    Game.Level.Map[x, y] = null;
            foreach (var e in Animations)
            {
                var x = e.TargetLogicalLocation.X;
                var y = e.TargetLogicalLocation.Y;
                var nextCreature = e.Command.TransformTo == null ? e.Creature : e.Command.TransformTo;
                if (Game.Level.Map[x, y] == null) Game.Level.Map[x, y] = nextCreature;               
                else
                {
                    bool newDead = nextCreature.DeadInConflict(Game.Level.Map[x, y]);
                    bool oldDead = Game.Level.Map[x, y].DeadInConflict(nextCreature);
                    if (Game.Level.Map[x, y] is Player && nextCreature is Coin)
                        Game.Scores += 1;

                    if (Game.Level.NumberOfCoins == Game.Scores)// Эту логику перенести в GameForm
                    {
                        var form = new WinnerForm(Game.Level);
                        form.Activate();
                        form.ShowDialog();    
                        break;                        
                    }
                    if (newDead && oldDead)
                        Game.Level.Map[x, y] = null;
                    else if (!newDead && oldDead)
                        Game.Level.Map[x, y] = nextCreature;
                    else if (!newDead && !oldDead)
                        throw new Exception(string.Format(
                            "Существа {0} и {1} претендуют на один и тот же участок карты", nextCreature.GetType().Name,
                            Game.Level.Map[x, y].GetType().Name));
                /*    if (Game.Level.Map[x, y] is Monster && nextCreature is Monster)
                    {
                        var form = new LooserForm();
                        form.Activate();
                        form.ShowDialog();
                        break;
                    }*/
                }
            }
        }
    }
}
