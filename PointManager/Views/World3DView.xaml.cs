using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PointManager.Temp;
using Camera = PointManager.Temp.Camera;

namespace PointManager.Views
{
    /// <summary>
    /// Interaction logic for World3DView.xaml
    /// </summary>
    public partial class World3DView : UserControl
    {
        public World3DView() { InitializeComponent();}

        private enum Movement { Negative = -1, None = 0, Positive = 1 }
        readonly System.Windows.Media.Media3D.PerspectiveCamera _newPerspectiveCamera = new PerspectiveCamera();
        System.Windows.Threading.DispatcherTimer _timer;
        Movement _walk, _strafe;
        private double _steps = 1;
        private Camera _cameraPosition;

        //ViewModel
        private void PrintCameraData()
        {
            TextX.Text = (Math.Round(_cameraPosition.X, 2)).ToString();
            TextY.Text = (Math.Round(_cameraPosition.Y, 2)).ToString();
            TextZ.Text = (Math.Round(_cameraPosition.Z, 2)).ToString();
            TextV.Text = (Math.Round(_cameraPosition.DegreeVertical, 2)).ToString();
            TextH.Text = (Math.Round(_cameraPosition.DegreeHorizontal, 2)).ToString();
        }


        //Command?
        private void Window1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: _walk = Movement.Positive; break;
                case Key.Down: _walk = Movement.Negative; break;
                case Key.Left: _strafe = Movement.Negative; break;
                case Key.Right: _strafe = Movement.Positive; break;
                case Key.Z: _cameraPosition.Y += 0.1; break;
                case Key.X: _cameraPosition.Y -= 0.1; break;
            }
        }

        //Command?
        private void Window1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: _walk = Movement.None; break;
                case Key.Down: _walk = _walk = Movement.None; break;
                case Key.Left: _strafe = Movement.None; break;
                case Key.Right: _strafe = Movement.None; break;
            }
        }


        //Command?
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_walk != Movement.None) _cameraPosition.Move((double)_walk * _steps * 0.1);
            if (_strafe != Movement.None) _cameraPosition.Strafe((double)_strafe * _steps * 0.1);
            _newPerspectiveCamera.Position = _cameraPosition.Position;
            _newPerspectiveCamera.LookDirection = new Vector3D(_cameraPosition.Look.X, _cameraPosition.Look.Y, _cameraPosition.Look.Z);
            PrintCameraData();
        }

        //ViewModel
        private void Window1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Viewport3D1.Camera = _newPerspectiveCamera;
            _cameraPosition = new Camera() { X = 1, Y = 0.5, Z = 0 }; //CameraPososition.degreeHorizontal = CameraPososition.degreeVertical =0;
            _newPerspectiveCamera.Position = _cameraPosition.Position;
            _newPerspectiveCamera.LookDirection = new Vector3D(_cameraPosition.Look.X, _cameraPosition.Look.Y, _cameraPosition.Look.Z);
            (new MazeGenerator()).MakeMaze(Model3Dgroup);
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(16);
            _timer.Tick += new EventHandler(Timer_Tick);
            this._timer.Start();
        }


        //ViewModel
        private void SetCameraAngles(Point point)
        {
            var midY = this.ActualHeight / 2;
            // ned:  360-270.
            if (point.Y > midY)
            {
                var proc = (point.Y - midY) / midY;
                _cameraPosition.DegreeVertical = 360 - 90 * proc;
            }
            // Vert: up:  0-90
            if (point.Y < midY)
            {
                var proc = point.Y / midY;
                _cameraPosition.DegreeVertical = 90 - 90 * proc;
            }
            var proc2 = point.X / this.ActualWidth;
            _cameraPosition.DegreeHorizontal = 720 - 720 * proc2;
        }


        //Command/Behaviour
        private void Window1_MouseMove(object sender, MouseEventArgs e) { SetCameraAngles(e.GetPosition(null)); }
    }
}
