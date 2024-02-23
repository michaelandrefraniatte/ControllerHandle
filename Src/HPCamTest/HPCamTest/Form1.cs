using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using System.Drawing.Drawing2D;
namespace HPCamTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        private System.Drawing.Point backpoint = new System.Drawing.Point(0, 0);
        public static double camx, camy, irx, iry, watchM = 50, watchM1 = 2, watchM2 = 0;
        private static bool runningoff;
        public static Task taskM;
        public static uint CurrentResolution = 0;
        public static Form1 form = (Form1)Application.OpenForms["Form1"];
        public static List<IntPoint> corners = new List<IntPoint>();
        public static AForge.Math.Geometry.SimpleShapeChecker shapeChecker = new AForge.Math.Geometry.SimpleShapeChecker();
        public static BrightnessCorrection brightnessfilter = new BrightnessCorrection(-50);
        public static ColorFiltering colorfilter = new ColorFiltering();
        public static ExtractBiggestBlob extractfilter = new ExtractBiggestBlob();
        private static BlobCounter blobCounter = new BlobCounter();
        public static BlobsFiltering blobfilter = new BlobsFiltering();
        public static ConnectedComponentsLabeling componentfilter = new ConnectedComponentsLabeling();
        public static Blob[] blobs;
        public static Grayscale grayscalefilter = new Grayscale(1, 0, 0);
        public static EuclideanColorFiltering euclideanfilter = new EuclideanColorFiltering();
        private static Stopwatch diffM = new Stopwatch();
        private static Bitmap img;
        private VideoCapabilities[] videoCapabilities;
        public static System.Collections.Generic.List<double> valListX = new System.Collections.Generic.List<double>(), valListY = new System.Collections.Generic.List<double>();
        public static double sumX = 0f, sumY = 0f;
        private void Start()
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString);
            videoCapabilities = FinalFrame.VideoCapabilities;
            FinalFrame.VideoResolution = videoCapabilities[videoCapabilities.Length - 1];
            FinalFrame.SetCameraProperty(CameraControlProperty.Zoom, 0, CameraControlFlags.Manual);
            FinalFrame.SetCameraProperty(CameraControlProperty.Focus, 0, CameraControlFlags.Manual);
            FinalFrame.SetCameraProperty(CameraControlProperty.Exposure, 0, CameraControlFlags.Manual);
            FinalFrame.SetCameraProperty(CameraControlProperty.Iris, 0, CameraControlFlags.Manual);
            FinalFrame.SetCameraProperty(CameraControlProperty.Pan, 0, CameraControlFlags.Manual);
            FinalFrame.NewFrame += FinalFrame_NewFrame;
            FinalFrame.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            diffM.Start();
            Start();
            System.Threading.Thread.Sleep(1000);
            taskM = new Task(WiiJoy_thrM);
            taskM.Start();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e.KeyData);
        }
        private void OnKeyDown(System.Windows.Forms.Keys keyData)
        {
            if (keyData == System.Windows.Forms.Keys.F1)
            {
                const string message = "• Author: Michaël André Franiatte.\n\r\n\r• Contact: michael.franiatte@gmail.com.\n\r\n\r• Publisher: https://github.com/michaelandrefraniatte.\n\r\n\r• Copyrights: All rights reserved, no permissions granted.\n\r\n\r• License: Not open source, not free of charge to use.";
                const string caption = "About";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (keyData == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
            }
        }
        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            img = (Bitmap)eventArgs.Frame.Clone();
            brightnessfilter.ApplyInPlace(img);
            colorfilter.Red = new IntRange(0, 255);
            colorfilter.Green = new IntRange(205, 255);
            colorfilter.Blue = new IntRange(205, 255);
            colorfilter.ApplyInPlace(img);
            brightnessfilter.ApplyInPlace(img);
            euclideanfilter.CenterColor = new RGB(255, 255, 255);
            euclideanfilter.Radius = 175;
            euclideanfilter.ApplyInPlace(img);
            blobCounter.ProcessImage(img);
            blobs = blobCounter.GetObjectsInformation();
            for (int i = 0; i < blobs.Length; i++)
            {
                shapeChecker.RelativeDistortionLimit = 100f;
                shapeChecker.MinAcceptableDistortion = 20f;
                if (shapeChecker.IsCircle(blobCounter.GetBlobsEdgePoints(blobs[i])))
                {
                    backpoint.X = (int)blobs[i].CenterOfGravity.X;
                    backpoint.Y = (int)blobs[i].CenterOfGravity.Y;
                }
            }
            Bitmap EditableImg = new Bitmap(img);
            EditableImg.MakeTransparent();
            DrawLines(ref EditableImg, backpoint);
            form.pictureBox1.Image = EditableImg;
            camx = (backpoint.X - img.Width / 2f) * 6f;
            camy = (backpoint.Y - img.Height / 2f) * 6f;
        }
        private void DrawLines(ref Bitmap image, System.Drawing.Point p)
        {
            Graphics g = Graphics.FromImage(image);
            Pen p1 = new Pen(Color.Red, 2);
            System.Drawing.Point ph = new System.Drawing.Point(image.Width, p.Y);
            System.Drawing.Point ph2 = new System.Drawing.Point(0, p.Y);
            g.DrawLine(p1, p, ph);
            g.DrawLine(p1, p, ph2);
            ph = new System.Drawing.Point(p.X, 0);
            ph2 = new System.Drawing.Point(p.X, image.Height);
            g.DrawLine(p1, p, ph);
            g.DrawLine(p1, p, ph2);
            g.Dispose();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            runningoff = true;
            System.Threading.Thread.Sleep(1000);
            if (FinalFrame.IsRunning == true)
                FinalFrame.Stop();
            TimeEndPeriod(1);
        }
        private void WiiJoyIR()
        {
            if (FinalFrame.IsRunning != true)
                Start();
            watchM2 = (double)diffM.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
            watchM = (watchM2 - watchM1) / 1000f;
            watchM1 = watchM2;
            if (valListX.Count >= 200 & valListY.Count >= 200)
            {
                valListX.RemoveAt(0);
                valListX.Add(camx);
                irx = valListXAverage(valListX);
                valListY.RemoveAt(0);
                valListY.Add(camy);
                iry = valListYAverage(valListY);
            }
            else
            {
                valListX.Add(0);
                valListY.Add(0);
            }
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2) - irx * (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2) / 1024f), (int)((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2) + iry * (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2) / 1024f));
            System.Threading.Thread.Sleep(1);
        }
        public double valListXAverage(System.Collections.Generic.List<double> valList)
        {
            sumX = 0f;
            foreach (double val in valList)
                sumX += val;
            return sumX / 200f;
        }
        public double valListYAverage(System.Collections.Generic.List<double> valList)
        {
            sumY = 0f;
            foreach (double val in valList)
                sumY += val;
            return sumY / 200f;
        }
        private void WiiJoy_thrM()
        {
            for (; ; )
            {
                if (runningoff)
                    return;
                WiiJoyIR();
            }
        }
    }
}