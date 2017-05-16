using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pacman;

namespace Pacmam
{
    public partial class LooserForm : Form
    {
        public LooserForm(GameForm source)
        {
            ClientSize = new Size(500, 665);
            var table = new TableLayoutPanel();
            var label = new Label
            {
                Text = "You lost. Let's try to pass this exam!",
                Dock = DockStyle.Fill,
                //Image = animatedImage,
                Bounds = new Rectangle(0, 0, 500, 500)
            };

            var buttonRepeat = new Button
            {
                Text = "Try again!",
                Dock = DockStyle.Fill,
                Bounds = new Rectangle(0, 550, 500, 50)
            };
            buttonRepeat.Click += (sender, args) => {source.RepeatLevel(); source.Show(); this.Close(); };

            var buttonQuit = new Button
            {
                Text = "Quit game",
                Dock = DockStyle.Fill,
                Bounds = new Rectangle(0, 600, 500, 50)
            };
            buttonQuit.Click += (sender, args) => source.Close();

            table.RowStyles.Clear();
            table.Controls.Add(label);
            table.Controls.Add(buttonRepeat);
            table.Controls.Add(buttonQuit);
            table.Dock = DockStyle.Fill;

            Controls.Add(table);

        }
    }
}
