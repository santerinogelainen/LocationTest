using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Gms.Maps.Model;
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

				public double MetersMoved { get; set; }
				
				public bool UserIsMoving { get; set; }

				private int CurrentMovementCheckInterval { get; set; }
				private double MetersMovedBetweenMovementChecks { get; set; }

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
                .SetInterval(Settings.Location.UpdateInterval)
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

				public void CheckMetersMoved(Location oldLocation, Location newLocation)
				{
						if (oldLocation != null && newLocation != null)
						{
								MetersMovedBetweenMovementChecks += Convert.ToMeters(Convert.ToLatLng(oldLocation), Convert.ToLatLng(newLocation));

								if (CurrentMovementCheckInterval >= Settings.Location.MovementThresholdInterval)
								{
										D.WL(String.Format("Between movement check intervals user has moved:"), this);
										D.WL(String.Format("{0}m of {1}m in {2}s", MetersMovedBetweenMovementChecks, Settings.Location.MovementThreshold, (Settings.Location.MovementThresholdInterval * Settings.Location.UpdateInterval) / 1000), this);
										if (MetersMovedBetweenMovementChecks >= Settings.Location.MovementThreshold)
										{
												UserIsMoving = true;
												D.WL(String.Format("This means that the user is moving."), this);
										}
										else
										{
												UserIsMoving = false;
												D.WL(String.Format("This means that the user is NOT moving."), this);
										}

										D.WLS(ParentActivity, String.Format("{0}m/{1}m in {2}s, Moving: {3}", MetersMovedBetweenMovementChecks, Settings.Location.MovementThreshold, (Settings.Location.MovementThresholdInterval * Settings.Location.UpdateInterval) / 1000, UserIsMoving));

										MetersMovedBetweenMovementChecks = 0;
										CurrentMovementCheckInterval = 0;
								}
								else
								{
										CurrentMovementCheckInterval++;
								}
						}
				}

        /// <summary>
        /// Update the location
        /// </summary>
        /// <param name="location">new location</param>
        public void UpdateLocation(Location location = null)
        {
            if (location != null)
            {
								CheckMetersMoved(LastLocation, location);
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