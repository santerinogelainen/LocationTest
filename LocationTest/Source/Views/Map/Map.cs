using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using LocationTest.Views.Support;
using LocationTest.Support;

namespace LocationTest.Views.Map
{
    /// <summary>
    /// Represents a map
    /// 
    /// This is a wrapper class for GoogleMapView that actually contains the googlemap. This class contains touch events that manipulate the view.
    /// </summary>
    public class Map : TouchableLayout
    {

        // the parent activity
        public FragmentActivity Activity { get; set; }

        // center point of the map where touch events get reversed
        public Vector2 CenterPoint { get; set; }

        // starting bearing when user touches the screen
        public float StartBearing { get; set; }

        // view that contains the googlemap
        public GoogleMapView MapView { get; set; }

        public Map(FragmentActivity container, GoogleMapOptions options) : base(container)
        {
            Activity = container;

            // its important that we generate an id and insert this layout before creating GoogleMapView since GoogleMapView uses that id to generate the google map inside this layout
            Id = GenerateViewId();
            
            Activity.FindViewById<ViewGroup>(Resource.Id.layout).AddView(this);

            MapView = new GoogleMapView(this, options);

            Post(OnViewCreated);
        }

        public void OnViewCreated()
        {
            CenterPoint = new Vector2(Width / 2, Height / 2);
            //D.WL(CenterPoint, this);
        }

        public override void OnTouchStart(MotionEvent e)
        {
            StartBearing = MapView.GoogleMap.CameraPosition.Bearing;
        }

        public override void OnTouchMove(MotionEvent e)
        {
            CameraPosition.Builder camera = new CameraPosition.Builder(MapView.GoogleMap.CameraPosition);
            
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
								camera.Bearing(StartBearing - MinusBearing);
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
								camera.Bearing(StartBearing + PlusBearing);
						}

						// finally set the camera of the google map
            MapView.GoogleMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(camera.Build()));
        }

        
    }
}