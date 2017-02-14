using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointManager.ViewModels
{
    class ViewModelLocator : ViewModelBase
    {
        public static World3DViewModel World3DViewModel { get; } = new World3DViewModel();

        public static MainViewModel MainViewModel { get; } = new MainViewModel();

        public static PointNavigationViewModel PointNavigationViewModel { get; } = new PointNavigationViewModel();
    }
}
