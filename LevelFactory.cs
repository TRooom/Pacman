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
            Func<Level,ICreature[,]> mapInit = (x) =>{
                var map = new ICreature[Level.MapWidth, Level.MapHeight];
                for (var i = 0; i < Level.MapWidth; i++)
                    for (var j = 0; j < Level.MapHeight; j++)
                        map[i, j] = new Coin();
                map[1, 1] = new Wall();
                map[1, 2] = new Wall();
                map[1, 3] = new Wall();
                map[2, 1] = new Wall();

                map[5,5] = new Player(x);
                map[14,14] = new Monster(x);
                return map;
            };
            var numberOfCoins = 50;
            return new Level(name, numberOfCoins, mapInit);
        }

        private static Level GetMediumLevel()
        {
            var name = "SecondCourse";
            var monstersLocations = new Point[]
                {new Point(1, 10), new Point(10, 10), new Point(19, 10), new Point(28, 10)};
            Func<Level, ICreature[,]> mapInit = (x) =>
            {
                var map = new ICreature[Level.MapWidth, Level.MapHeight];

                map[Level.MapWidth / 2, Level.MapHeight / 2] = new Player(x);
                map[1,10] = new Monster(x);
                map[10, 10] = new Monster(x);
                map[19, 10] = new Monster(x);
                map[28, 10] = new Monster(x);
                for (var i = 0; i < Level.MapWidth; i++)
                {
                    for (var j = 0; j < 7; j++)
                        map[i, j] = new Coin();
                    for (var j = 11; j < Level.MapHeight; j++)
                        map[i, j] = new Coin();
                }
                return map;
            };
            var numberOfCoins = 50;

            return new Level(name, numberOfCoins, mapInit);
        }

    }
}
