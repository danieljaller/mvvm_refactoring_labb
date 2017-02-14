using System.Windows;
using System.Windows.Input;
using PointManager.Views;

namespace PointManager
{
    public partial class MainWindow : Window
    {
        private World3DView world3DView;
        public MainWindow()
        {
            InitializeComponent();
            world3DView = UCWorld3DView;
        }

        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            world3DView.Window1_KeyDown(sender, e);
        }

        public void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            world3DView.Window1_KeyUp(sender, e);
        }
    }
}