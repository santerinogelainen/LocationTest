using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocationTest.Views.Support;

namespace LocationTest.Views.UI
{
		public class UpgradeMenuClose : TouchableButton, ValueAnimator.IAnimatorUpdateListener
		{
				UpgradeMenu Menu { get; set; }
				ValueAnimator ShowUpgradeMenuAnimation { get; set; }

				public UpgradeMenuClose(Activity parent, UpgradeMenu menu) : base(parent)
				{
						Menu = menu;
						Text = "Close";
						SetTextColor(Color.Blue);
						SetBackgroundColor(Color.Transparent);
						SetPadding(0, 0, 0, 0);
						LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 150);
				}

				public override void OnTouchStart(MotionEvent e)
				{
						ShowUpgradeMenuAnimation = ValueAnimator.OfInt(0, Menu.MeasuredHeight);
						ShowUpgradeMenuAnimation.AddUpdateListener(this);
						ShowUpgradeMenuAnimation.SetDuration(500);
						ShowUpgradeMenuAnimation.Start();
				}

				public void OnAnimationUpdate(ValueAnimator animation)
				{
						Menu.Top = (int)animation.AnimatedValue;
				}

		}
}