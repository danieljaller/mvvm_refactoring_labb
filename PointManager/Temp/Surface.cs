namespace Maze
{

    //MODEL
    public class Surface
    {
        public Surface() { }
        public Surface(double x1, double z1, double x2, double z2) { X1 = x1; Z1 = z1; X2 = x2; Z2 = z2; }
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Z1 { get; set; }
        public double Z2 { get; set; }
    }
}