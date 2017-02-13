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
        readonly System.Windows.Media.Media3D.PerspectiveCamera _newPerspectiveCamera = new PerspectiveCamera();
        System.Windows.Threading.DispatcherTimer _timer;
        Movement _walk, _strafe;
        private double _steps = 1;
        private Camera _cameraPosition;

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
                _steps = value;
            }
        }

        public Camera CameraPosition
        {
            get { return _cameraPosition; }
            set { _cameraPosition = value; }
        }

        public Movement Walk
        {
            get { return _walk; }
            set { _walk = value; }
        }

        public Movement Strafe
        {
            get { return _strafe; }
            set { _strafe = value; }
        }

        public PerspectiveCamera NewPerspectiveCamera
        {
            get { return _newPerspectiveCamera; }
        }

        public DispatcherTimer Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }
    }
}
