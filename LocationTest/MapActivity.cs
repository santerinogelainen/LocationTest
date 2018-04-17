using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Common;
using Android.Util;
using Android.Support.V4.App;

namespace LocationTest
{
    [Activity(Label = "MapActivity")]
    public class MapActivity : FragmentActivity
    {

        Map map;
        GoogleMapOptions mapOptions = new GoogleMapOptions()
            .InvokeMapType(Settings.Map.MapType)
            .InvokeMaxZoomPreference(Settings.Map.MaxZoom)
            .InvokeMinZoomPreference(Settings.Map.MinZoom)
            .InvokeScrollGesturesEnabled(false)
            .InvokeCompassEnabled(false);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MapActivity);

            if (IsGooglePlayServicesInstalled())
            {
                map = new Map(this, mapOptions);
            }
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

