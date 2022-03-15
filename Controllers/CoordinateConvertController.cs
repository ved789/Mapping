using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Mapping.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoordinateConvertController : Controller
    {
        private static readonly string[] Summaries = new[]
        {
            "WGS84", "BNG", "EPSG2200", "EPSG2200", "EPSG2100", "EPSG2300", "EPSG2400", "EPSG2500", "EPSG2600", "EPSG2700"
        };

        private readonly ILogger<CoordinateConvertController> _logger;

        public CoordinateConvertController(ILogger<CoordinateConvertController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Coordinate> Get()
        {
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new Coordinate
            {
                FromMapPoints = GetRandomMapPoints(),
                ToMapPoints = GetRandomMapPoints(),
                FromWkt = Summaries[rng.Next(Summaries.Length)],
                ToWkt = Summaries[rng.Next(Summaries.Length)]
            })
                .ToArray();
        }

        private double[] GetRandomMapPoints()
        {
            var rng = new Random();
            double[] mapPoints = { rng.Next(-360, 360), rng.Next(-90, 90) };
            return mapPoints;
        }
    }
}
