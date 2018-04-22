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
using Android.Util;
using Android.Views;
using Android.Widget;
using LocationTest.Support;

namespace LocationTest.Views.UI
{
    public class Character : ImageView
    {

        public Character(Context context) : base(context)
        {
        }
				public Character(Context context, IAttributeSet attrs) : this(context) { }
				public Character(Context context, IAttributeSet attrs, int defStyle) : this(context) { }

		}
}