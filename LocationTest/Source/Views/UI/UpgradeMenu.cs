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
using Android.Graphics;
using Android.Util;
using LocationTest.Support;

namespace LocationTest.Views.UI
{
    public class UpgradeMenu : RelativeLayout
    {

        public UpgradeMenu(Context context) : base(context)
        {
						RelativeLayout.LayoutParams param = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
						LayoutParameters = param;

            SetBackgroundColor(Color.White);
            Post(OnViewCreated);
        }

				public UpgradeMenu(Context context, IAttributeSet attrs) : this(context) { }
				public UpgradeMenu(Context context, IAttributeSet attrs, int defStyle) : this(context) { }

				public void OnViewCreated()
        {
            TranslationY = MeasuredHeight;
				}

    }
}