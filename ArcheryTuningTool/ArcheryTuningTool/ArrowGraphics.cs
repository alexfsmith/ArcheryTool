using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArcheryTool
{
    /// <summary>
    /// Custom shape for fletched arrow graphic
    /// </summary>
    class FletchedGraphic : Shape
    {
        public FletchedGraphic(Point mouse)
        {
            Width = 10;
            Height = 10;
            Fill = Brushes.White;
            Stroke = Brushes.Black;
            StrokeThickness = 1;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Margin = new Thickness(mouse.X, mouse.Y, 0, 0);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                Point p00 = new Point(-1.5d, 0.0d);
                Point p01 = new Point(-1.0d, 7.0d);
                Point p02 = new Point(1.0d, 7.0d);
                Point p03 = new Point(1.5d, 0.0d);
                Point p04 = new Point(6.83d, -3.5d);
                Point p05 = new Point(5.83d, -5.5d);
                Point p06 = new Point(0.0d, -1.0d);
                Point p07 = new Point(-5.83d, -5.5d);
                Point p08 = new Point(-6.83d, -3.5d);

                List<PathFigure> figures = new List<PathFigure>(1);
                List<PathSegment> segments = new List<PathSegment>(6);
                
                segments.Add(new LineSegment(p01, true));
                segments.Add(new LineSegment(p02, true));
                segments.Add(new LineSegment(p03, true));
                segments.Add(new LineSegment(p04, true));
                segments.Add(new LineSegment(p05, true));
                segments.Add(new LineSegment(p06, true));
                segments.Add(new LineSegment(p07, true));
                segments.Add(new LineSegment(p08, true));

                PathFigure pf = new PathFigure(p00, segments, true);
                figures.Add(pf);

                Geometry g = new PathGeometry(figures);

                return g;
            }
        }
    }

    /// <summary>
    /// Custom shape for bareshaft arrow graphic
    /// </summary>
    class BareshaftGraphic : Shape
    {
        public BareshaftGraphic(Point mouse)
        {
            Fill = Brushes.White;
            Stroke = Brushes.Black;
            StrokeThickness = 1;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Margin = new Thickness(mouse.X, mouse.Y, 0, 0);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {

                Geometry g = new EllipseGeometry(new Point(0,0),5,5);

                return g;
            }
        }
    }
}
