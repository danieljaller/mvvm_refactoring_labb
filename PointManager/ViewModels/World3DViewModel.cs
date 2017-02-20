using PointManager.Temp;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace PointManager.ViewModels
{
    class World3DViewModel : ViewModelBase
    {
        public enum Movement { Negative = -1, None = 0, Positive = 1 }

        private DispatcherTimer _timer;
        Movement _walk, _strafe;
        private double _steps = 1;
        private ICamera _camera;

        public double Steps
        {
            get { return _steps; }
            set { _steps = value; OnPropertyChanged(); }
        }

        public ICamera Camera
        {
            get { return _camera; }
            set { _camera = value; OnPropertyChanged(); }
        }

        public Movement Walk
        {
            get { return _walk; }
            set { _walk = value; OnPropertyChanged(); }
        }

        public Movement Strafe
        {
            get { return _strafe; }
            set { _strafe = value; OnPropertyChanged(); }
        }

        public PerspectiveCamera NewPerspectiveCamera { get; } = new PerspectiveCamera();

        public DispatcherTimer Timer
        {
            get { return _timer; }
            set { _timer = value; OnPropertyChanged(); }
        }
        public void SetCameraAngles(Point point, UserControl uc)
        {
            var midY = uc.ActualHeight / 2;
            // ned:  360-270.
            if (point.Y > midY)
            {
                var proc = (point.Y - midY) / midY;
                Camera.DegreeVertical = 360 - 90 * proc;
            }
            // Vert: up:  0-90
            if (point.Y < midY)
            {
                var proc = point.Y / midY;
                Camera.DegreeVertical = 90 - 90 * proc;
            }
            var proc2 = point.X / uc.ActualWidth;
            Camera.DegreeHorizontal = 720 - 720 * proc2;
        }

        public void KeyDownHandler(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: Walk = Movement.Positive; break;
                case Key.Down: Walk = Movement.Negative; break;
                case Key.Left: Strafe = Movement.Negative; break;
                case Key.Right: Strafe = Movement.Positive; break;
                case Key.Z: Camera.Y += 0.1; break;
                case Key.X: Camera.Y -= 0.1; break;
            }
        }
        public void KeyUpHandler(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: Walk = Movement.None; break;
                case Key.Down: Walk = Movement.None; break;
                case Key.Left: Strafe = Movement.None; break;
                case Key.Right: Strafe = Movement.None; break;
            }
        }
        public void TimerTickHandler()
        {
            if (Walk != Movement.None)
                Camera.Move(
                    (double)Walk * Steps * 0.1);
            if (Strafe != Movement.None)
                Camera.Strafe(
                    (double)Strafe * Steps * 0.1);
            NewPerspectiveCamera.Position = Camera.Position;
            NewPerspectiveCamera.LookDirection = new Vector3D(
                Camera.Look.X, Camera.Look.Y,
                Camera.Look.Z);
        }


    }
}
