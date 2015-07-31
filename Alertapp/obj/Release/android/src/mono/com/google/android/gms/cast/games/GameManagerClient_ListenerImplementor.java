package mono.com.google.android.gms.cast.games;


public class GameManagerClient_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.cast.games.GameManagerClient.Listener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onGameMessageReceived:(Ljava/lang/String;Lorg/json/JSONObject;)V:GetOnGameMessageReceived_Ljava_lang_String_Lorg_json_JSONObject_Handler:Android.Gms.Cast.Games.GameManagerClient/IListenerInvoker, Xamarin.GooglePlayServices.Cast\n" +
			"n_onStateChanged:(Lcom/google/android/gms/cast/games/GameManagerState;Lcom/google/android/gms/cast/games/GameManagerState;)V:GetOnStateChanged_Lcom_google_android_gms_cast_games_GameManagerState_Lcom_google_android_gms_cast_games_GameManagerState_Handler:Android.Gms.Cast.Games.GameManagerClient/IListenerInvoker, Xamarin.GooglePlayServices.Cast\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Cast.Games.GameManagerClient/IListenerImplementor, Xamarin.GooglePlayServices.Cast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GameManagerClient_ListenerImplementor.class, __md_methods);
	}


	public GameManagerClient_ListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GameManagerClient_ListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Cast.Games.GameManagerClient/IListenerImplementor, Xamarin.GooglePlayServices.Cast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onGameMessageReceived (java.lang.String p0, org.json.JSONObject p1)
	{
		n_onGameMessageReceived (p0, p1);
	}

	private native void n_onGameMessageReceived (java.lang.String p0, org.json.JSONObject p1);


	public void onStateChanged (com.google.android.gms.cast.games.GameManagerState p0, com.google.android.gms.cast.games.GameManagerState p1)
	{
		n_onStateChanged (p0, p1);
	}

	private native void n_onStateChanged (com.google.android.gms.cast.games.GameManagerState p0, com.google.android.gms.cast.games.GameManagerState p1);

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
