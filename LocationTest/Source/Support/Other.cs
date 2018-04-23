﻿using System;
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
				public static void WLS(Context context, Object text)
				{
						((Activity)context).FindViewById<TextView>(Resource.Id.debugmessages).Text = text.ToString();
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