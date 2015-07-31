package mono.com.google.android.gms.cast;


public class RemoteMediaPlayer_OnPreloadStatusUpdatedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.cast.RemoteMediaPlayer.OnPreloadStatusUpdatedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreloadStatusUpdated:()V:GetOnPreloadStatusUpdatedHandler:Android.Gms.Cast.RemoteMediaPlayer/IOnPreloadStatusUpdatedListenerInvoker, Xamarin.GooglePlayServices.Cast\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Cast.RemoteMediaPlayer/IOnPreloadStatusUpdatedListenerImplementor, Xamarin.GooglePlayServices.Cast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RemoteMediaPlayer_OnPreloadStatusUpdatedListenerImplementor.class, __md_methods);
	}


	public RemoteMediaPlayer_OnPreloadStatusUpdatedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RemoteMediaPlayer_OnPreloadStatusUpdatedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Cast.RemoteMediaPlayer/IOnPreloadStatusUpdatedListenerImplementor, Xamarin.GooglePlayServices.Cast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onPreloadStatusUpdated ()
	{
		n_onPreloadStatusUpdated ();
	}

	private native void n_onPreloadStatusUpdated ();

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
