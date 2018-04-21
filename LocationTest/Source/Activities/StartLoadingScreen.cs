using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocationTest.Support;

namespace LocationTest.Activities
{
    [Activity(Label = "StartLoadingScreen", MainLauncher = true)]
    public class StartLoadingScreen : Activity
    {

        string[] AllPermissions { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StartLoadingScreen);

            AllPermissions = PermissionManager.GetRequestedPermissions();
            if (!CheckAllPermissions())
            {
                PermissionManager.AskForPermissions(this, AllPermissions);
            } else
            {
                StartActivity(typeof(MapActivity));
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
    }
}