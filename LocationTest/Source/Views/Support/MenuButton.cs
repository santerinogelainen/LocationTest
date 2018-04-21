using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LocationTest.Views.Support
{
    public class MenuButton : TouchableImageButton
    {

				public MenuButton(Context context) : base(context)
        {
            FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(175, 175);
            param.SetMargins(10, 10, 10, 10);
            param.Gravity = GravityFlags.Bottom;
            LayoutParameters = param;

            SetPadding(0, 0, 0, 0);
            SetScaleType(ScaleType.FitCenter);
            SetAdjustViewBounds(true);

            SetBackgroundColor(Color.Transparent);
        }
    }
}