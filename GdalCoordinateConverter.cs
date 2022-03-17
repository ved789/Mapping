using OSGeo.OSR;

namespace Mapping
{
    public class GdalCoordinateConverter
    {
        SpatialReference src = new SpatialReference("");
        SpatialReference dst = new SpatialReference("");
        private string miCoordSys;

        public GdalCoordinateConverter(string miCoordSys)
        {
            this.miCoordSys = miCoordSys;
        }

        public double[] ConvertToWgs84(double[] point)
        {
            src.ImportFromMICoordSys(miCoordSys);
            dst.ImportFromMICoordSys("CoordSys Earth Projection 1, 104 Bounds (-180, -90) (180, 90)");
            return Convert(point);
        }

        public double[] ConvertFromWgs84(double[] point)
        {
            dst.ImportFromMICoordSys(miCoordSys);
            src.ImportFromMICoordSys("CoordSys Earth Projection 1, 104 Bounds (-180, -90) (180, 90)");
            return Convert(point);
        }

        public double[] ConvertToCoordSys(double[] point, string toCoordSys)
        {
            src.ImportFromMICoordSys(miCoordSys);
            dst.ImportFromMICoordSys(toCoordSys);

            return Convert(point);
        }

        private double[] Convert(double[] point)
        {
            CoordinateTransformation ct = new CoordinateTransformation(src, dst);
            var points = new[] { point[0], point[1] };
            ct.TransformPoint(points);
            return points;
        }
    }
}
