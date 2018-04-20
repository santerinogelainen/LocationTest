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

namespace LocationTest
{
    /// <summary>
    /// Represents a map
    /// 
    /// This is a wrapper class for GoogleMapView that actually contains the googlemap. This class contains touch events that manipulate the view.
    /// </summary>
    public class Map : FrameLayout
    {

        // the parent activity
        public FragmentActivity Activity { get; set; }

        public Vector2 CenterPoint { get; set; }

        // starting bearing when user touches the screen
        public float StartBearing { get; set; }
        public float StartTilt { get; set; }

        // touch positions on start, end and the delta during movement
        public Vector2 TouchStart { get; set; }
        public Vector2 TouchCurrent { get; set; }
        public Vector2 TouchEnd { get; set; }
        public Vector2 TouchDelta { get; set; }

        // view that contains the googlemap
        public GoogleMapView MapView { get; set; }

        public Map(FragmentActivity container, GoogleMapOptions options) : base(container)
        {
            InitTouchEvents();
            Activity = container;

            // its important that we generate an id and insert this layout before creating GoogleMapView since GoogleMapView uses that id to generate the google map inside this layout
            Id = GenerateViewId();
            
            Activity.FindViewById<LinearLayout>(Resource.Id.layout).AddView(this);

            MapView = new GoogleMapView(this, options);

            Post(OnViewCreated);
        }

        public void OnViewCreated()
        {
            CenterPoint = new Vector2(Width / 2, Height / 2);
            //D.WL(CenterPoint, this);
        }

        /// <summary>
        /// Initializes the touch events
        /// </summary>
        public void InitTouchEvents()
        {
            TouchStart = new Vector2();
            TouchEnd = new Vector2();
            TouchDelta = new Vector2();
            TouchCurrent = new Vector2();
        }

        /// <summary>
        /// Sets a vector's x and y values with motionevent e.GetX or e.GetY 
        /// </summary>
        /// <param name="vector">vector</param>
        /// <param name="e">motion event</param>
        public void SetVector(Vector2 vector, MotionEvent e)
        {
            vector.X = e.GetX();
            vector.Y = e.GetY();
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                // on first touch, set the touchstart location
                case MotionEventActions.Down:
                    SetVector(TouchStart, e);
                    StartBearing = MapView.GoogleMap.CameraPosition.Bearing;
                    StartTilt = MapView.GoogleMap.CameraPosition.Tilt;
                    D.WL(String.Format("Start: X: {0}, Y: {1}", TouchStart.X, TouchStart.Y));
                    break;
                // on touch up, set the touch end location
                case MotionEventActions.Up:
                    SetVector(TouchEnd, e);
                    D.WL(String.Format("End: X: {0}, Y: {1}", TouchEnd.X, TouchEnd.Y));
                    break;
                // when we move update the delta position with the starting position and current position
                case MotionEventActions.Move:
                    SetVector(TouchCurrent, e);
                    TouchDelta.X = TouchStart.X - e.GetX();
                    TouchDelta.Y = TouchStart.Y - e.GetY();
                    D.WL(String.Format("Current: X: {0}, Y: {1}", TouchCurrent.X, TouchCurrent.Y));
                    D.WL(String.Format("Delta: X: {0}, Y: {1}", TouchDelta.X, TouchDelta.Y));

                    CameraPosition.Builder camera = new CameraPosition.Builder(MapView.GoogleMap.CameraPosition);
                    if (TouchCurrent.Y > CenterPoint.Y)
                    {
                        camera.Bearing(StartBearing - TouchDelta.X / Settings.Gestures.BearingSpeed);
                    } else
                    {
                        camera.Bearing(StartBearing + TouchDelta.X / Settings.Gestures.BearingSpeed);
                    }

                    /*if (e.PointerCount == 2)
                    {
                        float tilt = StartTilt - TouchDelta.Y / Settings.Gestures.TiltSpeed;

                        // do not overflow tilt, min 0 max 90
                        if (tilt < 0)
                        {
                            tilt = 0;
                        }
                        if (tilt > 90)
                        {
                            tilt = 90;
                        }
                        camera.Tilt(tilt);
                    }*/

                    MapView.GoogleMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(camera.Build()));

                    break;
            }
            return base.DispatchTouchEvent(e);
        }
    }
}