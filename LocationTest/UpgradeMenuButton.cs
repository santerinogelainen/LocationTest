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

namespace LocationTest
{
    public class UpgradeMenuButton : MenuButton
    {

        Activity Activity { get; set; }

        public UpgradeMenuButton(Activity parent) : base(parent)
        {
            Activity = parent;
            BitmapImage bmp = new BitmapImage(Resource.Drawable.placeholder);
            SetImageDrawable(bmp);
        }
        
        public override void OnClick(View v)
        {
            Toast.MakeText(Activity, "click", ToastLength.Long).Show();
        }

    }
}