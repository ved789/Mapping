using System;

using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.NTv2;

namespace Mapping
{
    public class CoordinateConverter
    {
        #region PrivateMethods
        //public const string MapInfoBritishNationalGrid = "mapinfo:coordsys 8,79,7,-2,49,0.9996012717,400000,-100000";
        //public const string MapInfoWgs84 = "mapinfo:coordsys 2001,104,-180,-90,180,90";
        private readonly string srs;
        private readonly CoordinateSystemFactory coordinateSystemFactory = new CoordinateSystemFactory();
        private readonly CoordinateTransformationFactory coordinateTransformationFactory =
            new CoordinateTransformationFactory();

        private double[] ConvertToCoordSys(double[] mapPoints, CoordinateSystem fromCoordinateSystem, CoordinateSystem toCoordinateSystem)
        {
            GridFile grid = null;
            var inverse = false;
            bool gridApplied = false;
            if (fromCoordinateSystem.AuthorityCode == 27700)
            {
                grid = CoordinateSystemHelper.GetGridFile();
                gridApplied = true;
            }

            if (toCoordinateSystem.AuthorityCode == 27700)
            {
                grid = CoordinateSystemHelper.GetGridFile();
                inverse = true;
                gridApplied = true;
            }

            ICoordinateTransformation coordinateTransformation = grid != null
                ? coordinateTransformationFactory.CreateFromCoordinateSystems(fromCoordinateSystem, toCoordinateSystem, grid, inverse)
                : coordinateTransformationFactory.CreateFromCoordinateSystems(fromCoordinateSystem, toCoordinateSystem);

            try
            {
                return coordinateTransformation.MathTransform.Transform(mapPoints);
            }
            catch (Exception e)
            {
                string errorMsg = "Grid transformation failed: given coordinate outside of grid bounds.";
                if (gridApplied && e.Message == errorMsg)
                {
                    double[] returnValues = { -360, 90 };
                    returnValues[0] = mapPoints[0] < 0 ? -360 : 360;
                    returnValues[1] = mapPoints[1] < 0 ? 90 : -90;
                    return returnValues;
                }

                throw;
            }
        }

        private CoordinateSystem GetCoordinateSystem(string srsString)
        {
            return coordinateSystemFactory.CreateFromWkt(CoordinateSystemHelper.GetWkt(srsString));
        }
        private CoordinateSystem GetWgs84CoordinateSystem()
        {
            return coordinateSystemFactory.CreateFromWkt(GeographicCoordinateSystem.WGS84.ToString());
        }
        #endregion

        public CoordinateConverter(string srsString)
        {
            srs = srsString;
        }

        public double[] ConvertToWgs84(double[] mapPoints)
        {
            return ConvertToCoordSys(mapPoints, GetCoordinateSystem(srs), GetWgs84CoordinateSystem());
        }

        public double[] ConvertFromWgs84(double[] mapPoints)
        {
            return ConvertToCoordSys(mapPoints, GetWgs84CoordinateSystem(), GetCoordinateSystem(srs));
        }

        public double[] ConvertToCoordSys(double[] mapPoints, string fromSrsString, string toSrsString)
        {
            return ConvertToCoordSys(mapPoints, GetCoordinateSystem(fromSrsString), GetCoordinateSystem(toSrsString));
        }

        public double[] ConvertToCoordSys(double[] mapPoints, string toSrsString)
        {
            return ConvertToCoordSys(mapPoints, srs, toSrsString);
        }
    }
}