using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android;
using Android.Support.V4.Content;
using Android.Content.PM;
using Android.Support.V4.App;

namespace LocationTest.Support
{
    static class PermissionManager
    {

        /// <summary>
        /// Returns all the requested permissions by this app
        /// </summary>
        /// <returns>array of strings (permissions)</returns>
        public static string[] GetRequestedPermissions()
        {
            Context context = Application.Context;
            return context.PackageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.Permissions).RequestedPermissions.ToArray();

        }

        /// <summary>
        /// Returns true if specified permission is granted for the application
        /// </summary>
        /// <param name="permission">permission</param>
        /// <returns>true if permission granted</returns>
        public static bool CheckPermission(string permission)
        {
            return ContextCompat.CheckSelfPermission(Application.Context, permission) == Permission.Granted;
        }

        /// <summary>
        /// Ask for permission from the user
        /// </summary>
        /// <param name="activity">activity</param>
        /// <param name="permission">permissions</param>
        public static void AskForPermissions(Activity activity, string[] permissions)
        {
            ActivityCompat.RequestPermissions(activity, permissions, 0);
        }

    }
}