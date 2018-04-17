package md5e960c1517b04153ee5e3995739c1c260;


public class LocationProvider
	extends md5c3f794aca97c55dc0e6d5992e563f700.LocationCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLocationResult:(Lcom/google/android/gms/location/LocationResult;)V:GetOnLocationResult_Lcom_google_android_gms_location_LocationResult_Handler\n" +
			"";
		mono.android.Runtime.register ("LocationTest.LocationProvider, LocationTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LocationProvider.class, __md_methods);
	}


	public LocationProvider ()
	{
		super ();
		if (getClass () == LocationProvider.class)
			mono.android.TypeManager.Activate ("LocationTest.LocationProvider, LocationTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onLocationResult (com.google.android.gms.location.LocationResult p0)
	{
		n_onLocationResult (p0);
	}

	private native void n_onLocationResult (com.google.android.gms.location.LocationResult p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
