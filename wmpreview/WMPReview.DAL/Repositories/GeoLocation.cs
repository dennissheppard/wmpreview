using System;

namespace WMPReview.DAL.Repositories
{  /**
 * <p>Represents a point on the surface of a sphere. (The Earth is almost
 * spherical.)</p>
 *
 * <p>To create an instance, call one of the static methods fromDegrees() or
 * fromRadians().</p>
 *
 * <p>This code was originally published at
 * <a href="http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates#Java">
 * http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates#Java</a>.</p>
 *
 * @author Jan Philip Matuschek
 * @version 22 September 2010
     * 
     *ported to C# by jackson hayes 12/13/2014
 */
    public class GeoLocation
    {

        public static double earthRadius = 6371.01;

        public static double ConvertMilesToKilometers(double miles)
        {
            //
            // Multiply by this constant and return the result.
            //
            return miles * 1.609344;
        }

        public static double ConvertKilometersToMiles(double kilometers)
        {
            //
            // Multiply by this constant.
            //
            return kilometers * 0.621371192;
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            return degrees*Math.PI/180;
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            return radians*180/Math.PI;
        }

        private double radLat; // latitude in radians
        private double radLon; // longitude in radians
        private double degLat; // latitude in degrees
        private double degLon; // longitude in degrees

        private static double MIN_LAT = ConvertDegreesToRadians(-90d); // -PI/2
        private static double MAX_LAT = ConvertDegreesToRadians(90d); //  PI/2
        private static double MIN_LON = ConvertDegreesToRadians(-180d); // -PI
        private static double MAX_LON = ConvertDegreesToRadians(180d); //  PI

        private GeoLocation()
        {
        }

        /**
	 * @param latitude the latitude, in degrees.
	 * @param longitude the longitude, in degrees.
	 */

        public static GeoLocation fromDegrees(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation();
            result.radLat = ConvertDegreesToRadians(latitude);
            result.radLon = ConvertDegreesToRadians(longitude);
            result.degLat = latitude;
            result.degLon = longitude;
            result.checkBounds();
            return result;
        }

        /**
	 * @param latitude the latitude, in radians.
	 * @param longitude the longitude, in radians.
	 */

        public static GeoLocation fromRadians(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation();
            result.radLat = latitude;
            result.radLon = longitude;
            result.degLat = ConvertRadiansToDegrees(latitude);
            result.degLon = ConvertRadiansToDegrees(longitude);
            result.checkBounds();
            return result;
        }

        private void checkBounds()
        {
            if (radLat < MIN_LAT || radLat > MAX_LAT ||
                radLon < MIN_LON || radLon > MAX_LON)
                throw new ArgumentException();
        }

        /**
	 * @return the latitude, in degrees.
	 */

        public double getLatitudeInDegrees()
        {
            return degLat;
        }

        /**
	 * @return the longitude, in degrees.
	 */

        public double getLongitudeInDegrees()
        {
            return degLon;
        }

        /**
	 * @return the latitude, in radians.
	 */

        public double getLatitudeInRadians()
        {
            return radLat;
        }

        /**
	 * @return the longitude, in radians.
	 */

        public double getLongitudeInRadians()
        {
            return radLon;
        }

        public override string ToString()
        {
            return "(" + degLat + "\u00B0, " + degLon + "\u00B0) = (" +
                   radLat + " rad, " + radLon + " rad)";
        }

        /**
	 * Computes the great circle distance between this GeoLocation instance
	 * and the location argument.
	 * @param radius the radius of the sphere, e.g. the average radius for a
	 * spherical approximation of the figure of the Earth is approximately
	 * 6371.01 kilometers.
	 * @return the distance, measured in the same unit as the radius
	 * argument.
	 */

        public double distanceTo(GeoLocation location, double radius)
        {
            return Math.Acos(Math.Sin(radLat)*Math.Sin(location.radLat) +
                             Math.Cos(radLat)*Math.Cos(location.radLat)*
                             Math.Cos(radLon - location.radLon))*radius;
        }

        /**
	 * <p>Computes the bounding coordinates of all points on the surface
	 * of a sphere that have a great circle distance to the point represented
	 * by this GeoLocation instance that is less or equal to the distance
	 * argument.</p>
	 * <p>For more information about the formulae used in this method visit
	 * <a href="http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates">
	 * http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates</a>.</p>
	 * @param distance the distance from the point represented by this
	 * GeoLocation instance. Must me measured in the same unit as the radius
	 * argument.
	 * @param radius the radius of the sphere, e.g. the average radius for a
	 * spherical approximation of the figure of the Earth is approximately
	 * 6371.01 kilometers.
	 * @return an array of two GeoLocation objects such that:<ul>
	 * <li>The latitude of any point within the specified distance is greater
	 * or equal to the latitude of the first array element and smaller or
	 * equal to the latitude of the second array element.</li>
	 * <li>
         * 
         * If the longitude of the first array element is smaller or equal to
	 * the longitude of the second element, then
	 * the longitude of any point within the specified distance is greater
	 * or equal to the longitude of the first array element and smaller or
	 * equal to the longitude of the second array element.</li>
	 * <li>If the longitude of the first array element is greater than the
	 * longitude of the second element (this is the case if the 180th
	 * meridian is within the distance), then
	 * the longitude of any point within the specified distance is greater
	 * or equal to the longitude of the first array element
	 * <strong>or</strong> smaller or equal to the longitude of the second
	 * array element.</li>
	 * </ul>
	 */

        public GeoLocation[] boundingCoordinates(double distance, double radius)
        {

            if (radius < 0d || distance < 0d)
                throw new ArgumentException();

            // angular distance in radians on a great circle
            double radDist = distance/radius;

            double minLat = radLat - radDist;
            double maxLat = radLat + radDist;

            double minLon, maxLon;
            if (minLat > MIN_LAT && maxLat < MAX_LAT)
            {
                double deltaLon = Math.Asin(Math.Sin(radDist)/
                                            Math.Cos(radLat));
                minLon = radLon - deltaLon;
                if (minLon < MIN_LON) minLon += 2d*Math.PI;
                maxLon = radLon + deltaLon;
                if (maxLon > MAX_LON) maxLon -= 2d*Math.PI;
            }
            else
            {
                // a pole is within the distance
                minLat = Math.Max(minLat, MIN_LAT);
                maxLat = Math.Min(maxLat, MAX_LAT);
                minLon = MIN_LON;
                maxLon = MAX_LON;
            }

            return new GeoLocation[]
            {
                fromRadians(minLat, minLon),
                fromRadians(maxLat, maxLon)
            };
        }

    }
}