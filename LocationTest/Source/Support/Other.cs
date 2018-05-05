using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
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
				public static DebugMessage[] Messages = new DebugMessage[100];

        /// <summary>
        /// WriteLine
        /// </summary>
        public static void WL(Object text, Object caller = null)
        {
            string line = String.Format("{0,0:00}:{1,0:00}:{2,0:00} | ", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            if (caller != null)
            {
                line += String.Format("{0} | ", caller.GetType().Name);
            }
						if (text == null)
						{
								line += "null";
						} else
						{
								line += text.ToString();
						}
            System.Diagnostics.Debug.WriteLine(line);
        }


				/// <summary>
				/// Write line on screen
				/// </summary>
				/// <param name="context">activity context</param>
				/// <param name="text">text</param>
				/// <param name="id">the id of the line, min 0, max 99</param>
				public static void WLS(Context context, Object text, int id)
				{
						if (Messages[id] != null)
						{
								Messages[id].Text = text.ToString();
						} else
						{
								DebugMessage msg = new DebugMessage(context);
								msg.Text = text.ToString();
								Messages[id] = msg;
								((Activity)context).FindViewById<LinearLayout>(Resource.Id.debuglayout).AddView(Messages[id]);
						}
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

				/// <summary>
				/// Convert a LatLng to a Location
				/// </summary>
				/// <param name="latLng"></param>
				/// <returns></returns>
				public static Location ToLocation(LatLng latLng)
				{
						Location l = new Location(LocationManager.GpsProvider);
						l.Longitude = latLng.Longitude;
						l.Latitude = latLng.Latitude;
						return l;
				}
		}

		public static class ActivityHelper
		{
				public static View FindViewByIdName(Activity activity, string idname)
				{
						if (!string.IsNullOrEmpty(idname))
						{
								int id = activity.Resources.GetIdentifier(idname, "id", activity.PackageName);
								if (id != 0)
								{
										return activity.FindViewById(id);
								}
						}
						return null;
				}
		}

}