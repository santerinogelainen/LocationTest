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


				public List<MeterEvent> MeterEvents { get; set; }

				public bool LocationOverriden { get; set; }

				public double MetersMoved { get; set; }
				
				public bool UserIsMoving { get; set; }

				private int CurrentMovementCheckInterval { get; set; }
				private double FirstIterationLatitude { get; set; }
				private double FirstIterationLongitude { get; set; }

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
						MeterEvents = new List<MeterEvent>();

						if (startrequesting)
            {
								RequestLastLocationNow();
                StartRequestingLocationUpdates();
            }
        }

        /// <summary>
        /// Start requesting location updates. Access the last location acquired with the LastLocation property
        /// </summary>
        async public void StartRequestingLocationUpdates()
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
        async public void StopRequestingLocationUpdates()
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

				public void CheckUserMovement(Location newLocation)
				{
						if (newLocation != null)
						{
								if (CurrentMovementCheckInterval == 0)
								{
										FirstIterationLatitude = newLocation.Latitude;
										FirstIterationLongitude = newLocation.Longitude;
								}

								if (CurrentMovementCheckInterval == Settings.Location.MovementThresholdInterval)
								{
										// stupid hack because the garbage collection would dispose the objects location or latlng for somereason
										float meters = Convert.ToLocation(new LatLng(FirstIterationLatitude, FirstIterationLongitude)).DistanceTo(newLocation);
										if (meters >= Settings.Location.MovementThreshold)
										{
												UserIsMoving = true;
										}
										D.WLS(ParentActivity, String.Format("{0}m of {1}m radius in {2}s", meters, Settings.Location.MovementThreshold, (CurrentMovementCheckInterval * Settings.Location.UpdateInterval) / 1000), 1);
										CurrentMovementCheckInterval = 0;
								}
								else
								{
										CurrentMovementCheckInterval++;
								}
						}
				}

				/// <summary>
				/// Do something every x meters
				/// </summary>
				/// <param name="meters"></param>
				/// <param name="action"></param>
				public void Every(int meters, Action action)
				{
						MeterEvents.Add(new MeterEvent(meters, action));
				}

        /// <summary>
        /// Update the location
        /// </summary>
        /// <param name="location">new location</param>
        public void UpdateLocation(Location location)
        {
            if (location != null)
						{
								D.WLS(ParentActivity, "lon: " + location.Longitude + ", lat: " + location.Latitude, 4);
								CheckUserMovement(location);

								if (UserIsMoving)
								{
										float moved = LastLocation.DistanceTo(location);
										MetersMoved += moved;
										foreach (MeterEvent e in MeterEvents)
										{
												e.RunAction(moved);
										}
										D.WLS(ParentActivity, MetersMoved, 2);
								}

								OnLocationUpdate?.Invoke(LastLocation, location);
                LastLocation = location;
            }
        }

				public void OverrideLocation(Location location)
				{
						StopRequestingLocationUpdates();
						UpdateLocation(location);
						LocationOverriden = true;
				}

				public void StopOverridingLocation()
				{
						StartRequestingLocationUpdates();
						LocationOverriden = false;
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