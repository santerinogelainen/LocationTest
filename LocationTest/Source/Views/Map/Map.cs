﻿using System;

using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Support.V4.App;
using Android.Views;
using LocationTest.Views.Support;
using LocationTest.Support;
using Android.Util;
using Android.Locations;
using Android.Animation;
using Android.Views.Animations;

namespace LocationTest.Views.Map
{
    /// <summary>
    /// Represents a map
    /// 
    /// This is a wrapper class for GoogleMapView that actually contains the googlemap. This class contains touch events that manipulate the view.
    /// </summary>
    public class Map : TouchableLayout, ValueAnimator.IAnimatorUpdateListener, IOnMapReadyCallback
		{

				// animation values
				private ValueAnimator Animator { get; set; }
				private LatLng LocationBefore { get; set; }
				private LatLng LocationAfter { get; set; }

				/// <summary>
				/// Called when the bearing changes on the map
				/// </summary>
				public event Action<float> OnRotate;

        // the parent activity
        public FragmentActivity Activity { get; set; }

        // center point of the map where touch events get reversed
        public Vector2 CenterPoint { get; set; }

        // starting bearing when user touches the screen
        public float StartBearing { get; set; }

        // view that contains the googlemap
        public GoogleMapView MapView { get; set; }

				// map compass
				public Compass Compass { get; set; }

				// the google map
				public GoogleMap GoogleMap { get; set; }

				// the location provider
				public LocationTest.Support.LocationProvider LocationProvider { get; set; }

				public Map(Context context) : base(context)
        {
            Activity = (FragmentActivity)context;

						// init animation class for custom animations
						Animator = new ValueAnimator();
						Animator.AddUpdateListener(this);
						Animator.SetInterpolator(new LinearInterpolator());

						Post(OnViewCreated);
        }

				public Map(Context context, IAttributeSet attrs) : this(context) { }
				public Map(Context context, IAttributeSet attrs, int defStyle) : this(context) { }


				public void OnViewCreated()
        {
						// init center point of this view
						CenterPoint = new Vector2(Width / 2, Height / 2);

						MapView = new GoogleMapView(this, Settings.GoogleMapOptions);
						// add compass after the mapview has been created, so that the compass is on top of the mapview
						MapView.OnCreated += AddCompass;
						MapView.OnCreated += StartLocationUpdates;
        }

				private void StartLocationUpdates()
				{
						// start requesting location
						LocationProvider = new LocationTest.Support.LocationProvider(Activity);
						LocationProvider.OnLocationUpdate += OnLocationUpdate;
				}

				/// <summary>
				/// Called when the location updates in the provider
				/// </summary>
				/// <param name="before">location before update</param>
				/// <param name="after">location after update</param>
				public void OnLocationUpdate(Location before, Location after)
				{
						if (before != null && after != null)
						{
								LocationBefore = LocationTest.Support.Convert.ToLatLng(before);
								LocationAfter = LocationTest.Support.Convert.ToLatLng(after);
								Animator.SetIntValues(0, 1000);
								Animator.SetDuration(Settings.Location.UpdateInterval);
								Animator.Start();
						}
				}

				/// <summary>
				/// Add a compass to this layout
				/// </summary>
				private void AddCompass()
				{
						AddView(Compass = new Compass(this, Resource.Drawable.placeholder, Resource.Drawable.placeholder));
				}

        public override void OnTouchStart(MotionEvent e)
        {
						// set the bearing that we start with
            StartBearing = GoogleMap.CameraPosition.Bearing;
        }

				public void OnAnimationUpdate(ValueAnimator animation)
				{
						LatLng final = new LatLng(LocationBefore.Latitude + Animator.AnimatedFraction * (LocationAfter.Latitude - LocationBefore.Latitude), LocationBefore.Longitude + Animator.AnimatedFraction * (LocationAfter.Longitude - LocationBefore.Longitude));

						CameraPosition.Builder camera = new CameraPosition.Builder(GoogleMap.CameraPosition);
						camera.Target(final);
						GoogleMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(camera.Build()));
				}

				/// <summary>
				/// Called when the google map is ready
				/// </summary>
				/// <param name="map">The map</param>
				public void OnMapReady(GoogleMap map)
				{
						GoogleMap = map;

						// disable gestures, since the containing map (Parent) handles all gestures
						GoogleMap.UiSettings.SetAllGesturesEnabled(false);
				}

















				/// <summary>
				/// Welcome to the depths of hell
				/// </summary>
				/// <param name="e"></param>
				public override void OnTouchMove(MotionEvent e)
				{
						CameraPosition.Builder camera = new CameraPosition.Builder(GoogleMap.CameraPosition);
						float Bearing;

						// this is a bit complicated

						// if the currect touch position is on the bottom half of the screen
						// or just below the centerpoint
						if (TouchCurrent.Y > CenterPoint.Y)
						{
								// If the start position was on the top half of the screen that means we crossed the center point
								// reset the touch so that the controls dont bug out
								if (TouchStart.Y <= CenterPoint.Y)
								{
										ResetTouchStart(e);
										return;
								}

								// otherwise we can move the camera most likely
								float MinusBearing;

								// check if we are on the right half of the screen currently
								// or on the right side of the center point
								if (TouchCurrent.X > CenterPoint.X)
								{
										// again check if we went over the left and right side of the screen divider by looking at the touch starting position
										// and reset since it bugs out otherwise
										if (TouchStart.X <= CenterPoint.X)
										{
												ResetTouchStart(e);
												return;
										}
										// calculate the bearing that we subtract from the starting bearing
										MinusBearing = ((TouchDelta.X - TouchDelta.Y) / Settings.Gestures.BearingSpeed);
								}
								// we are on the left half of the screen currently
								// or on the left side of the center point
								else
								{
										// again check if we went over the left and right side of the screen divider by looking at the touch starting position
										// and reset since it bugs out otherwise
										if (TouchStart.X >= CenterPoint.X)
										{
												ResetTouchStart(e);
												return;
										}
										// calculate the bearing that we subtract from the starting bearing
										MinusBearing = ((TouchDelta.X + TouchDelta.Y) / Settings.Gestures.BearingSpeed);
								}

								// set the camera bearing
								Bearing = StartBearing - MinusBearing;
						}

						// if the currect touch position is on the top half of the screen
						// or just above the centerpoint
						else
						{
								// If the start position was on the top half of the screen that means we crossed the center point
								// reset the touch so that the controls dont bug out
								if (TouchStart.Y >= CenterPoint.Y)
								{
										ResetTouchStart(e);
										return;
								}

								// otherwise we can move the camera most likely
								float PlusBearing;

								// check if we are on the right half of the screen currently
								// or on the right side of the center point
								if (TouchCurrent.X > CenterPoint.X)
								{
										// again check if we went over the left and right side of the screen divider by looking at the touch starting position
										// and reset since it bugs out otherwise
										if (TouchStart.X <= CenterPoint.X)
										{
												ResetTouchStart(e);
												return;
										}
										// calculate the bearing that we subtract from the starting bearing
										PlusBearing = ((TouchDelta.X + TouchDelta.Y) / Settings.Gestures.BearingSpeed);
								}
								// we are on the left half of the screen currently
								// or on the left side of the center point
								else
								{
										// again check if we went over the left and right side of the screen divider by looking at the touch starting position
										// and reset since it bugs out otherwise
										if (TouchStart.X >= CenterPoint.X)
										{
												ResetTouchStart(e);
												return;
										}
										// calculate the bearing that we subtract from the starting bearing
										PlusBearing = ((TouchDelta.X - TouchDelta.Y) / Settings.Gestures.BearingSpeed);
								}

								// set the camera bearing
								Bearing = StartBearing + PlusBearing;
						}

						camera.Bearing(Bearing);
						OnRotate?.Invoke(Bearing);

						// finally set the camera of the google map
						GoogleMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(camera.Build()));
				}


		}
}