package mono.com.google.android.gms.cast;


public class RemoteMediaPlayer_OnQueueStatusUpdatedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.cast.RemoteMediaPlayer.OnQueueStatusUpdatedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onQueueStatusUpdated:()V:GetOnQueueStatusUpdatedHandler:Android.Gms.Cast.RemoteMediaPlayer/IOnQueueStatusUpdatedListenerInvoker, Xamarin.GooglePlayServices.Cast\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Cast.RemoteMediaPlayer/IOnQueueStatusUpdatedListenerImplementor, Xamarin.GooglePlayServices.Cast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RemoteMediaPlayer_OnQueueStatusUpdatedListenerImplementor.class, __md_methods);
	}


	public RemoteMediaPlayer_OnQueueStatusUpdatedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RemoteMediaPlayer_OnQueueStatusUpdatedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Cast.RemoteMediaPlayer/IOnQueueStatusUpdatedListenerImplementor, Xamarin.GooglePlayServices.Cast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onQueueStatusUpdated ()
	{
		n_onQueueStatusUpdated ();
	}

	private native void n_onQueueStatusUpdated ();

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
