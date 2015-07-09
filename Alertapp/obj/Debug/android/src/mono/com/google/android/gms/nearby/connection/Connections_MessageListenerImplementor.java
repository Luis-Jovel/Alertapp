package mono.com.google.android.gms.nearby.connection;


public class Connections_MessageListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.nearby.connection.Connections.MessageListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onDisconnected:(Ljava/lang/String;)V:GetOnDisconnected_Ljava_lang_String_Handler:Android.Gms.Nearby.Connection.IConnectionsMessageListenerInvoker, Xamarin.GooglePlayServices.Nearby\n" +
			"n_onMessageReceived:(Ljava/lang/String;[BZ)V:GetOnMessageReceived_Ljava_lang_String_arrayBZHandler:Android.Gms.Nearby.Connection.IConnectionsMessageListenerInvoker, Xamarin.GooglePlayServices.Nearby\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Nearby.Connection.IConnectionsMessageListenerImplementor, Xamarin.GooglePlayServices.Nearby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Connections_MessageListenerImplementor.class, __md_methods);
	}


	public Connections_MessageListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Connections_MessageListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Nearby.Connection.IConnectionsMessageListenerImplementor, Xamarin.GooglePlayServices.Nearby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDisconnected (java.lang.String p0)
	{
		n_onDisconnected (p0);
	}

	private native void n_onDisconnected (java.lang.String p0);


	public void onMessageReceived (java.lang.String p0, byte[] p1, boolean p2)
	{
		n_onMessageReceived (p0, p1, p2);
	}

	private native void n_onMessageReceived (java.lang.String p0, byte[] p1, boolean p2);

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
