using Core.Exceptions;
using System;

namespace Geolocation
{
    public static class GeoCalculator
    {
        private static double EarthRadiusInMiles = 3959.0;
        private static double EarthRadiusInNauticalMiles = 3440.0;
        private static double EarthRadiusInKilometers = 6371.0;
        private static double EarthRadiusInMeters = 6371000.0;

        /// <summary>   
        /// Calculate the distance between two sets of coordinates.
        /// <param name="originLatitude">The latitude of the origin location in decimal notation</param>
        /// <param name="originLongitude">The longitude of the origin location in decimal notation</param>
        /// <param name="destinationLatitude">The latitude of the destination location in decimal notation</param>
        /// <param name="destinationLongitude">The longitude of the destination location in decimal notation</param>
        /// <param name="decimalPlaces">The number of decimal places to round the return value to</param>
        /// <param name="distanceUnit">The unit of distance</param>
        /// <returns>A <see cref="Double"/> value representing the distance in miles from the origin to the destination coordinate</returns>
        /// </summary>
        public static double GetDistance(double originLatitude, double originLongitude, double destinationLatitude, double destinationLongitude, int decimalPlaces = 1, DistanceUnit distanceUnit = DistanceUnit.Miles)
        {
            if (!CoordinateValidator.Validate(originLatitude, originLongitude))
                throw new BusinessException("La longitud debe ser un numero entre -180 y 180.", BusinessExceptionCode.LongitudeOutRange);
            if (!CoordinateValidator.Validate(destinationLatitude, destinationLongitude))
                throw new BusinessException("La latitud debe ser un numero entre -90 y 90.", BusinessExceptionCode.LatitudeOutRange);

            double radius = GetRadius(distanceUnit);
            return Math.Round(
                    radius * 2 *
                    Math.Asin(Math.Min(1,
                                       Math.Sqrt(
                                           (Math.Pow(Math.Sin(originLatitude.DiffRadian(destinationLatitude) / 2.0), 2.0) +
                                            Math.Cos(originLatitude.ToRadian()) * Math.Cos(destinationLatitude.ToRadian()) *
                                            Math.Pow(Math.Sin((originLongitude.DiffRadian(destinationLongitude)) / 2.0),
                                                     2.0))))), decimalPlaces);
        }

        private static double GetRadius(DistanceUnit distanceUnit)
        {
            switch (distanceUnit)
            {
                case DistanceUnit.Kilometers:
                    return EarthRadiusInKilometers;
                case DistanceUnit.Meters:
                    return EarthRadiusInMeters;
                case DistanceUnit.NauticalMiles:
                    return EarthRadiusInNauticalMiles;
                default:
                    return EarthRadiusInMiles;
            }
        }
    }
}
