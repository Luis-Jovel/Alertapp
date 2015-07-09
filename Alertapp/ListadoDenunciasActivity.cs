
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Alertapp
{
	[Activity (Label = "ListadoDenunciasActivity", Theme="@style/MyTheme")]			
	public class ListadoDenunciasActivity : Android.Support.V7.App.ActionBarActivity
	{
		private CardView card1;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.ListadoDenuncias);
			card1 = FindViewById<CardView>(Resource.Id.card1);
			card1.Click += (object sender, EventArgs e) => {
				Console.WriteLine("click grid view 1");
			};
		}
	}
}

