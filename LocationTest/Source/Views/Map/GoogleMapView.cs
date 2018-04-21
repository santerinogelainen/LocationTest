using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Support.V4.App;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Android.Gms.Maps.Model;
using LocationTest.Support;

namespace LocationTest.Views.Map
{
    /// <summary>
    /// Represents the googlemap part of the map. Cannot contain any custom openGL graphics, just the googlemap
    /// </summary>
    public class GoogleMapView : SupportMapFragment, IOnMapReadyCallback
    {

        // the google map
        public GoogleMap GoogleMap { get; set; }

        // the options
        public GoogleMapOptions Options { get; set; }

        // the map that contains this GoogleMapView
        public Map Parent { get; set; }

        // the location provider
        public LocationTest.Support.LocationProvider LocationProvider { get; set; }

        /// <summary>
        /// Create the GoogleMapView
        /// </summary>
        /// <param name="parent">The parent map of this GoogleMapView</param>
        /// <param name="options">The options for this GoogleMapView</param>
        public GoogleMapView(Map parent, GoogleMapOptions options) : base(){
            Parent = parent;

            // set the options and initialize GoogleMapView
            SetOptions(options);
            InitMap();

            // start requesting location
            LocationProvider = new LocationTest.Support.LocationProvider(Parent.Activity);
            LocationProvider.OnLocationUpdate += OnLocationUpdate;
            
        }

        /// <summary>
        /// Se the options for this GoogleMapView
        /// </summary>
        /// <param name="options">Google map options</param>
        public void SetOptions(GoogleMapOptions options)
        {
            Bundle args = new Bundle();
            args.PutParcelable("MapOptions", options);
            Arguments = args;

            Options = options;
        }

        /// <summary>
        /// Initialize the GoogleMapView in the parent map layout
        /// </summary>
        public void InitMap()
        {
            FragmentManager manager = Parent.Activity.SupportFragmentManager;
            FragmentTransaction transaction = manager.BeginTransaction();
            transaction.Add(Parent.Id, this, "mapfragment");
            transaction.Commit();
            GetMapAsync(this);
        }

        /// <summary>
        /// Called when the view has been created, use to edit the view's properties
        /// </summary>
        /// <param name="view">The view</param>
        /// <param name="savedInstanceState"></param>
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            // set width and height to match parent
            view.LayoutParameters.Width = ViewGroup.LayoutParams.MatchParent;
            view.LayoutParameters.Width = ViewGroup.LayoutParams.MatchParent;

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
        /// Called when the location updates in the provider
        /// </summary>
        /// <param name="before"></param>
        /// <param name="after"></param>
        public void OnLocationUpdate(Location before, Location after)
        {
            LatLng ln = new LatLng(after.Latitude, after.Longitude);

            CameraUpdate position = CameraUpdateFactory.NewLatLng(ln);
            GoogleMap.AnimateCamera(position, null);
            //System.Diagnostics.Debug.WriteLine("longitude: {0}, latitude: {1}", location.Longitude, location.Latitude);
        }


    }
}