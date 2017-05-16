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
using Pacmam;

namespace Pacman
{
    public partial class GameForm : Form
    {    
        private readonly Dictionary<string, Bitmap> bitmaps;
        private const string imagesDirectory = "C:\\Users\\DrRoo\\Documents\\Visual Studio 2015\\Projects\\Pacman\\Pacman\\images";
        private GameState gameState;
        private bool isFinished;
        private const int elementSize = 50;
        private int tickCount;
        private int stage;

        public GameForm(Level level = null)
        {
            bitmaps = new Dictionary<string, Bitmap>();
            level = level ?? LevelFactory.Levels[stage];
            StartNewGame(level);
            var timer = new Timer { Interval = 10 };
            timer.Tick += TimerTick;
            timer.Start();
            var top = 10;
            foreach (var l in LevelFactory.Levels)
                top += AddLevel(l, top) + 10;
            this.ClientSize = new Size(elementSize*Level.MapWidth,
               (Level.MapHeight+1) * elementSize);
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Text = "Pacman";
            this.DoubleBuffered = true;

            var dir = new DirectoryInfo(imagesDirectory);
            foreach (var e in dir.GetFiles("*.png"))
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);
        }

        public int AddLevel(Level l,int top)
        {
            var link = new LinkLabel
            {
                Text = l.Name,
                Left = 10,
                Top = top,
                BackColor = Color.Transparent,
            };
            link.LinkClicked += (sender, args) => StartNewGame(l);
            link.Parent = this;
            Controls.Add(link);
            return link.PreferredHeight;
        }

        private void StartNewGame(Level level)
        {
            level.Reset();
            this.gameState = new GameState(level);
            this.isFinished = false;
            this.tickCount = 0;
        }

        public void RepeatLevel()
        {
            gameState.IsPlayerDead = false;
            gameState.currentLevel.Reset();
            isFinished = false;
            tickCount = 0;
        }

        public void NextLevel()
        {
            stage++;
            if (stage > LevelFactory.Levels.Count - 1)
                this.Close(); //Конец игры
            else
                StartNewGame(LevelFactory.Levels[stage]);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            gameState.currentLevel.KeyPressed = e.KeyCode;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, elementSize);
            e.Graphics.FillRectangle(Brushes.DarkSeaGreen, 0, 0, elementSize*Level.MapWidth,
                elementSize*Level.MapHeight);
            foreach (var a in gameState.Animations)
            {
                var fileName = a.Creature.ImageFileName;
                var loc = a.Location;
                e.Graphics.DrawImage(bitmaps[fileName], loc);
            }
            e.Graphics.ResetTransform();
            e.Graphics.DrawString(gameState.Scores.ToString(), new Font("Arial", 16), Brushes.Green, 100, 10);
        }

        

        private void TimerTick(object sender, EventArgs args)
        {
            if (isFinished)
                return;
            if (gameState.currentLevel.NumberOfCoins == gameState.Scores)
            {
                this.Hide();
                isFinished = true;
                var form = new WinnerForm(this);
                form.ShowDialog();
            }
            if (gameState.IsPlayerDead)
            {
                this.Hide();
                isFinished = true;
                var form = new LooserForm(this);
                form.ShowDialog();
            }
            if (tickCount == 0) gameState.BeginAct();
            foreach (var e in gameState.Animations)
                e.Location = new Point(e.Location.X + 4*e.Command.DeltaX, e.Location.Y + 4*e.Command.DeltaY);
            if (tickCount == 7)
                gameState.EndAct();
            tickCount++;
            if (tickCount == 8) tickCount = 0;
            Invalidate();
            Update();
        }
    }

}
