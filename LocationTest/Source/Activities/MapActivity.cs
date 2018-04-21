﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Common;
using Android.Util;
using Android.Support.V4.App;
using Android.Views;
using LocationTest.Support;
using LocationTest.Views.UI;
using LocationTest.Views.Map;

namespace LocationTest.Activities
{
    [Activity(Label = "MapActivity")]
    public class MapActivity : FragmentActivity
    {

				public int Width { get; set; }
				public int Height { get; set; }

        Map Map { get; set; }
        Character Character { get; set; }
        UpgradeMenuButton UpgradeMenuButton { get; set; }
        UpgradeMenu UpgradeMenu { get; set; }

        ViewGroup Layout { get; set; }

        GoogleMapOptions mapOptions = new GoogleMapOptions()
            .InvokeMapType(Settings.Map.MapType)
            .InvokeMaxZoomPreference(Settings.Map.MaxZoom)
            .InvokeMinZoomPreference(Settings.Map.MinZoom)
            .InvokeCompassEnabled(false);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MapActivity);

            Layout = FindViewById<ViewGroup>(Resource.Id.layout);

            if (IsGooglePlayServicesInstalled())
            {
                Map = new Map(this, mapOptions);
            }

            Character = new Character(this);
            Layout.AddView(Character);

						UpgradeMenu = new UpgradeMenu(this);
						UpgradeMenuButton = new UpgradeMenuButton(this, UpgradeMenu);
						Layout.AddView(UpgradeMenuButton);
						Layout.AddView(UpgradeMenu);
				}

        public bool IsGooglePlayServicesInstalled()
        {
            var query = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (query == ConnectionResult.Success)
            {
                Log.Info("MainActivity", "Google play is installed");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(query))
            {
                string error = GoogleApiAvailability.Instance.GetErrorString(query);
                Log.Error("MainActivity", "Google playe services error {0} - {1}", query, error);
            }

            return true;
        }
    }
}

