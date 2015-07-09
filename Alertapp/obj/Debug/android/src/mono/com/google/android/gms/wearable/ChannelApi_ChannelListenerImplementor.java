package mono.com.google.android.gms.wearable;


public class ChannelApi_ChannelListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.wearable.ChannelApi.ChannelListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onChannelClosed:(Lcom/google/android/gms/wearable/Channel;II)V:GetOnChannelClosed_Lcom_google_android_gms_wearable_Channel_IIHandler:Android.Gms.Wearable.IChannelApiChannelListenerInvoker, Xamarin.GooglePlayServices.Wearable\n" +
			"n_onChannelOpened:(Lcom/google/android/gms/wearable/Channel;)V:GetOnChannelOpened_Lcom_google_android_gms_wearable_Channel_Handler:Android.Gms.Wearable.IChannelApiChannelListenerInvoker, Xamarin.GooglePlayServices.Wearable\n" +
			"n_onInputClosed:(Lcom/google/android/gms/wearable/Channel;II)V:GetOnInputClosed_Lcom_google_android_gms_wearable_Channel_IIHandler:Android.Gms.Wearable.IChannelApiChannelListenerInvoker, Xamarin.GooglePlayServices.Wearable\n" +
			"n_onOutputClosed:(Lcom/google/android/gms/wearable/Channel;II)V:GetOnOutputClosed_Lcom_google_android_gms_wearable_Channel_IIHandler:Android.Gms.Wearable.IChannelApiChannelListenerInvoker, Xamarin.GooglePlayServices.Wearable\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Wearable.IChannelApiChannelListenerImplementor, Xamarin.GooglePlayServices.Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ChannelApi_ChannelListenerImplementor.class, __md_methods);
	}


	public ChannelApi_ChannelListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ChannelApi_ChannelListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Wearable.IChannelApiChannelListenerImplementor, Xamarin.GooglePlayServices.Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onChannelClosed (com.google.android.gms.wearable.Channel p0, int p1, int p2)
	{
		n_onChannelClosed (p0, p1, p2);
	}

	private native void n_onChannelClosed (com.google.android.gms.wearable.Channel p0, int p1, int p2);


	public void onChannelOpened (com.google.android.gms.wearable.Channel p0)
	{
		n_onChannelOpened (p0);
	}

	private native void n_onChannelOpened (com.google.android.gms.wearable.Channel p0);


	public void onInputClosed (com.google.android.gms.wearable.Channel p0, int p1, int p2)
	{
		n_onInputClosed (p0, p1, p2);
	}

	private native void n_onInputClosed (com.google.android.gms.wearable.Channel p0, int p1, int p2);


	public void onOutputClosed (com.google.android.gms.wearable.Channel p0, int p1, int p2)
	{
		n_onOutputClosed (p0, p1, p2);
	}

	private native void n_onOutputClosed (com.google.android.gms.wearable.Channel p0, int p1, int p2);

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
