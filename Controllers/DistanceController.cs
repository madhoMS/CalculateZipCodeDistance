using FindDistance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FindDistance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly IMongoCollection<ZipCodes> _ZipCodesCollection;

        #region Constr
        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceController"/> class.
        /// </summary>
        /// <param name="databaseSettings"></param>
        public DistanceController(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _ZipCodesCollection = mongoDatabase.GetCollection<ZipCodes>(databaseSettings.Value.ZipCodeCollectionName);
        }
        #endregion

        #region Methods
        /// <summary>
        /// This endpoint used to get distance between two zipcodes
        /// </summary>
        /// <param name="from">from zipcode</param>
        /// <param name="to">to zipcode</param>
        /// <returns></returns>
        [HttpGet("{from}/{to}")]
        public async Task<IActionResult> GetDistance(long from, long to)
        {
            try
            {
                //get Latitude and Longitude detail using zip code
                var fromData = await _ZipCodesCollection.Find(x => x.ZIP == from).FirstOrDefaultAsync();
                if (fromData is null) return NotFound();

                var toData = await _ZipCodesCollection.Find(x => x.ZIP == to).FirstOrDefaultAsync();
                if (toData is null) return NotFound();

                //calculate distance between two zipcodes.
                var distanse = FindDistance(fromData.Latitude, toData.Latitude, fromData.Longitude, toData.Longitude);

                var msg = $"The distance between {fromData.City} ({fromData.ZIP}) to {toData.City} ({toData.ZIP}) is {distanse} miles";
                return Ok(new
                {
                    Status = StatusCodes.Status200OK,
                    Message = msg,
                    Distance = distanse
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion


        #region Function to find Distance
        /// <summary>
        /// calculate radians
        /// </summary>
        /// <param name="angleIn10thofaDegree"></param>
        /// <returns></returns>
        static double ToRadians(double angleIn10thofaDegree)
        {
            // Angle in 10th of a degree
            return (angleIn10thofaDegree *
                           Math.PI) / 180;
        }
        /// <summary>
        /// function used to calculate distance
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon1"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        static double FindDistance(double lat1,
                               double lat2,
                               double lon1,
                               double lon2)
        {

            // The math module contains a function named toRadians
            // which converts from degrees to radians.
            lon1 = ToRadians(lon1);
            lon2 = ToRadians(lon2);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in kilometers. Use 3956 (4059)for miles and 6371 for KM.
            double r = 3956;

            // calculate the result
            var result = (c * r);
            return Math.Round((Double)result, 3);
        }

        #endregion
    }
}
