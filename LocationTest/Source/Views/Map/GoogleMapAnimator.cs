using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using LocationTest.Support;

namespace LocationTest.Views.Map
{

		public class MapValueAnimator : ValueAnimator, ValueAnimator.IAnimatorUpdateListener
		{
				public event Action<ValueAnimator> DuringAnimation;

				public MapValueAnimator()
				{
						AddUpdateListener(this);
				}

				public void OnAnimationUpdate(ValueAnimator animation)
				{
						DuringAnimation?.Invoke(animation);
				}
		}

		public class GoogleMapAnimator
		{

				public event Action<float> OnBearingAnimation;
				public event Action<LatLng> OnLocationAnimation;

				MapValueAnimator BearingAnimator { get; set; }
				MapValueAnimator LocationAnimator { get; set; }
				LatLng LocationTo { get; set; }
				GoogleMap Map { get; set; }

				public GoogleMapAnimator(GoogleMap map)
				{
						Map = map;

						// bearing animator
						BearingAnimator = new MapValueAnimator();
						BearingAnimator.DuringAnimation += BearingAnimation;

						// location animator
						LocationAnimator = new MapValueAnimator();
						LocationAnimator.SetInterpolator(new LinearInterpolator());
						LocationAnimator.DuringAnimation += LocationAnimation;
				}

				/// <summary>
				/// Stop a bearing animation
				/// </summary>
				public void StopBearingAnimation()
				{
						if (BearingAnimator.IsRunning)
						{
								BearingAnimator.End();
						}
				}

				/// <summary>
				/// Stop a location animation
				/// </summary>
				public void StopLocationAnimation()
				{
						if (LocationAnimator.IsRunning)
						{
								LocationAnimator.End();
						}
				}

				/// <summary>
				/// Animate a bearing change
				/// </summary>
				/// <param name="to">to</param>
				/// <param name="length">animation length</param>
				public void AnimateBearing(float to, int length = 1000)
				{
						StopBearingAnimation();
						// check shortest distance to the bearing we want
						float start = Map.CameraPosition.Bearing;
						if (start - to > 180)
						{
								BearingAnimator.SetFloatValues(start, start + ((360 - start) + to));
						} else if (start - to < -180)
						{
								BearingAnimator.SetFloatValues(360 + start, to);
						} else 
						{
								BearingAnimator.SetFloatValues(start, to);
						}
						BearingAnimator.SetDuration(length);
						BearingAnimator.Start();
				}

				/// <summary>
				/// Animate a location change
				/// </summary>
				/// <param name="to">to</param>
				/// <param name="length">animation length</param>
				public void AnimateLocation(LatLng to, int length = 1000)
				{
						StopLocationAnimation();
						LocationTo = to;
						LocationAnimator.SetFloatValues((float)Map.CameraPosition.Target.Latitude, (float)to.Latitude);
						LocationAnimator.SetDuration(length);
						LocationAnimator.Start();
				}

				/// <summary>
				/// What actually happens during a bearing animation
				/// </summary>
				/// <param name="animation"></param>
				public void BearingAnimation(ValueAnimator animation)
				{
						CameraPosition.Builder camera = new CameraPosition.Builder(Map.CameraPosition);
						float val = (float)animation.AnimatedValue;
						camera.Bearing(val);
						OnBearingAnimation?.Invoke(val);
						Map.MoveCamera(CameraUpdateFactory.NewCameraPosition(camera.Build()));
				}

				public void LocationAnimation(ValueAnimator animation)
				{
						LatLng before = Map.CameraPosition.Target;
						LatLng final = new LatLng(before.Latitude + animation.AnimatedFraction * (LocationTo.Latitude - before.Latitude), before.Longitude + animation.AnimatedFraction * (LocationTo.Longitude - before.Longitude));

						CameraPosition.Builder camera = new CameraPosition.Builder(Map.CameraPosition);
						camera.Target(final);
						OnLocationAnimation?.Invoke(final);
						Map.MoveCamera(CameraUpdateFactory.NewCameraPosition(camera.Build()));
				}
		}
}