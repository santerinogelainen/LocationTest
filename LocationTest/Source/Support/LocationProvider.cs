using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Gms.Tasks;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LocationTest.Support
{

    /// <summary>
    /// Provides the location and location updates
    /// </summary>
    public class LocationProvider : LocationCallback
    {

        const int UPDATE_INTERVAL = 4000;

        /// <summary>
        /// Fires when a new location is acquired
        /// </summary>
        /// <param name="location">the new location</param>
        public delegate void LocationUpdate(Location before, Location after);
        public event LocationUpdate OnLocationUpdate;

        // android location provider client
        FusedLocationProviderClient Client { get; set; }

        // the last location that was aqcuired
        Location LastLocation { get; set; }

        // true if this class is requesting location
        bool IsRequestingLocation { get; set; }

        // the parent activity of the provider
        private Activity ParentActivity { get; set; }

        /// <summary>
        /// Create the locationprovider
        /// </summary>
        /// <param name="parent">Parent activity of this provider</param>
        /// <param name="startrequesting">default = true. set to false if you do not want it to start requesting updates</param>
        public LocationProvider(Activity parent, bool startrequesting = true)
        {
            ParentActivity = parent;
            Client = LocationServices.GetFusedLocationProviderClient(ParentActivity);

            if (startrequesting)
            {
                StartRequestingLocationUpdates();
            }
        }

        /// <summary>
        /// Start requesting location updates. Access the last location acquired with the LastLocation property
        /// </summary>
        async private void StartRequestingLocationUpdates()
        {
            LocationRequest request = new LocationRequest()
                .SetPriority(LocationRequest.PriorityHighAccuracy)
                .SetInterval(UPDATE_INTERVAL)
                .SetFastestInterval(1000);

            await Client.RequestLocationUpdatesAsync(request, this);
            IsRequestingLocation = true;
        }

        /// <summary>
        /// Stop requesting location updates.
        /// </summary>
        async private void StopRequestingLocationUpdates()
        {
            if (IsRequestingLocation)
            {
                await Client.RemoveLocationUpdatesAsync(this);
                IsRequestingLocation = false;
            }
        }

        /// <summary>
        /// Request a location update immidiately. Access the location with the LastLocation property. If the location update fails, LastLocation wont change
        /// </summary>
        async void RequestLastLocationNow()
        {
            Location location = await Client.GetLastLocationAsync();
            UpdateLocation(location);
        }

        /// <summary>
        /// Update the location
        /// </summary>
        /// <param name="location">new location</param>
        public void UpdateLocation(Location location = null)
        {
            if (location != null)
            {
                OnLocationUpdate?.Invoke(LastLocation, location);
                LastLocation = location;
            }
        }

        /// <summary>
        /// Fires when a new location is acquired my the fused client
        /// </summary>
        /// <param name="result"></param>
        public override void OnLocationResult(LocationResult result)
        {
            if (result.Locations.Any())
            {
                Location location = result.Locations.First();
                UpdateLocation(location);
            }
        }

    }
}