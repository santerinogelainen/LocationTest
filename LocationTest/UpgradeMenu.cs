﻿using System;
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

namespace LocationTest
{
    public class UpgradeMenu : RelativeLayout
    {

        public UpgradeMenu(Activity parent) : base(parent)
        {
            LayoutParameters = new RelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            SetBackgroundColor(Color.White);
            Post(OnViewCreated);
        }

        public void OnViewCreated()
        {
            Top = Height;
        }

    }
}