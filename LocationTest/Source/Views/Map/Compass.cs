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
using LocationTest.Support;

namespace LocationTest.Views.Map
{
		public class Compass : FrameLayout, View.IOnClickListener
		{

				Map Map { get; set; }
				public ImageView BackgroundImage { get; set; }
				public ImageView PointerImage { get; set; }

				/// <summary>
				/// Contains the styling for this linearlayout
				/// </summary>
				public FrameLayout.LayoutParams Style {
						get {
								FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(150, 150);
								param.Gravity = GravityFlags.Right;
								param.SetMargins(10, 10, 10, 10);
								return param;
						}
				}

				/// <summary>
				/// Create a new compass
				/// </summary>
				/// <param name="parent">map that cointains this compass</param>
				/// <param name="background">the background image resource id</param>
				/// <param name="pointer">the pointer image resource id</param>
				public Compass(Map parent, int background, int pointer) : base(parent.Context)
				{
						Map = parent;
						LayoutParameters = Style;
						BackgroundImage = AddImage(background);
						PointerImage = AddImage(pointer);
						parent.OnRotate += OnMapRotate;
						SetOnClickListener(this);
				}
				
				/// <summary>
				/// Add an image to this linearlayout
				/// </summary>
				/// <param name="resID">image resource id</param>
				/// <returns>ImageView created and added</returns>
				private ImageView AddImage(int resID)
				{
						ImageView image = new ImageView(Map.Context);
						image.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
						image.SetImageResource(resID);
						AddView(image);
						return image;
				}

				private void OnMapRotate(float bearing)
				{
						if (PointerImage != null)
						{
								PointerImage.Rotation = 360 - bearing;
						}
				}

				public void OnClick(View v)
				{
						D.WL("straighten");
						Map.Straighten();
				}
		}
}