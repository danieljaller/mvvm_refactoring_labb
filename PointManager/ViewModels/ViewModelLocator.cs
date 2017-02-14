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
    }
}
