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

namespace LocationTest.Support
{
    public class BitmapImage
    {

        public BitmapDrawable Drawable { get; set; }

        public BitmapImage(int resId)
        {
            Bitmap bmp = BitmapFactory.DecodeResource(Application.Context.Resources, resId);
            Drawable = new BitmapDrawable(bmp);
            Drawable.SetAntiAlias(false);
        }

        public static implicit operator BitmapDrawable(BitmapImage img)
        {
            return img.Drawable;
        }

    }
}