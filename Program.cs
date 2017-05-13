using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pacman;

namespace Pacman
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            // Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            var form = new GameForm(LevelFactory.Levels);
            Application.Run(form);
        }
    }
}


/*
 в GameForm
  надо после вывода винформы прекращать игру,
  а еще нормально выводить форму для лузера
  как сделать бэкграунд?

  настроить переключение уровней
  почему во втором уровне монстр натыкаясь на монетку вызывает ошибку? кстати не всегда

    и кнопки сделать


    наделала кучу фигни в Gameform, Winnerform, сделала Level в Game статиком, 
    хочу менять уровни при нажатии кнопки в виннерформе; надо сделать Reset.


  */
 