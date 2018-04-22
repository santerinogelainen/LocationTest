using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LocationTest.Support
{

    public class Vector2
    {

        public float X { get; set; }
        public float Y { get; set; }

        public Vector2() { }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return String.Format("Vector2 | X: {0}, Y: {1}", X, Y);
        }

        public static implicit operator string(Vector2 vector)
        {
            return vector.ToString();
        }
    }

    /// <summary>
    /// Debug class
    /// </summary>
    public static class D
    {
        /// <summary>
        /// WriteLine
        /// </summary>
        public static void WL(string text, Object caller = null)
        {
            string line = String.Format("{0,0:00}:{1,0:00}:{2,0:00} | ", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (caller != null)
            {
                line += String.Format("{0} | ", caller.GetType().Name);
            }
            line += text;
            System.Diagnostics.Debug.WriteLine(line);
        }


				/// <summary>
				/// Write line on screen
				/// </summary>
				public static void WLS(Context context, string text)
				{
						((Activity)context).FindViewById<TextView>(Resource.Id.debugmessages).Text = text;
				}
    }

		public static class Convert
		{
				/// <summary>
				/// Convert Location to a LatLng
				/// </summary>
				/// <param name="location"></param>
				/// <returns></returns>
				public static LatLng ToLatLng(Location location)
				{
						return new LatLng(location.Latitude, location.Longitude);
				}

				private static double PI180(double value)
				{
						return value * Math.PI / 180;
				}

				/// <summary>
				/// Converts two LatLng points to meters
				/// https://stackoverflow.com/questions/639695/how-to-convert-latitude-or-longitude-to-meters
				/// </summary>
				/// <param name="point1"></param>
				/// <param name="point2"></param>
				/// <returns></returns>
				public static double ToMeters(LatLng point1, LatLng point2)
				{
						double eRadius = 6378.137f;
						double dLat = PI180(point2.Latitude) - PI180(point1.Latitude);
						double dLon = PI180(point2.Longitude) - PI180(point1.Longitude);
						double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
								Math.Cos(PI180(point1.Latitude)) * Math.Cos(PI180(point2.Latitude)) *
								Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
						double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
						double d = eRadius * c;
						return d * 1000;
				}
		}

}