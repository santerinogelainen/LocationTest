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

namespace LocationTest
{
    class Map : SupportMapFragment, IOnMapReadyCallback
    {

        public View OriginalView { get; set; }
        public override View View => OriginalView;

        MapGestureWrapper GestureWrapper { get; set; }

        // the map
        GoogleMap GoogleMap { get; set; }

        // the options
        GoogleMapOptions Options { get; set; }

        // the parent activity
        private FragmentActivity ParentActivity { get; set; }

        // the location provider
        LocationProvider LocationProvider { get; set; }

        /// <summary>
        /// Create the map
        /// </summary>
        /// <param name="parent">The parent activity of this map</param>
        /// <param name="options">The options for this map</param>
        public Map(FragmentActivity parent, GoogleMapOptions options) : base(){
            ParentActivity = parent;

            // set the options and initialize map
            SetOptions(options);
            InitMap();

            // start requesting location
            LocationProvider = new LocationProvider(ParentActivity);
            LocationProvider.OnLocationUpdate += OnLocationUpdate;
            
        }

        /// <summary>
        /// Se the options for this map
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
        /// Initialize the map in the activity
        /// </summary>
        public void InitMap()
        {
            FragmentManager manager = ParentActivity.SupportFragmentManager;
            FragmentTransaction transaction = manager.BeginTransaction();
            transaction.Add(Resource.Id.layout, this, "map");
            transaction.Commit();
            GetMapAsync(this);
        }

        /// <summary>
        /// What happens when we create the view. Because we need custom controls, we need to wrap the created view in a custom layout that contains all gestures (MapGestureWrapper)
        /// </summary>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            OriginalView = base.OnCreateView(inflater, container, savedInstanceState);
            GestureWrapper = new MapGestureWrapper(ParentActivity);
            GestureWrapper.AddView(OriginalView);
            return GestureWrapper;
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
        /// Called when the map is ready
        /// </summary>
        /// <param name="map">The map</param>
        public void OnMapReady(GoogleMap map)
        {
            GoogleMap = map;

            // gestures
            GoogleMap.UiSettings.SetAllGesturesEnabled(false);
            GestureWrapper.Map = GoogleMap;
        }

        public void OnLocationUpdate(Location before, Location after)
        {
            LatLng ln = new LatLng(after.Latitude, after.Longitude);

            CameraUpdate position = CameraUpdateFactory.NewLatLng(ln);
            GoogleMap.AnimateCamera(position, null);
            //System.Diagnostics.Debug.WriteLine("longitude: {0}, latitude: {1}", location.Longitude, location.Latitude);
        }


    }
}