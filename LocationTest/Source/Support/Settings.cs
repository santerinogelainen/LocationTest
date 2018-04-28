using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LocationTest.Support
{
    public static class Settings
    {
				public static class Location
				{
						// how many meters do we need to walk
						public const int MovementThreshold = 20;
						// in this many intervals (one interval is UpdateInterval)
						public const int MovementThresholdInterval = 8;
						// before the game start counting them towards meters walked

						public const int UpdateInterval = 4000;
				}

        public static class Map
        {
            public const int MaxZoom = 19;
            public const int MinZoom = 17;
            public const int MapType = GoogleMap.MapTypeNormal;
        }

				public static GoogleMapOptions GoogleMapOptions =  new GoogleMapOptions()
            .InvokeMapType(Map.MapType)
            .InvokeMaxZoomPreference(Map.MaxZoom)
            .InvokeMinZoomPreference(Map.MinZoom)
            .InvokeCompassEnabled(false);

        public static class Gestures
        {
            // HIGHER = SLOWER
            public const int TiltSpeed = 15;
            // HIGHER = SLOWER
            public const int BearingSpeed = 10;
        }

    }
}