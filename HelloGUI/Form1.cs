using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloGUI
{
    public partial class mainFrame: Form
    {
        public mainFrame()
        {
            InitializeComponent();
        }

        private void btnClickMe_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;

            if (name == "")
            {
                MessageBox.Show("Please enter your name first!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }         

            string result = "Hello " + name + "!";
            tbResult.Text = result;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to close it?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("See you again!", "Exit");
                this.Close();
            }
        }
    }
}
