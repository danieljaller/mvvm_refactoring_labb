using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using Camera = PointManager.Temp.Camera;

namespace PointManager.ViewModels
{
    class World3DViewModel : ViewModelBase
    {
        public enum Movement { Negative = -1, None = 0, Positive = 1 }

        private DispatcherTimer _timer;
        Movement _walk, _strafe;
        private double _steps = 1;
        private Camera _camera;

        public double Steps
        {
            get
            {
                return _steps;
            }

            set
            {
                _steps = value; OnPropertyChanged();
            }
        }

        public Camera Camera
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
                ViewModelLocator.World3DViewModel.Camera.DegreeVertical = 360 - 90 * proc;
            }
            // Vert: up:  0-90
            if (point.Y < midY)
            {
                var proc = point.Y / midY;
                ViewModelLocator.World3DViewModel.Camera.DegreeVertical = 90 - 90 * proc;
            }
            var proc2 = point.X / uc.ActualWidth;
            ViewModelLocator.World3DViewModel.Camera.DegreeHorizontal = 720 - 720 * proc2;
        }

    }
}
