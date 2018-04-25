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

namespace LocationTest.Support
{
		public class DebugMessage : TextView
		{

				public DebugMessage(Context c) : base(c)
				{
						LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
						SetTextColor(Color.White);
						SetTextSize(Android.Util.ComplexUnitType.Px, 18);
				}

		}
}