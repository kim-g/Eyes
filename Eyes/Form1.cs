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
        Image FrontImage;
        Image FrontView;
        int ImageScale = 100;
        Point ImagePozition = new Point(0,0);
        DragObject FrontDrag;

        public MainForm()
        {
            InitializeComponent();
            Config = SQLite.SQLiteConfig.Open("config.db");
            Texts = SQLite.SQLiteLanguage.Open("language.db", Config.GetConfigValue("language"));

            // Настройка всех надписей
            GetPhotoButton.Text = Texts.GetText("main form", "get photo");
            FrontView = new Bitmap(Front.Width, Front.Height);
            FrontDrag = new DragObject();
            Front.MouseWheel += Front_MouseWheel;
        }

        private void Front_MouseWheel(object sender, MouseEventArgs e)
        {
            ImageScale += e.Delta / 12;
            SetFrontDrag();
            ImagePozition = FrontDrag.MoveOnceOuter(0, 0);
            RedrawFrontImage();
            Front.Refresh();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void GetPhotoButton_Click(object sender, EventArgs e)
        {
            Image Snapshot = Camera_Form.GetPhoto(this);

            if (Snapshot != null)
                FrontImage = Snapshot;
            ImageScale = Math.Min(FrontView.Width * 100 / FrontImage.Width, FrontView.Height * 100 / FrontImage.Height);
            StatusLabel.Text = ImageScale.ToString() + "%";

            SetFrontDrag();
            RedrawFrontImage();
            Front.Refresh();
        }

        private void SetFrontDrag()
        {
            FrontDrag.SetOwnerSize(Front.Width, Front.Height);
            FrontDrag.SetObjectSize(FrontImage.Width * ImageScale / 100, FrontImage.Height * ImageScale / 100);
        }

        private void RedrawFrontImage()
        {
            Graphics Draw = Graphics.FromImage(FrontView);
            Draw.FillRectangle(new SolidBrush(Color.Black), 0, 0, FrontView.Width, FrontView.Height);
            Draw.DrawImage(FrontImage, new RectangleF(ImagePozition.X, ImagePozition.Y, FrontImage.Width * ImageScale / 100, FrontImage.Height * ImageScale / 100));
        }

        private void Front_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(FrontView, new PointF(0, 0));
        }

        private void Front_MouseDown(object sender, MouseEventArgs e)
        {
            FrontDrag.StartDrag(e.X, e.Y);
        }

        private void Front_MouseMove(object sender, MouseEventArgs e)
        {
            if (ImagePozition.Move(FrontDrag.MoveOuter(e.X, e.Y)))
            {
                RedrawFrontImage();
                Front.Refresh();
            }
            
        }

        private void Front_MouseUp(object sender, MouseEventArgs e)
        {
            FrontDrag.StopDrag();
        }
    }
}
