using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LocationTest.Views.Support;

namespace LocationTest.Views.UI
{
    public class MenuButton : TouchableImageButton
    {

				public MenuButton(Context context, IAttributeSet attrs) : this(context) { }
				public MenuButton(Context context, IAttributeSet attrs, int defStyle) : this(context) { }

				public MenuButton(Context context) : base(context)
        {
            SetPadding(0, 0, 0, 0);
            SetScaleType(ScaleType.FitCenter);
            SetAdjustViewBounds(true);

            SetBackgroundColor(Color.Transparent);
        }

    }
}