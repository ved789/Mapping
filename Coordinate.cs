using System;

namespace Mapping
{
    public class Coordinate
    {
        public double[] FromMapPoints { get; set; }

        public double[] ToMapPoints { get; set; }

        public string FromWkt { get; set; }

        public string ToWkt { get; set; }
    }
}