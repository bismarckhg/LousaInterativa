using System.Drawing; // For Point and Color

namespace LousaInterativa
{
    public class DrawableLine
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Color LineColor { get; set; }
        public int LineWidth { get; set; }

        // Constructor
        public DrawableLine(Point startPoint, Point endPoint, Color lineColor, int lineWidth)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.LineColor = lineColor;
            this.LineWidth = lineWidth;
        }
    }
}
