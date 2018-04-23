using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LocationTest.Support;
using Android.Graphics;

namespace LocationTest.Views.UI
{
		public class MoneyView : LinearLayout
		{
				ImageView i { get; set; }
				TextView t { get; set; }

				int h = 70;

				public string Text {
						get {
								return t.Text;
						}
						set {
								t.Text = value;
						}
				}

				public MoneyView(Context context) : base(context)
				{
						AddTextView(context);
				}

				public void AddTextView(Context context)
				{
						t = new TextView(context);
						LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
						param.Gravity = GravityFlags.Left;
						t.LayoutParameters = param;
						t.Text = "0";
						t.SetTypeface(Fonts.Pixel, TypefaceStyle.Normal);
						t.SetTextColor(Color.White);
						t.SetTextSize(ComplexUnitType.Px, h);
						AddView(t);
				}



				public MoneyView(Context context, IAttributeSet attrs) : base(context, attrs) {
						if (Tag != null)
						{
								i = new ImageView(context);
								LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(h, h);
								param.Gravity = GravityFlags.Left;
								i.LayoutParameters = param;
								int id = context.Resources.GetIdentifier((string)Tag, "drawable", context.PackageName);
								i.SetImageResource(id);
								AddView(i);
						}
						AddTextView(context);
				}
				public MoneyView(Context context, IAttributeSet attrs, int defStyle) : this(context, attrs) { }
		}
}