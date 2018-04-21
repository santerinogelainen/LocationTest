using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace LocationTest
{
    public class UpgradeMenuButton : MenuButton, ValueAnimator.IAnimatorUpdateListener
    {

        Activity Activity { get; set; }
				UpgradeMenu Menu { get; set; }
				ValueAnimator ShowUpgradeMenuAnimation { get; set; }

				public UpgradeMenuButton(Activity parent, UpgradeMenu menu) : base(parent)
        {
            Activity = parent;
						Menu = menu;

            BitmapImage bmp = new BitmapImage(Resource.Drawable.placeholder);
            SetImageDrawable(bmp);
        }

        public override void OnTouchStart(MotionEvent e)
        {
						ShowUpgradeMenuAnimation = ValueAnimator.OfInt(Menu.MeasuredHeight, 0);
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