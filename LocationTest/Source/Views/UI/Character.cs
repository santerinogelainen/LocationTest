using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocationTest.Support;

namespace LocationTest.Views.UI
{
    public class Character : ImageView
    {

        public Character(Activity parent) : base(parent)
        {
            BitmapImage bmp = new BitmapImage(Resource.Drawable.placeholder);
            SetImageDrawable(bmp);
            FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(100, 100);
            param.Gravity = GravityFlags.Center;
            LayoutParameters = param;
        }

    }
}