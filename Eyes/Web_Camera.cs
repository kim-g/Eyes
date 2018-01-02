using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eyes
{
    public class Web_Camera
    {
        private FilterInfoCollection videoDevices;
        public VideoCaptureDevice videoDevice;
        private VideoCapabilities[] videoCapabilities;
        private VideoCapabilities[] snapshotCapabilities;
        bool RenderingAFrame = false;
        bool RenderFrame = true;
        private PictureBox picFrame;
        private Image WebCamVideo;
        Bitmap bitmap;

        public Web_Camera(int WebCameraN, PictureBox PicFrame)
        {
            // enumerate video devices
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            // Проверка наличия камер
            if (videoDevices.Count < 1)
            {
                MessageBox.Show("Для правильной работы ваш компьютер должени иметь веб-камеру. В настоящий момент веб-камера не подключена или неисправна.\n\nПрограмма не может работать без веб-камеры и вынуждена быть закрыта.", "ОШИБКА");
                Application.Exit();
                return;
            }
            if (WebCameraN >= videoDevices.Count)
            {
                MessageBox.Show("Выбрана не существующая в системе веб-камера.\n\nБудет выбрана веб-камера по-умолчанию.", "ОШИБКА");
            }
            videoDevice = new VideoCaptureDevice(videoDevices[WebCameraN].MonikerString);
            videoDevice.NewFrame += new NewFrameEventHandler(cam_NewFrame);
            videoCapabilities = videoDevice.VideoCapabilities;
            snapshotCapabilities = videoDevice.SnapshotCapabilities;
            picFrame = PicFrame;
        }

        public void Start_Web_Camera(int Resolution)
        {
            if (videoDevice != null)
            {
                if ((videoCapabilities != null) && (videoCapabilities.Length != 0))
                {
                    videoDevice.VideoResolution = videoCapabilities[Resolution];
                }

                videoDevice.Start();
            }
        }

        public void Stop_Web_Camera()
        {
            videoDevice.Stop();
        }

        // Обработка получаемого с камеры кадра
        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Application.DoEvents();
            try
            {
                if (!RenderFrame) return;
                if (RenderingAFrame) return;
                RenderingAFrame = true;
                Bitmap Pic = (Bitmap)eventArgs.Frame.Clone();
                Thread t = new Thread(new ParameterizedThreadStart(ModifyPic));
                t.Start(Pic);
            }
            catch
            {

            }

            Application.DoEvents();
        }

        private void ModifyPic(Object Pic_In)
        {
            try
            {

                bitmap = (Bitmap)Pic_In;

                // Освобождение ресурсов и присваение новых значений.
                Image Temp = WebCamVideo;
                WebCamVideo = (Image)bitmap.Clone();
                Temp.Dispose();

                Graphics g = Graphics.FromImage(bitmap);
                Temp = picFrame.Image;
                picFrame.Image = bitmap;
                Temp.Dispose();
                g.Dispose();
                RenderingAFrame = false;

            }
            catch (Exception e)
            {
                RenderingAFrame = false;
                //MessageBox.Show(e.Message);
                Application.DoEvents();

            }
        }

        Bitmap ResizeBMP(Bitmap sourse_bmp, int x, int y, int width, int height, PixelFormat PF)
        {
            Bitmap destination_bmp = new Bitmap(width, height, PF);
            try
            {
                Graphics g = Graphics.FromImage(destination_bmp);
                g.DrawImage(sourse_bmp, 0, 0, new Rectangle(x, y, x + width, y + height), GraphicsUnit.Pixel);
                g.Dispose();
                sourse_bmp.Dispose();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return destination_bmp;
        }

        public Image GetSnapshot()
        {
            return (Image)WebCamVideo.Clone();
        }

    }
}
