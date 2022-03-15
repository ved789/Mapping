using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json;

using ProjNet.NTv2;

namespace Mapping
{
    public static class CoordinateSystemHelper
    {
        private static List<CoordinateSystemSet> coordinateSystemSet;
        private static List<KeyValuePair<string, string>> cacheCoordinateSystemSet = new List<KeyValuePair<string, string>>();
        private static GridFile ostn02;

        static CoordinateSystemHelper()
        {
            LoadCoordinates();
        }

        private static void LoadCoordinates()
        {
            Stream stream = null;
            try
            {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Mapping.Binaries.CoordinateSystemSet.json");
                if (stream == null)
                    return;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    coordinateSystemSet = JsonConvert.DeserializeObject<List<CoordinateSystemSet>>(json);
                }
            }
            finally
            {
                stream?.Dispose();
            }
        }

        public static GridFile GetGridFile()
        {
            return ostn02 ?? (ostn02 = GridFile.Open(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mapping.Binaries.OSTN02_NTv2.gsb"), true));
        }

        public static string GetWkt(string srsString)
        {
            var result = cacheCoordinateSystemSet.Where(kvp => kvp.Key == srsString);
            if (result.Count() == 1)
            {
                return result.First().Value;
            }

            string wkt = coordinateSystemSet.Find(x => x.MiSrsId == srsString.Substring("mapinfo:".Length))?.Wkt;
            if (cacheCoordinateSystemSet.Count < 2)
            {
                cacheCoordinateSystemSet.Add(new KeyValuePair<string, string>(srsString, wkt));
            }

            return string.IsNullOrEmpty(wkt) ? throw new Exception("Unable to fetch WKT of: " + srsString) : wkt;
        }
    }
}