
namespace Mapping
{
    public class CoordinateSystemSet
    {
        public CoordinateSystemSet(string miSrsId, string miSrsName, string wkt, bool isProjected)
        {
            MiSrsId = miSrsId;
            MiSrsName = miSrsName;
            Wkt = wkt;
            IsProjected = isProjected;
        }

        public string MiSrsId { get; }
        public string MiSrsName { get; }
        public string Wkt { get; }
        public bool IsProjected { get; }
    }
}