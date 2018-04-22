using Android.App;
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
using Android.Animation;
using Android.Locations;

namespace LocationTest.Activities
{
    [Activity(Label = "MapActivity")]
    public class MapActivity : FragmentActivity, ValueAnimator.IAnimatorUpdateListener
    {

        Map Map { get; set; }
        Character Character { get; set; }
        UpgradeMenu UpgradeMenu { get; set; }
				
				ValueAnimator Animation { get; set; }

				protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MapActivity);

						Map = FindViewById<Map>(Resource.Id.map);
            Character = FindViewById<Character>(Resource.Id.character);
						UpgradeMenu = FindViewById<UpgradeMenu>(Resource.Id.upgrademenu);

						Animation = new ValueAnimator();
						Animation.AddUpdateListener(this);
				}

				[Java.Interop.Export("ShowUpgradeMenu")]
				public void ShowUpgradeMenu(View v)
				{
						Animation.SetIntValues(UpgradeMenu.MeasuredHeight, 0);
						Animation.SetDuration(500);
						Animation.Start();
				}

				[Java.Interop.Export("HideUpgradeMenu")]
				public void HideUpgradeMenu(View v)
				{
						Animation.SetIntValues(0, UpgradeMenu.MeasuredHeight);
						Animation.SetDuration(500);
						Animation.Start();
				}

				/*[Java.Interop.Export("OverrideLocation")]
				public void OverrideLocation(View v)
				{
						Location l = new Location(LocationManager.GpsProvider);
						l.Latitude = 61.054821;
						l.Longitude = 28.189627;
						Map.MapView.LocationProvider.OverrideLocation(l);
				}

				[Java.Interop.Export("StopOverrideLocation")]
				public void StopOverrideLocation(View v)
				{
						Map.MapView.LocationProvider.StopOverridingLocation();
				}*/

				public void OnAnimationUpdate(ValueAnimator animation)
				{
						UpgradeMenu.TranslationY = (int)animation.AnimatedValue;
				}

				// to do! move to startloadingscreen
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

