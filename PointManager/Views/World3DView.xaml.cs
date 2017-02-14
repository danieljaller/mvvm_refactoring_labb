using PointManager.Temp;
using PointManager.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Camera = PointManager.Temp.Camera;

namespace PointManager.Views
{
    /// <summary>
    /// Interaction logic for World3DView.xaml
    /// </summary>
    public partial class World3DView : UserControl
    {

        public World3DView()
        {

            InitializeComponent();
            Focus();
        }


        //Command?
        public void Window1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: ViewModelLocator.World3DViewModel.Walk = World3DViewModel.Movement.Positive; break;
                case Key.Down: ViewModelLocator.World3DViewModel.Walk = World3DViewModel.Movement.Negative; break;
                case Key.Left: ViewModelLocator.World3DViewModel.Strafe = World3DViewModel.Movement.Negative; break;
                case Key.Right: ViewModelLocator.World3DViewModel.Strafe = World3DViewModel.Movement.Positive; break;
                case Key.Z: ViewModelLocator.World3DViewModel.Camera.Y += 0.1; break;
                case Key.X: ViewModelLocator.World3DViewModel.Camera.Y -= 0.1; break;
            }
        }

        //Command?
        public void Window1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: ViewModelLocator.World3DViewModel.Walk = World3DViewModel.Movement.None; break;
                case Key.Down: ViewModelLocator.World3DViewModel.Walk = ViewModelLocator.World3DViewModel.Walk = World3DViewModel.Movement.None; break;
                case Key.Left: ViewModelLocator.World3DViewModel.Strafe = World3DViewModel.Movement.None; break;
                case Key.Right: ViewModelLocator.World3DViewModel.Strafe = World3DViewModel.Movement.None; break;
            }
        }


        //Command?
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ViewModelLocator.World3DViewModel.Walk != World3DViewModel.Movement.None)
                ViewModelLocator.World3DViewModel.Camera.Move(
                    (double)ViewModelLocator.World3DViewModel.Walk * ViewModelLocator.World3DViewModel.Steps * 0.1);
            if (ViewModelLocator.World3DViewModel.Strafe != World3DViewModel.Movement.None)
                ViewModelLocator.World3DViewModel.Camera.Strafe(
                    (double)ViewModelLocator.World3DViewModel.Strafe * ViewModelLocator.World3DViewModel.Steps * 0.1);
            ViewModelLocator.World3DViewModel.NewPerspectiveCamera.Position = ViewModelLocator.World3DViewModel.Camera.Position;
            ViewModelLocator.World3DViewModel.NewPerspectiveCamera.LookDirection = new Vector3D(
                ViewModelLocator.World3DViewModel.Camera.Look.X, ViewModelLocator.World3DViewModel.Camera.Look.Y, 
                ViewModelLocator.World3DViewModel.Camera.Look.Z);
        }

        //ViewModel
        private void Window1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Viewport3D1.Camera = ViewModelLocator.World3DViewModel.NewPerspectiveCamera;
            ViewModelLocator.World3DViewModel.Camera = new Camera() { X = 1, Y = 0.5, Z = 0 };
            ViewModelLocator.World3DViewModel.NewPerspectiveCamera.Position = ViewModelLocator.World3DViewModel.Camera.Position;
            ViewModelLocator.World3DViewModel.NewPerspectiveCamera.LookDirection =
                new Vector3D(ViewModelLocator.World3DViewModel.Camera.Look.X, ViewModelLocator.World3DViewModel.Camera.Look.Y, ViewModelLocator.World3DViewModel.Camera.Look.Z);
            (new MazeGenerator()).MakeMaze(Model3Dgroup);
            ViewModelLocator.World3DViewModel.Timer = new System.Windows.Threading.DispatcherTimer();
            ViewModelLocator.World3DViewModel.Timer.Interval = TimeSpan.FromMilliseconds(16);
            ViewModelLocator.World3DViewModel.Timer.Tick += new EventHandler(Timer_Tick);
            ViewModelLocator.World3DViewModel.Timer.Start();
        }


        //Command/Behaviour
        private void Window1_MouseMove(object sender, MouseEventArgs e) { ViewModelLocator.World3DViewModel.SetCameraAngles(e.GetPosition(null), this); }
    }
}
