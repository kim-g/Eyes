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
        public static SQLite.SQLiteConfig Config;
        public static SQLite.SQLiteLanguage Texts;
 

        public MainForm()
        {
            InitializeComponent();
            Config = SQLite.SQLiteConfig.Open("config.db");
            Texts = SQLite.SQLiteLanguage.Open("language.db", Config.GetConfigValue("language"));

            // Настройка всех надписей
            GetPhotoButton.Text = Texts.GetText("main form", "get photo");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void GetPhotoButton_Click(object sender, EventArgs e)
        {
            Image Snapshot = Camera_Form.GetPhoto(this);

            if (Snapshot != null)
                pictureBox1.Image = Snapshot;
        }
    }
}
