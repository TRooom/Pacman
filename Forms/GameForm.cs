using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Pacman
{
    public partial class GameForm : Form
    {    
        Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private GameState GameState;
        public Game Game;
        private Level currentLevel;
        private readonly Timer timer;
        private int iterationIndex;

        public GameForm(IEnumerable<Level> levels)//Форма должна получать только 1 уровень
        {
            InitializeComponent();          
            //BackgroundImage = levels.ToArray()[0].LevelImage;
            timer = new Timer { Interval = 10 };
            timer.Tick += TimerTick;
            timer.Start();
            var top = 10;
            foreach (var level in levels.ToArray())
            {
                if (currentLevel == null) currentLevel = level;
                var link = new LinkLabel
                {
                    Text = level.Name,
                    Left = 10,
                    Top = top,
                    BackColor = Color.Transparent
                };
                link.LinkClicked += (sender, args) => ChangeLevel(level);
                link.Parent = this;
                Controls.Add(link);
                top += link.PreferredHeight + 10;
            }
            KeyPreview = true;
            Game = new Game(levels);            
            Game.Level.KeyPressed = Keys.D;
            GameState = new GameState(Game);
            ClientSize = new Size(GameState.ElementSize*Level.MapWidth,
                GameState.ElementSize*Level.MapHeight + GameState.ElementSize);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Text = "Pacman";
            DoubleBuffered = true;

            //var imagesDirectory = new DirectoryInfo("C:\\Users\\1\\Desktop\\хакер-хуякер\\sharp\\Pacman\\Pacman\\images");
            //foreach (var e in imagesDirectory.GetFiles("*.png"))
            //    bitmaps[e.Name] = (Bitmap) Bitmap.FromFile(e.FullName);



            timer = new Timer {Interval = 1};
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void ChangeLevel(Level newSpace)
        {
            currentLevel = newSpace;
            currentLevel.Reset();
            timer.Start();
            iterationIndex = 0;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Game.Level.KeyPressed = e.KeyCode;
        }

        protected override void OnKeyUp(KeyEventArgs e)//Уже не нужно
        {
           // Game.Level.KeyPressed = Keys.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, GameState.ElementSize);
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, GameState.ElementSize*Level.MapWidth,
                GameState.ElementSize*Level.MapHeight);
            foreach (var a in GameState.Animations)
            {
                var fileName = a.Creature.ImageFileName;
                var loc = a.Location;
                e.Graphics.DrawImage(bitmaps[fileName], loc);
            }
            e.Graphics.ResetTransform();
            e.Graphics.DrawString(Game.Scores.ToString(), new Font("Arial", 16), Brushes.Green, 100, 10);
        }

        int tickCount = 0;

        private void TimerTick(object sender, EventArgs args)
        {
            if (currentLevel == null) return;
            if (currentLevel.IsCompleted)
                timer.Stop();
            else
                Text = ". Iteration # " + iterationIndex++;
            if (tickCount == 0) GameState.BeginAct();
            foreach (var e in GameState.Animations)
                e.Location = new Point(e.Location.X + 4*e.Command.DeltaX, e.Location.Y + 4*e.Command.DeltaY);
            if (tickCount == 7)
                GameState.EndAct();
            tickCount++;
            if (tickCount == 8) tickCount = 0;
            Invalidate();
            Update();
        }
    }

}
