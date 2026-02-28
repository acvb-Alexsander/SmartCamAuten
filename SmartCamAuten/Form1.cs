using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace SmartCamAuten
{
    public partial class Form1 : Form
    {
        private VideoCaptureDevice videoSource;
        public Form1()
        {
            InitializeComponent();

            var videoSources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoSources != null && videoSources.Count>0)
            {
                videoSource = new VideoCaptureDevice(videoSources[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
            }
        }

        private void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pbWebcam.Image = (Bitmap)eventArgs.Frame.Clone();
        }

  

        private void btOnOff(object sender, EventArgs evento)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                pbWebcam.Image = null;
            }
            else
            {
                videoSource.Start();
            }
        }

        private void btCapture_Click(object sender, EventArgs evento)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.DefaultExt = "png";
                dialog.AddExtension = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pbWebcam.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}
