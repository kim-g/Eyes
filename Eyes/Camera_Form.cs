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
    public partial class Camera_Form : Form
    {
        Web_Camera Camera;
        Image Result = null;

        public Camera_Form(Form Owner)
        {
            InitializeComponent();
            this.Owner = Owner;
            Text = MainForm.Texts.GetText("camera form", "title");
        }

        public static Image GetPhoto(Form Owner)
        {
            Camera_Form CF = new Camera_Form(Owner);

            CF.Camera = new Web_Camera(MainForm.Config.GetConfigValueInt("CameraNum"), CF.pictureBox1);
            CF.Camera.Start_Web_Camera(MainForm.Config.GetConfigValueInt("CameraResolution"));

            CF.ShowDialog();

            CF.Camera.Stop_Web_Camera();
            return CF.Result;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Result = Camera.GetSnapshot();
            Close();
        }
    }
}
