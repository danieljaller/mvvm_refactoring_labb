using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;
using PointManager.Annotations;

namespace PointManager.Temp
{

    //MODEL?
    public class Camera : INotifyPropertyChanged
    {
        public Point3D Position { get { return _Position; } set { _Position = value; } }
        public double DegreeHorizontal { get { return _degreeHorizontal; } set { _degreeHorizontal = AngleInterval(value); OnPropertyChanged();} }
        public double DegreeVertical { get { return _degreeVertical; } set { _degreeVertical = AngleInterval(value); OnPropertyChanged();} }

        public double X { get { return _Position.X; } set { _Position.X = value; OnPropertyChanged();} }
        public double Y { get { return _Position.Y; } set { _Position.Y = value; OnPropertyChanged();} }
        public double Z { get { return _Position.Z; } set { _Position.Z = value; OnPropertyChanged();} }

        public Point3D Look
        {
            get
            {
                const int dist = 3;
                double X1 = Math.Sin(DegreeHorizontal * halfPi) * dist, Z1 = Math.Cos(DegreeHorizontal * halfPi) * dist;
                return new Point3D() { Y = (Math.Sin(DegreeVertical * halfPi) * dist), Z = (Math.Cos(DegreeVertical * halfPi) * Z1), X = (Math.Cos(DegreeVertical * halfPi) * X1) };
            }
        }

        public void Move(double Distance) { _Position.X += Math.Sin(DegreeHorizontal * halfPi) * Distance; _Position.Z += Math.Cos(DegreeHorizontal * halfPi) * Distance; }
        public void Strafe(double Distance)
        {
            var dx = Math.Sin(DegreeHorizontal * halfPi) * Distance;
            var dz = Math.Cos(DegreeHorizontal * halfPi) * Distance;
            _Position.X +=-dz;
            _Position.Z +=dx;
        }
        private const double halfPi = Math.PI / 180;
        private Point3D _Position;
        private double _degreeHorizontal, _degreeVertical;
        private double AngleInterval(double deg) { if (deg > 360) return deg - 360; if (deg < 0) return deg + 360; return deg; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}