using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacmam;

namespace Pacman
{
    public static class LevelFactory
    {
        public static List<Level> Levels;

        static LevelFactory()
        {
            Levels = new List<Level> {GetEasyLevel(),GetMediumLevel()};
        }

        private static Level GetEasyLevel()
        {
            var name = "EasyMode";
            //var levelImage =
            //    Bitmap.FromFile("C:\\Users\\1\\Desktop\\хакер-хуякер\\sharp\\Pacman\\Pacman\\images\\easymode.png");
            var playerLocation = new Point(5, 5);
            var monsterLocation = new Point(14, 14);
            var map = new ICreature[Level.MapWidth, Level.MapHeight];
            for (var i = 0; i < Level.MapWidth; i++)
                for (var j = 0; j < Level.MapHeight; j++)
                    map[i, j] = new Coin();

            map[1, 1] = new Wall();
            map[1,2] = new Wall();
            map[1,3] = new Wall();
            map[2,1] = new Wall();
            var numberOfCoins = 250;            
            
            return new Level(name, playerLocation, map, numberOfCoins, monsterLocation);
        }

        private static Level GetMediumLevel()
        {
            var name = "SecondCourse";
            var playerLocation = new Point(Level.MapWidth/2, Level.MapHeight/2);
            var monstersLocations = new Point[]
                {new Point(1, 10), new Point(10, 10), new Point(19, 10), new Point(28, 10),};
            var map = new ICreature[Level.MapWidth, Level.MapHeight];
            for (var i = 0; i < Level.MapWidth; i++)
            {
                for (var j = 0; j < 7; j++)
                    map[i, j] = new Coin();
                for (var j = 11; j < Level.MapHeight; j++)
                    map[i, j] = new Coin();
            }
            var numberOfCoins = 250;

            return new Level(name, playerLocation, map, numberOfCoins, monstersLocations);
        }

    }
}
