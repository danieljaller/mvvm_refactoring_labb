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
using PointManager.ViewModels;
using Camera = PointManager.Temp.Camera;

namespace PointManager.Views
{
    /// <summary>
    /// Interaction logic for World3DView.xaml
    /// </summary>
    public partial class World3DView : UserControl
    {
        private World3DViewModel viewModel;
        public World3DView()
        {
            viewModel = new World3DViewModel();
            InitializeComponent();
            
        }

        //ViewModel
        private void PrintCameraData()
        {
            TextX.Text = (Math.Round(viewModel.CameraPosition.X, 2)).ToString();
            TextY.Text = (Math.Round(viewModel.CameraPosition.Y, 2)).ToString();
            TextZ.Text = (Math.Round(viewModel.CameraPosition.Z, 2)).ToString();
            TextV.Text = (Math.Round(viewModel.CameraPosition.DegreeVertical, 2)).ToString();
            TextH.Text = (Math.Round(viewModel.CameraPosition.DegreeHorizontal, 2)).ToString();
        }


        //Command?
        private void Window1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: viewModel.Walk = World3DViewModel.Movement.Positive; break;
                case Key.Down: viewModel.Walk = World3DViewModel.Movement.Negative; break;
                case Key.Left: viewModel.Strafe = World3DViewModel.Movement.Negative; break;
                case Key.Right: viewModel.Strafe = World3DViewModel.Movement.Positive; break;
                case Key.Z: viewModel.CameraPosition.Y += 0.1; break;
                case Key.X: viewModel.CameraPosition.Y -= 0.1; break;
            }
        }

        //Command?
        private void Window1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: viewModel.Walk = World3DViewModel.Movement.None; break;
                case Key.Down: viewModel.Walk = viewModel.Walk = World3DViewModel.Movement.None; break;
                case Key.Left: viewModel.Strafe = World3DViewModel.Movement.None; break;
                case Key.Right: viewModel.Strafe = World3DViewModel.Movement.None; break;
            }
        }


        //Command?
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (viewModel.Walk != World3DViewModel.Movement.None) viewModel.CameraPosition.Move((double)viewModel.Walk * viewModel.Steps * 0.1);
            if (viewModel.Strafe != World3DViewModel.Movement.None) viewModel.CameraPosition.Strafe((double)viewModel.Strafe * viewModel.Steps * 0.1);
            viewModel.NewPerspectiveCamera.Position = viewModel.CameraPosition.Position;
            viewModel.NewPerspectiveCamera.LookDirection = new Vector3D(viewModel.CameraPosition.Look.X, viewModel.CameraPosition.Look.Y, viewModel.CameraPosition.Look.Z);
            PrintCameraData();
        }

        //ViewModel
        private void Window1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Viewport3D1.Camera = viewModel.NewPerspectiveCamera;
            viewModel.CameraPosition = new Camera() { X = 1, Y = 0.5, Z = 0 }; //CameraPososition.degreeHorizontal = CameraPososition.degreeVertical =0;
            viewModel.NewPerspectiveCamera.Position = viewModel.CameraPosition.Position;
            viewModel.NewPerspectiveCamera.LookDirection = new Vector3D(viewModel.CameraPosition.Look.X, viewModel.CameraPosition.Look.Y, viewModel.CameraPosition.Look.Z);
            (new MazeGenerator()).MakeMaze(Model3Dgroup);
            viewModel.Timer = new System.Windows.Threading.DispatcherTimer();
            viewModel.Timer.Interval = TimeSpan.FromMilliseconds(16);
            viewModel.Timer.Tick += new EventHandler(Timer_Tick);
            this.viewModel.Timer.Start();
        }


        //ViewModel
        private void SetCameraAngles(Point point)
        {
            var midY = this.ActualHeight / 2;
            // ned:  360-270.
            if (point.Y > midY)
            {
                var proc = (point.Y - midY) / midY;
                viewModel.CameraPosition.DegreeVertical = 360 - 90 * proc;
            }
            // Vert: up:  0-90
            if (point.Y < midY)
            {
                var proc = point.Y / midY;
                viewModel.CameraPosition.DegreeVertical = 90 - 90 * proc;
            }
            var proc2 = point.X / this.ActualWidth;
            viewModel.CameraPosition.DegreeHorizontal = 720 - 720 * proc2;
        }


        //Command/Behaviour
        private void Window1_MouseMove(object sender, MouseEventArgs e) { SetCameraAngles(e.GetPosition(null)); }
    }
}
