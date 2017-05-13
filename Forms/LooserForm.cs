using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacmam
{
    public partial class LooserForm : Form
    {
        public LooserForm()
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

            var buttonQuit = new Button()
            {
                Text = "Quit game",
                Dock = DockStyle.Fill,
                Bounds = new Rectangle(0, 600, 500, 50)
            };

            table.RowStyles.Clear();
            table.Controls.Add(label);
            table.Controls.Add(buttonRepeat);
            table.Controls.Add(buttonQuit);
            table.Dock = DockStyle.Fill;

            Controls.Add(table);

        }
    }
}
