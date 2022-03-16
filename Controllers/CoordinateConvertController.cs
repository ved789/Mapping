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
            "mapinfo:coordsys 8,79,7,-2,49,0.9996012717,400000,-100000", "mapinfo:coordsys 2001,104,-180,-90,180,90"
        };

        private readonly ILogger<CoordinateConvertController> _logger;

        public CoordinateConvertController(ILogger<CoordinateConvertController> logger)
        {
            _logger = logger;
        }

        [Produces("application/json")]
        [HttpGet]
        public IEnumerable<Coordinate> Get()
        {
            var rng = new Random();

            return Enumerable.Range(1, 2).Select(index => new Coordinate
            {
                FromMapPoints = GetRandomMapPoints(),
                ToMapPoints = GetRandomMapPoints(),
                FromWkt = Summaries[rng.Next(Summaries.Length)],
                ToWkt = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
        }

        [Produces("application/json")]
        [HttpPost]
        public IEnumerable<Coordinate> Post(IEnumerable<Coordinate> request)
        {
            IList<Coordinate> response = new List<Coordinate>();

            foreach (Coordinate coordinate in request)
            {
                response.Add(new Coordinate
                {
                    FromMapPoints = coordinate.FromMapPoints,
                    ToMapPoints = new CoordinateConverter(coordinate.FromWkt).ConvertToCoordSys(coordinate.FromMapPoints, coordinate.ToWkt),
                    FromWkt = coordinate.FromWkt,
                    ToWkt = coordinate.ToWkt
                });
            }

            return response;
        }

        private double[] GetRandomMapPoints()
        {
            var rng = new Random();
            double[] mapPoints = { rng.Next(-360, 360), rng.Next(-90, 90) };
            return mapPoints;
        }
    }
}
