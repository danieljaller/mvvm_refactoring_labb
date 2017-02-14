using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            set { _camera = value; OnPropertyChanged();}
        }

        public Movement Walk
        {
            get { return _walk; }
            set { _walk = value; OnPropertyChanged();}
        }

        public Movement Strafe
        {
            get { return _strafe; }
            set { _strafe = value; OnPropertyChanged();}
        }

        public PerspectiveCamera NewPerspectiveCamera { get; } = new PerspectiveCamera();

        public DispatcherTimer Timer
        {
            get { return _timer; }
            set { _timer = value; OnPropertyChanged(); }
        }

    }
}
