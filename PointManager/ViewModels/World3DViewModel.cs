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
        private double _textX;
        private double _textY;
        private double _textZ;
        private double _textV;
        private double _textH;

        public World3DViewModel()
        {

        }

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
            set { _timer = value; OnPropertyChanged(); PrintCameraData(); }
        }

        public double TextH
        {
            get { return _textH; }
            set { _textH = value; OnPropertyChanged();}
        }

        public double TextV
        {
            get { return _textV; }
            set { _textV = value; OnPropertyChanged();}
        }

        public double TextZ
        {
            get { return _textZ; }
            set { _textZ = value; OnPropertyChanged();}
        }

        public double TextY
        {
            get { return _textY; }
            set { _textY = value; OnPropertyChanged();}
        }

        public double TextX
        {
            get { return _textX; }
            set { _textX = value; OnPropertyChanged();}
        }

        public void PrintCameraData()
        {
            
            TextX = Camera.X;
            
            TextY = Camera.Y;
            
            TextZ = Camera.Z;
            
            TextV = Camera.DegreeVertical;
            
            TextH = Camera.DegreeHorizontal;
        }
    }
}
