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
		public class UpgradeView : FrameLayout, View.IOnClickListener, IHasValue
		{
				/// <summary>
				/// Runs when this upgrade is upgraded
				/// </summary>
				public event Action<int> OnUpgrade;

				public static int ImageSize = 150;
				private TextView CountView { get; set; }
				public int Count { get; set; }

				public MoneyView Gold { get; set; }
				public MoneyView Silver { get; set; }
				public MoneyView Copper { get; set; }

				public readonly int Padding = 20;

				public FrameLayout.LayoutParams Style {
						get {
								FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
								return param;
						}
				}

				public UpgradeView(Context context, IAttributeSet attrs) : base(context, attrs) {
						Init();
						AttributeReader attributes = new AttributeReader(context, Resource.Styleable.UpgradeView, attrs);

						// set icon
						SetImage(attributes.GetResourceId(Resource.Styleable.UpgradeView_src));

						// set title
						SetTitle(attributes.GetString(Resource.Styleable.UpgradeView_title));
						
						// set gold, silver and copper 
						Gold = new MoneyView(context, Resource.Drawable.gold16, attributes.GetInteger(Resource.Styleable.UpgradeView_gold_cost, 0));
						Silver = new MoneyView(context, Resource.Drawable.silver16, attributes.GetInteger(Resource.Styleable.UpgradeView_silver_cost, 0));
						Copper = new MoneyView(context, Resource.Drawable.copper16, attributes.GetInteger(Resource.Styleable.UpgradeView_copper_cost, 0));

						AddMoney(Gold, Silver, Copper);
				}
				public UpgradeView(Context context, IAttributeSet attrs, int defStyle) : this(context, attrs) { }
				
				public UpgradeView(Context context) : base(context)
				{
						Init();
				}

				/// <summary>
				/// Initializes this ugrade. Should be called one in every constructor before everything else
				/// </summary>
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
						CountView.LayoutParameters = param;
						AddView(CountView);
						
						SetOnClickListener(this);
				}

				/// <summary>
				/// Adds the money views to this layout
				/// </summary>
				/// <param name="gold">gold view</param>
				/// <param name="silver">silver view</param>
				/// <param name="copper">copper view</param>
				private void AddMoney(MoneyView gold, MoneyView silver, MoneyView copper)
				{
						TableLayout table = new TableLayout(Context);
						TableRow moneyLayout = new TableRow(Context);
						moneyLayout.AddView(gold);
						moneyLayout.AddView(silver);
						moneyLayout.AddView(copper);
						table.AddView(moneyLayout);
						FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
						param.SetMargins(ImageSize, 0, 0, 0);
						param.Gravity = GravityFlags.Bottom;
						table.LayoutParameters = param;
						table.SetPadding(Padding, 0, 0, 0);
						AddView(table);
				}

				/// <summary>
				/// Sets the image for this upgrade view
				/// </summary>
				/// <param name="resID">resource id</param>
				private void SetImage(int resID)
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
				private void SetTitle(string title)
				{
						// init view
						TextView text = new TextView(Context);
						text.Text = title;
						text.TextSize = 15;
						text.SetPadding(Padding, 0, 0, 0);
						text.SetTypeface(Fonts.Pixel, TypefaceStyle.Normal);
						text.SetTextColor(Color.White);

						// set layoutparams
						FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(LayoutParams.WrapContent, ImageSize);
						param.SetMargins(ImageSize, 0, 0, 0);
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