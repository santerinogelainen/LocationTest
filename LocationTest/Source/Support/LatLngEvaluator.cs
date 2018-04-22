using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace LocationTest.Support
{
		public class LatLngEvaluator : Java.Lang.Object, ITypeEvaluator
		{
				public Java.Lang.Object Evaluate(float fraction, Java.Lang.Object startValue, Java.Lang.Object endValue)
				{
						LatLng start = (LatLng)startValue;
						LatLng end = (LatLng)endValue;
						LatLng final = new LatLng(start.Latitude + fraction * (end.Latitude - start.Latitude), start.Longitude + fraction * (end.Longitude - start.Longitude));
						return final;
				}
		}
}