using System.Windows.Media.Media3D;

namespace PointManager.Temp
{
    public interface ICamera
    {
        Point3D Position { get; set; }
        Point3D Look { get; }
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
        double DegreeVertical { get; set; }
        double DegreeHorizontal { get; set; }
        void Move(double distance);
        void Strafe(double distance);

    }
}
