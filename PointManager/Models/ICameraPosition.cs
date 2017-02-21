namespace PointManager.Models
{
    public interface ICameraPosition
    {
        double cameraDegH { get; set; }
        double cameraDegV { get; set; }
        double cameraX { get; set; }
        double cameraY { get; set; }
        double cameraZ { get; set; }
        int Id { get; set; }
        string PositionName { get; set; }
    }
}