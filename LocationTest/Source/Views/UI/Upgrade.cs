using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LocationTest.Support;

namespace LocationTest.Views.UI
{
		public class UpgradeView : FrameLayout, View.IOnClickListener
		{

				public event Action<int> OnUpgrade;

				public static int ImageSize = 200;
				private TextView CountView { get; set; }
				public int Count { get; set; }

				public int Padding = 20;

				public FrameLayout.LayoutParams Style {
						get {
								FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
								return param;
						}
				}

				public UpgradeView(Context context, IAttributeSet attrs) : base(context, attrs) {
						Init();
						TypedArray a = context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.UpgradeView, 0, 0);

						// set icon
						if (a.HasValue(Resource.Styleable.UpgradeView_src))
						{
								int i = a.GetResourceId(Resource.Styleable.UpgradeView_src, -1);
								SetImage(i);
						}

						// set title
						if (a.HasValue(Resource.Styleable.UpgradeView_title))
						{
								string s = a.GetString(Resource.Styleable.UpgradeView_title);
								SetTitle(s);
						}

						a.Recycle();
				}
				public UpgradeView(Context context, IAttributeSet attrs, int defStyle) : this(context, attrs) { }
				
				public UpgradeView(Context context) : base(context)
				{
						Init();
				}

				public void Init()
				{
						LayoutParameters = Style;
						SetPadding(Padding, Padding, Padding, Padding);

						// count view
						CountView = new TextView(Context);
						CountView.Text = "" + Count;
						CountView.TextSize = 45;
						CountView.SetTextColor(new Color(255, 255, 255, 100));
						CountView.Gravity = GravityFlags.Center;
						CountView.SetTypeface(Fonts.Pixel, TypefaceStyle.Normal);
						FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(LayoutParams.WrapContent, ImageSize);
						param.Gravity = GravityFlags.Right;
						param.SetMargins(0, 25, 0, 0);
						CountView.LayoutParameters = param;
						AddView(CountView);
						
						SetOnClickListener(this);
				}

				public void SetImage(int resID)
				{
						// create view
						ImageView image = new ImageView(Context);

						// create bmp and add it
						Bitmap bmp = BitmapFactory.DecodeResource(Context.Resources, resID);
						image.SetImageBitmap(bmp);
						// set layoutparams
						FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(ImageSize, ImageSize);
						param.Gravity = GravityFlags.Left;
						image.LayoutParameters = param;

						AddView(image);
				}

				/// <summary>
				/// Sets the title for this view
				/// </summary>
				/// <param name="title">title as a string</param>
				public void SetTitle(string title)
				{
						// init view
						TextView text = new TextView(Context);
						text.Text = title;
						text.TextSize = 15;
						text.SetPadding(Padding, 0, 0, 0);
						text.Gravity = GravityFlags.CenterVertical;
						text.SetTypeface(Fonts.Pixel, TypefaceStyle.Normal);
						text.SetTextColor(Color.White);

						// set layoutparams
						FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(LayoutParams.WrapContent, ImageSize);
						param.SetMargins(ImageSize, 15, 0, 0);
						text.LayoutParameters = param;

						AddView(text);
				}

				/// <summary>
				/// Upgrade
				/// </summary>
				public void Upgrade()
				{
						Count++;
						CountView.Text = "" + Count;
						OnUpgrade?.Invoke(Count); 
				}

				public void OnClick(View v)
				{
						Upgrade();
				}
		}
}