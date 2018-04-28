using System;

using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Support.V4.App;
using Android.Views;
using LocationTest.Views.Support;
using LocationTest.Support;
using Android.Util;
using Android.Locations;
using LocationTest.Support.Map;
using System.IO;

namespace LocationTest.Views.Map
{
    /// <summary>
    /// Represents a map
    /// 
    /// This is a wrapper class for GoogleMapView that actually contains the googlemap. This class contains touch events that manipulate the view.
    /// </summary>
    public class Map : TouchableLayout, IOnMapReadyCallback
		{

				public event Action OnReady;

				// animation values
				private GoogleMapAnimator Animator { get; set; }

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
				public LocationTest.Support.Map.LocationProvider LocationProvider { get; set; }

				public Map(Context context) : base(context)
        {
            Activity = (FragmentActivity)context;
						LocationProvider = new LocationTest.Support.Map.LocationProvider(Activity, false);

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
						LocationProvider.OnLocationUpdate += OnLocationUpdate;
						LocationProvider.StartRequestingLocationUpdates();

						// debug
						/*List < LatLng > list = new List<LatLng>();
						list.Add(new LatLng(61.044643, 28.100023));
						list.Add(new LatLng(61.043815, 28.102172));
						list.Add(new LatLng(61.043950, 28.104712));
						list.Add(new LatLng(61.042805, 28.105718));
						LocationProvider.LoopBetween(list, Settings.Location.UpdateInterval);*/
				}

				/// <summary>
				/// Called when the location updates in the provider
				/// </summary>
				/// <param name="before">location before update</param>
				/// <param name="after">location after update</param>
				public void OnLocationUpdate(Location before, Location after)
				{
						if (after != null && Animator != null)
						{
								Animator.AnimateLocation(LocationTest.Support.Convert.ToLatLng(after), Settings.Location.UpdateInterval);
						}
				}

				/// <summary>
				/// Add a compass to this layout
				/// </summary>
				private void AddCompass()
				{
						AddView(Compass = new Compass(this, Resource.Drawable.compass150, Resource.Drawable.pointer150));
				}

        public override void OnTouchStart(MotionEvent e)
        {
						// set the bearing that we start with
            StartBearing = GoogleMap.CameraPosition.Bearing;

						// stop bearing animation
						Animator.StopBearingAnimation();
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

						string json;
						using (StreamReader reader = new StreamReader(Activity.Assets.Open("gmap_options.json")))
						{
								json = reader.ReadToEnd();
						}

						GoogleMap.SetMapStyle(new MapStyleOptions(json));

						// set animator
						Animator = new GoogleMapAnimator(GoogleMap);
						OnReady?.Invoke();
				}

				/// <summary>
				/// Remove all rotation / bearing
				/// </summary>
				public void Straighten()
				{
						Animator.OnBearingAnimation += OnRotate;
						Animator.AnimateBearing(0, 200);
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