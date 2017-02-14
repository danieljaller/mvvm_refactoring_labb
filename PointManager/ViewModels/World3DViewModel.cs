using System;
using System.Collections.Generic;
using System.Linq;
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
        private Camera _cameraPosition;
        private string _textX;
        private string _textY;
        private string _textZ;
        private string _textV;
        private string _textH;

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

        public Camera CameraPosition
        {
            get { return _cameraPosition; }
            set { _cameraPosition = value; OnPropertyChanged();}
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
            set { _timer = value; OnPropertyChanged();}
        }

        public string TextH
        {
            get { return _textH; }
            set { _textH = value; OnPropertyChanged();}
        }

        public string TextV
        {
            get { return _textV; }
            set { _textV = value; OnPropertyChanged();}
        }

        public string TextZ
        {
            get { return _textZ; }
            set { _textZ = value; OnPropertyChanged();}
        }

        public string TextY
        {
            get { return _textY; }
            set { _textY = value; OnPropertyChanged();}
        }

        public string TextX
        {
            get { return _textX; }
            set { _textX = value; OnPropertyChanged();}
        }

        public void PrintCameraData()
        {
            
            TextX = CameraPosition.X.ToString();
            
            TextY = CameraPosition.Y.ToString();
            
            TextZ = CameraPosition.Z.ToString();
            
            TextV = CameraPosition.DegreeVertical.ToString();
            
            TextH = CameraPosition.DegreeHorizontal.ToString();
        }
    }
}
