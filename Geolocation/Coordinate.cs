namespace Geolocation
{
    public struct Coordinate
    {
        /// <summary>
        /// Latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public Coordinate(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
