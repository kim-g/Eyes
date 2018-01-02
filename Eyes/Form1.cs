using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eyes
{
    public partial class MainForm : Form
    {
        SQLite.SQLiteConfig Config;
        SQLite.SQLiteLanguage Texts;
        Web_Camera Camera;


        public MainForm()
        {
            InitializeComponent();
            Config = SQLite.SQLiteConfig.Open("config.db");
            Texts = SQLite.SQLiteLanguage.Open("language.db", Config.GetConfigValue("language"));

            Camera = new Web_Camera(0, pictureBox1);
            Camera.Start_Web_Camera(5);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Camera.Stop_Web_Camera();
        }
    }
}
