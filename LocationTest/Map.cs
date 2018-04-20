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
            if (TouchCurrent.Y > CenterPoint.Y)
            {
                camera.Bearing(StartBearing - TouchDelta.X / Settings.Gestures.BearingSpeed);
            }
            else
            {
                camera.Bearing(StartBearing + TouchDelta.X / Settings.Gestures.BearingSpeed);
            }

            MapView.GoogleMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(camera.Build()));
        }

        
    }
}