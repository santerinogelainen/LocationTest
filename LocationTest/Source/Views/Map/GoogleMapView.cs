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
using Android.Animation;
using Android.Views.Animations;

namespace LocationTest.Views.Map
{
    /// <summary>
    /// Represents the googlemap part of the map. Cannot contain any custom openGL graphics, just the googlemap
    /// </summary>
    public class GoogleMapView : SupportMapFragment
    {

				/// <summary>
				/// Runs when the view has been fully created
				/// </summary>
				public event Action OnCreated;

        // the options
        public GoogleMapOptions Options { get; set; }

        // the map that contains this GoogleMapView
        public Map Parent { get; set; }

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
            GetMapAsync(Parent);
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
						
						OnCreated?.Invoke();
				}
		}
}