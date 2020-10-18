using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArcheryTuningTool
{
    /// <summary>
    /// Custom shape for fletched arrow graphic
    /// </summary>
    class ArrowGraphic : Shape
    {
        public ArrowGraphic()
        {

        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                Point p00 = new Point(-1.5d, 0.0d);
                Point p03 = new Point(1.5d, 0.0d);
                Point p01 = new Point(-1.0d, 7.0d);
                Point p02 = new Point(1.0d, 7.0d);

                Point p05 = new Point(5.83d, -5.5d);
                Point p04 = new Point(6.83d, -3.5d);
                Point p06 = new Point(0.0d, -1.0d);
                Point p08 = new Point(-6.83d, -3.5d);
                Point p07 = new Point(-5.83d, -5.5d);

                /*Point p0 = new Point(0.0d, 0.0d);
                Point p1 = new Point(0.0d, 5.0d);
                Point p2 = new Point(4.33d, -2.5d);
                Point p3 = new Point(-4.33d, -2.5d);*/

                List<PathFigure> figures = new List<PathFigure>(1);
                List<PathSegment> segments = new List<PathSegment>(6);
                
                segments.Add(new LineSegment(p01, true));
                //PathFigure pf = new PathFigure(p0, segments, false);
                //figures.Add(pf);
                //segments.Clear();

                segments.Add(new LineSegment(p02, true));
                //pf = new PathFigure(p0, segments, false);
                //figures.Add(pf);
                //segments.Clear();

                segments.Add(new LineSegment(p03, true));
                //pf = new PathFigure(p0, segments, false);
                //figures.Add(pf);

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
}
