using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    class User
    {
        // attributes
        private string username;
        private string password;
        private string gender;

        // properties
        public string Username { get => username; private set => username = value; }
        public string Password { get => password; private set => password = value; }
        public string Gender { get => gender; private set => gender = value; }

        // constructor
        public User(string username, string password, string gender)
        {
            this.username = username;
            this.password = password;
            this.gender = gender;
        }
    }

    public partial class Form1: Form
    {
        List<User> users;

        public Form1() // constructor
        {
            InitializeComponent();
            users = new List<User>();
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter your username and password first!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username == "sysadmin" && password == "P@ssw0rd")
            {
                MessageBox.Show("Login successful!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                foreach (User user in users)
                {
                    if (user.Username == username && user.Password == password)
                    {
                        MessageBox.Show("Login successful!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                MessageBox.Show("Login failed!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cbAgreement_CheckedChanged(object sender, EventArgs e)
        {
            btnSignUp.Enabled = cbAgreement.Checked;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            string gender;

            if (rbMale.Checked)
            {
                gender = "Male";
            } else if (rbFemale.Checked)
            {
                gender = "Female";
            } else
            {
                gender = "Unknown";
            }

            User newUser = new User(username, password, gender);

            users.Add(newUser);

            MessageBox.Show("Sign up successful! Your username is " + username + ".", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cbAgreement.Checked = false;
        }
    }
}
