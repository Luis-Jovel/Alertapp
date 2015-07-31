package mono.com.google.android.gms.nearby.connection;


public class Connections_EndpointDiscoveryListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.nearby.connection.Connections.EndpointDiscoveryListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onEndpointFound:(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V:GetOnEndpointFound_Ljava_lang_String_Ljava_lang_String_Ljava_lang_String_Ljava_lang_String_Handler:Android.Gms.Nearby.Connection.IConnectionsEndpointDiscoveryListenerInvoker, Xamarin.GooglePlayServices.Nearby\n" +
			"n_onEndpointLost:(Ljava/lang/String;)V:GetOnEndpointLost_Ljava_lang_String_Handler:Android.Gms.Nearby.Connection.IConnectionsEndpointDiscoveryListenerInvoker, Xamarin.GooglePlayServices.Nearby\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Nearby.Connection.IConnectionsEndpointDiscoveryListenerImplementor, Xamarin.GooglePlayServices.Nearby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Connections_EndpointDiscoveryListenerImplementor.class, __md_methods);
	}


	public Connections_EndpointDiscoveryListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Connections_EndpointDiscoveryListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Nearby.Connection.IConnectionsEndpointDiscoveryListenerImplementor, Xamarin.GooglePlayServices.Nearby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onEndpointFound (java.lang.String p0, java.lang.String p1, java.lang.String p2, java.lang.String p3)
	{
		n_onEndpointFound (p0, p1, p2, p3);
	}

	private native void n_onEndpointFound (java.lang.String p0, java.lang.String p1, java.lang.String p2, java.lang.String p3);


	public void onEndpointLost (java.lang.String p0)
	{
		n_onEndpointLost (p0);
	}

	private native void n_onEndpointLost (java.lang.String p0);

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
