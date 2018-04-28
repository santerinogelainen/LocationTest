using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace LocationTest.Support.Map
{
		/// <summary>
		/// Loops between a list of locations
		/// </summary>
		public class LocationLooper : Java.Lang.Object, IRunnable
		{
				LocationProvider Provider { get; set; }
				List<LatLng> Locations { get; set; }
				Handler Handler { get; set; }
				int Length { get; set; }
				int i = 0;

				/// <summary>
				/// Loops between a list of locations
				/// </summary>
				/// <param name="provider">location provider</param>
				/// <param name="locations">list of locations</param>
				/// <param name="ms">time between each loop in milliseconds</param>
				public LocationLooper(LocationProvider provider, List<LatLng> locations, int ms)
				{
						Provider = provider;
						Locations = locations;
						Handler = new Handler();
						Length = ms;
				}

				/// <summary>
				/// Start the loop
				/// </summary>
				public void Start()
				{
						Handler.Post(this);
				}

				public void Run()
				{
						Provider.OverrideLocation(Convert.ToLocation(Locations[i]));

						if (i >= Locations.Count - 1)
						{
								i = 0;
						}
						else
						{
								i++;
						}

						Handler.PostDelayed(this, Length);
				}
		}
}