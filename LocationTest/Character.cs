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

namespace LocationTest
{
    public class Character : ImageView
    {

        public Character(Activity parent) : base(parent)
        {
            Bitmap bmp1 = BitmapFactory.DecodeResource(parent.Resources, Resource.Drawable.placeholder);
            BitmapDrawable bmp = new BitmapDrawable(bmp1);
            bmp.SetAntiAlias(false);
            SetImageDrawable(bmp);
            FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(100, 100);
            param.Gravity = GravityFlags.Center;
            LayoutParameters = param;
        }

    }
}