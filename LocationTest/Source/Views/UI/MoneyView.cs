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
				// money view image
				ImageView Image { get; set; }

				// money view count text
				TextView CountView { get; set; }

				// money view count
				int Count { get; set; } 

				int Size { get; set; }

				public string Text {
						get {
								return CountView.Text;
						}
						set {
								CountView.Text = value;
						}
				}

				public MoneyView(Context context) : base(context)
				{
						// default size is 50
						Size = 50;
						AddCountView();
				}

				public MoneyView(Context context, int resId, int count, int size = 50) : base(context)
				{
						Size = size;
						AddImageView(resId);
						AddCountView();
						Set(count);
				}

				/// <summary>
				/// Add the count view to this moneyview
				/// </summary>
				/// <param name="count">the count</param>
				private void AddCountView(int count = 0)
				{
						// create countview set layout params
						CountView = new TextView(Context);
						LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LayoutParams.WrapContent, Size);
						param.Gravity = GravityFlags.Left;
						CountView.LayoutParameters = param;

						// set the count
						Set(count);

						// set text settings
						CountView.SetTypeface(Fonts.Pixel, TypefaceStyle.Normal);
						CountView.SetTextColor(Color.White);
						CountView.SetTextSize(ComplexUnitType.Px, Size);

						// add the view
						AddView(CountView);
				}

				/// <summary>
				/// Add the image view to this moneyview
				/// </summary>
				/// <param name="resId">image resource id</param>
				private void AddImageView(int resId)
				{
						// create the image view and set layout params
						Image = new ImageView(Context);
						LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(Size, Size);
						param.Gravity = GravityFlags.Left;
						param.RightMargin = 10;
						Image.LayoutParameters = param;

						D.WL(resId, this);

						// set the image and add the view
						Image.SetImageResource(resId);
						AddView(Image);
				}

				/// <summary>
				/// Add to the count
				/// </summary>
				/// <param name="amount">the amount to add</param>
				public void Add(int amount = 1)
				{
						Count += amount;
						Text = "" + Count;
				}

				/// <summary>
				/// Set the count
				/// </summary>
				/// <param name="newcount">new count</param>
				public void Set(int newcount)
				{
						Count = newcount;
						Text = "" + Count;
				}

				public MoneyView(Context context, IAttributeSet attrs) : base(context, attrs) {
						AttributeReader attributes = new AttributeReader(context, Resource.Styleable.MoneyView, attrs);
						Size = attributes.GetInteger(Resource.Styleable.MoneyView_size, 50);
						AddImageView(attributes.GetResourceId(Resource.Styleable.MoneyView_src));
						AddCountView(attributes.GetInteger(Resource.Styleable.MoneyView_count, 0));
				}
				public MoneyView(Context context, IAttributeSet attrs, int defStyle) : this(context, attrs) { }

				public static MoneyView operator ++(MoneyView view)
				{
						view.Add();
						return view;
				}

				public static implicit operator int (MoneyView view)
				{
						return view.Count;
				}
		}
}