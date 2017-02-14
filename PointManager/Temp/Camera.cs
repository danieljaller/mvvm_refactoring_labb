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
        public Point3D Position { get { return _position; } set { _position = value; } }
        public double DegreeHorizontal { get { return _degreeHorizontal; } set { _degreeHorizontal = AngleInterval(value); OnPropertyChanged();} }
        public double DegreeVertical { get { return _degreeVertical; } set { _degreeVertical = AngleInterval(value); OnPropertyChanged();} }

        public double X { get { return _position.X; } set { _position.X = value; OnPropertyChanged(nameof(X));} }
        public double Y { get { return _position.Y; } set { _position.Y = value; OnPropertyChanged();} }
        public double Z { get { return _position.Z; } set { _position.Z = value; OnPropertyChanged();} }

        public Point3D Look
        {
            get
            {
                const int dist = 3;
                double X1 = Math.Sin(DegreeHorizontal * halfPi) * dist, Z1 = Math.Cos(DegreeHorizontal * halfPi) * dist;
                return new Point3D() { Y = (Math.Sin(DegreeVertical * halfPi) * dist), Z = (Math.Cos(DegreeVertical * halfPi) * Z1), X = (Math.Cos(DegreeVertical * halfPi) * X1) };
            }
        }

        public void Move(double Distance) { _position.X += Math.Sin(DegreeHorizontal * halfPi) * Distance; _position.Z += Math.Cos(DegreeHorizontal * halfPi) * Distance; }
        public void Strafe(double Distance)
        {
            var dx = Math.Sin(DegreeHorizontal * halfPi) * Distance;
            var dz = Math.Cos(DegreeHorizontal * halfPi) * Distance;
            _position.X +=-dz;
            _position.Z +=dx;
        }
        private const double halfPi = Math.PI / 180;
        private Point3D _position;
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