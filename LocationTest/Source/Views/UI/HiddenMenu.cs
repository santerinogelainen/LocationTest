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
using Android.Graphics;
using Android.Util;
using LocationTest.Support;
using Android.Animation;

namespace LocationTest.Views.UI
{
    public class HiddenMenu : LinearLayout, ValueAnimator.IAnimatorUpdateListener
		{
				ValueAnimator Animator { get; set; }

				public HiddenMenu(Context context) : base(context)
        {
						Animator = new ValueAnimator();
						Animator.AddUpdateListener(this);

						Orientation = Orientation.Vertical;

						Post(OnViewCreated);
        }

				public HiddenMenu(Context context, IAttributeSet attrs) : this(context) { }
				public HiddenMenu(Context context, IAttributeSet attrs, int defStyle) : this(context) { }

				public void OnViewCreated()
        {
            TranslationY = MeasuredHeight;
				}

				public void OnAnimationUpdate(ValueAnimator animation)
				{
						TranslationY = (int)animation.AnimatedValue;
				}

				/// <summary>
				/// Shows the hiddenmenu
				/// </summary>
				public void Show()
				{
						Animator.Pause();
						Animator.SetIntValues((int)TranslationY, 0);
						Animator.SetDuration(500);
						Animator.Start();
				}

				/// <summary>
				/// Hides the hiddenmenu
				/// </summary>
				public void Hide()
				{
						Animator.Pause();
						Animator.SetIntValues((int)TranslationY, MeasuredHeight);
						Animator.SetDuration(500);
						Animator.Start();
				}
		}
}