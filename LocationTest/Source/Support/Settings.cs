using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LocationTest.Support
{
    public static class Settings
    {

        public static class Map
        {
            public const int MaxZoom = 19;
            public const int MinZoom = 17;
            public const int MapType = GoogleMap.MapTypeTerrain;
        }

        public static class DefaultCamera
        {
            public const int Tilt = 45;
            public static readonly LatLng Target = new LatLng(0, 0);
        }

        public static class Gestures
        {
            // HIGHER = SLOWER
            public const int TiltSpeed = 15;
            // HIGHER = SLOWER
            public const int BearingSpeed = 10;
        }

    }
}