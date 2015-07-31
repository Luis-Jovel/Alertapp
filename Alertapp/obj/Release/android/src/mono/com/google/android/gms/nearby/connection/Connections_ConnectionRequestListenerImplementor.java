package mono.com.google.android.gms.nearby.connection;


public class Connections_ConnectionRequestListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.nearby.connection.Connections.ConnectionRequestListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onConnectionRequest:(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;[B)V:GetOnConnectionRequest_Ljava_lang_String_Ljava_lang_String_Ljava_lang_String_arrayBHandler:Android.Gms.Nearby.Connection.IConnectionsConnectionRequestListenerInvoker, Xamarin.GooglePlayServices.Nearby\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Nearby.Connection.IConnectionsConnectionRequestListenerImplementor, Xamarin.GooglePlayServices.Nearby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Connections_ConnectionRequestListenerImplementor.class, __md_methods);
	}


	public Connections_ConnectionRequestListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Connections_ConnectionRequestListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Nearby.Connection.IConnectionsConnectionRequestListenerImplementor, Xamarin.GooglePlayServices.Nearby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onConnectionRequest (java.lang.String p0, java.lang.String p1, java.lang.String p2, byte[] p3)
	{
		n_onConnectionRequest (p0, p1, p2, p3);
	}

	private native void n_onConnectionRequest (java.lang.String p0, java.lang.String p1, java.lang.String p2, byte[] p3);

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
