using AForge.Video.DirectShow;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCamAuten
{
    public partial class Form1 : Form
    {
        static readonly CascadeClassifier cascadeClassifier;
        private VideoCaptureDevice videoSource;
        public Form1()
        {
            InitializeComponent();

            var xmlPath = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");
            if (!File.Exists(xmlPath))
            {
                MessageBox.Show($"Arquivo não encontrado: {xmlPath}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // opcional: lançar exceção controlada ou desativar funcionalidade de detecção
                return;
            }

            var videoSources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoSources != null && videoSources.Count>0)
            {
                videoSource = new VideoCaptureDevice(videoSources[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
            }
        }

        private void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

  

        private void btOnOff(object sender, EventArgs evento)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                pictureBox1.Image = null;
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
                dialog.DefaultExt = "jpg";
                dialog.AddExtension = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void abrirImagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog() { Multiselect = false, Filter = "JPEG|*.jpg" })
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(open.FileName);
                }
            }
        }
    }
}
