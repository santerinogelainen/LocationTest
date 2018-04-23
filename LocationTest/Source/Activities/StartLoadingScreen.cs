using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LocationTest.Support;

namespace LocationTest.Activities
{
    [Activity(Label = "StartLoadingScreen", MainLauncher = true)]
    public class StartLoadingScreen : Activity
    {

        string[] AllPermissions { get; set; }
				string GooglePlayError { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StartLoadingScreen);

						if (!IsGooglePlayServicesInstalled())
						{
								// to do show no services screen
						} else
						{
								AllPermissions = PermissionManager.GetRequestedPermissions();
								if (!CheckAllPermissions())
								{
										PermissionManager.AskForPermissions(this, AllPermissions);
								}
								else
								{
										StartActivity(typeof(MapActivity));
								}
						}
        }

        public bool CheckAllPermissions()
        {
            int index = 0;
            bool haspermissions = true;
            foreach(string permission in AllPermissions)
            {

                if (!PermissionManager.CheckPermission(permission))
                {
                    haspermissions = false;
                }

                index++;
            }
            return haspermissions;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (CheckAllPermissions())
            {
                StartActivity(typeof(MapActivity));
            }
        }
				
				public bool IsGooglePlayServicesInstalled()
				{
						var query = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
						if (query == ConnectionResult.Success)
						{
								return true;
						}

						if (GoogleApiAvailability.Instance.IsUserResolvableError(query))
						{
								GooglePlayError = GoogleApiAvailability.Instance.GetErrorString(query);
						}
						return false;
				}
		}
}