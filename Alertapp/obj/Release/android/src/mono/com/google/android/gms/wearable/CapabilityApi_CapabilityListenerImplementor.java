package mono.com.google.android.gms.wearable;


public class CapabilityApi_CapabilityListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.wearable.CapabilityApi.CapabilityListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCapabilityChanged:(Lcom/google/android/gms/wearable/CapabilityInfo;)V:GetOnCapabilityChanged_Lcom_google_android_gms_wearable_CapabilityInfo_Handler:Android.Gms.Wearable.ICapabilityApiCapabilityListenerInvoker, Xamarin.GooglePlayServices.Wearable\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Wearable.ICapabilityApiCapabilityListenerImplementor, Xamarin.GooglePlayServices.Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CapabilityApi_CapabilityListenerImplementor.class, __md_methods);
	}


	public CapabilityApi_CapabilityListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CapabilityApi_CapabilityListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Wearable.ICapabilityApiCapabilityListenerImplementor, Xamarin.GooglePlayServices.Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCapabilityChanged (com.google.android.gms.wearable.CapabilityInfo p0)
	{
		n_onCapabilityChanged (p0);
	}

	private native void n_onCapabilityChanged (com.google.android.gms.wearable.CapabilityInfo p0);

	java.util.ArrayList refList;
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
