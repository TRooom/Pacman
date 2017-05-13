using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public partial class WinnerForm : Form
    {

        Bitmap animatedImage = new Bitmap(@"C:\Users\1\Desktop\хакер-хуякер\sharp\Pacman\Pacman\images\гифф.gif"); //500x500 pix
        bool currentlyAnimating = false;
        public Level Level;

        public WinnerForm(Level level)
        {
            Level = level;
            InitializeComponent();
            ClientSize = new Size(500, 665);
            var table = new TableLayoutPanel();
            var label = new Label
            {
                Text = "Congratulations! Level complete!",                
                Dock = DockStyle.Fill,
                Image = animatedImage,
                Bounds = new Rectangle(0, 0, 500, 500)
            };
           
            var buttonNextLevel = new Button
            {
                Text = "Next Level",
                Dock = DockStyle.Fill,
                Bounds = new Rectangle(0, 500, 500, 50),                
            };
          //  buttonNextLevel.Click += (sender, args) => ChangeLevel(level);

            var buttonRepeat = new Button
            {
                Text = "Repeat",
                Dock = DockStyle.Fill,
                Bounds = new Rectangle(0, 550, 500, 50)
            };

            var buttonQuit = new Button()
            {
                Text = "Quit game",
                Dock = DockStyle.Fill,
                Bounds = new Rectangle(0, 600, 500, 50)
            };

            table.RowStyles.Clear();
            table.Controls.Add(label);
            table.Controls.Add(buttonNextLevel);
            table.Controls.Add(buttonRepeat);
            table.Controls.Add(buttonQuit);
            table.Dock = DockStyle.Fill;
            
            Controls.Add(table);            

        }

        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            AnimateImage();
            ImageAnimator.UpdateFrames();
            e.Graphics.DrawImage(this.animatedImage, new Point(0, 0));
        }
    }
}
