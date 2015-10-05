using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Specialized;
using Android.Graphics;
using Android.Locations;
using Android.Views.InputMethods;
using Android.Net;
using Android.Gms.Common;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Xamarin.Geolocation;
using System.Threading.Tasks;
//using Parse;
namespace Alertapp
{
    [Activity(Label = "Alertapp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : ActionBarActivity, IOnMapReadyCallback, Android.Gms.Maps.GoogleMap.IInfoWindowAdapter, Android.Gms.Maps.GoogleMap.IOnInfoWindowClickListener/*, ILocationListener*/
    {
        private GoogleMap mMap;
        private Button btnNormal, btnSatellite;
        private ImageButton ibtnReload, ibtnGps, ibtnSearch;
        private EditText txtBuscar;
        private WebClient cliente, clienteUpload;
        public static Dictionary<string, System.Uri> WebServices;
        public static Dictionary<int, float> ColorTipoDenuncia;
		public static Dictionary<int, int> MarkerTipoDenuncia;
        private List<Denuncia> Denuncias, DenunciasAux;
        private Address address;
        private Marker customMarker;
		//GPS OBSOLETO
//        Location _currentLocation;
//        LocationManager _locationManager;
//        String _locationProvider;
		//GPS NUEVO
		private Geolocator geolocator;
        private SupportToolbar toolBar;
        private Android.App.AlertDialog.Builder builder;
        private Android.App.AlertDialog alert;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
//		async void example(){
//			ParseObject gameScore = new ParseObject("GameScore");
//			gameScore["score"] = 1337;
//			gameScore["playerName"] = "Sean Plott";
//			await gameScore.SaveAsync();
//		}
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
//			ParseClient.Initialize("LNAuxom26NKczyL2hfU3deDyFvxkR9vAEVt3NYom",
//				"pTK01DCWyIlw3DQJludWbtnBgvpe2PqNFKa8aDmm");
			//example ();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            WebServices = new Dictionary<string, System.Uri> {
				{"getDenuncias",new System.Uri("http://alertapp.uphero.com/index.php/Mobile/getDenuncias")},
				{"setDenuncia",new System.Uri("http://alertapp.uphero.com/index.php/Mobile/setDenuncia")},
				{"getDenunciaPicture",new System.Uri("http://alertapp.uphero.com/index.php/Mobile/getDenunciaPicture")}
            };
			//este diccionario dejo de usarse pero puede resultar util luego
            ColorTipoDenuncia = new Dictionary<int, float>{
                {1, BitmapDescriptorFactory.HueRed},
                {2, BitmapDescriptorFactory.HueAzure},
                {3, BitmapDescriptorFactory.HueGreen},
                {4, BitmapDescriptorFactory.HueYellow},
            };
			MarkerTipoDenuncia = new Dictionary<int, int>{
				{1, Resource.Drawable.corte_marker},
				{2, Resource.Drawable.fuga_marker},
				{3, Resource.Drawable.damage_marker},
				{4, Resource.Drawable.otros_marker}
			};
            btnNormal = FindViewById<Button>(Resource.Id.btnNormal);
            btnSatellite = FindViewById<Button>(Resource.Id.btnSatellite);
            ibtnReload = FindViewById<ImageButton>(Resource.Id.ibtnReload);
            ibtnGps = FindViewById<ImageButton>(Resource.Id.ibtnGps);
            ibtnSearch = FindViewById<ImageButton>(Resource.Id.ibtnSearch);
            txtBuscar = FindViewById<EditText>(Resource.Id.txtBuscar);
            txtBuscar.ClearFocus();
            btnNormal.RequestFocus();
            btnNormal.Click +=btnNormal_Click;
            btnSatellite.Click +=btnSatellite_Click;
            txtBuscar.EditorAction += txtBuscar_EditorAction;
            ibtnReload.Click += ibtnReload_Click;
            ibtnGps.Click += ibtnGps_Click;
            ibtnSearch.Click += ibtnSearch_Click;

            toolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            toolBar.SetTitleTextColor(Color.White);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawe_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            SetSupportActionBar(toolBar);

            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Bienvenido Luis Jovel");
			mLeftDataSet.Add("Listado de Denuncias");
            mLeftDataSet.Add("Filtrar Resultados");
            mLeftDataSet.Add("     Ver Todos");
            mLeftDataSet.Add("     Corte de servicio");
            mLeftDataSet.Add("     Fuga de Agua");
            mLeftDataSet.Add("     Daño a Infraestructura");
            mLeftDataSet.Add("     Otros");
            mLeftDataSet.Add("Cerrar Sesion");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.ItemClick += mLeftDrawer_ItemClick;
            mLeftDrawer.Adapter = mLeftAdapter;

            mDrawerToggle = new MyActionBarDrawerToggle(
                this,
                mDrawerLayout,
                Resource.String.openDrawer,
                Resource.String.closeDrawer
            );
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mDrawerToggle.SyncState();
			//GPS NUEVO
			setupGPS();
            SetUpMap();
			//GPS OBSOLETO
//            InitializeLocationManager();
            cliente = new WebClient();
            //llamar a web service
            cliente.DownloadDataAsync(WebServices["getDenuncias"]);
            cliente.DownloadDataCompleted += cliente_DownloadDataCompleted;           

            //alertdialog
            builder = new Android.App.AlertDialog.Builder(this);
            alert = builder.Create();
        }

        void mLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
			if (e.Position==1) {
				Shared.denuncias = Denuncias;
				StartActivity (typeof(ListadoDenunciasActivity));
			}
            if (e.Position>=3 && e.Position <= 7)
            {
                filtrarDenunciasPorTipo(mLeftDrawer.GetItemAtPosition(e.Position).ToString().Trim());
            }
        }
        public void filtrarDenunciasPorTipo(string filtro)
        {
            mDrawerLayout.CloseDrawer(mLeftDrawer);
            DenunciasAux = Denuncias;
            if (filtro!="Ver Todos")
            {
                Denuncias = (from d in Denuncias
                             where d.nombretipo == filtro
                             select d).ToList();
            }
            mMap.Clear();
            loadDencunciasMarkers();
            Denuncias = DenunciasAux;
        }
		void setupGPS(){
			//GPS nuevo
			this.geolocator = new Geolocator(this){DesiredAccuracy = 50};
			this.geolocator.PositionError += (object sender, PositionErrorEventArgs e) => {
				alert.SetMessage("No se pudo ubicar la posicion");
				alert.Show();
			};
//			this.geolocator.PositionChanged += OnPositionChanged;
		}
        void ibtnGps_Click(object sender, EventArgs e)
        {
			if (!this.geolocator.IsGeolocationAvailable || !this.geolocator.IsGeolocationEnabled)
			{
				alert.SetMessage("Internet o GPS estan desactivados");
				alert.Show();
				return;
			}
			//GPS NUEVO
			this.geolocator.GetPositionAsync(timeout: 10000).ContinueWith(t => {
				Console.WriteLine ("Position Status: {0}", t.Result.Timestamp);
				Console.WriteLine ("Position Latitude: {0}", t.Result.Latitude);
				Console.WriteLine ("Position Longitude: {0}", t.Result.Longitude);
				Geocoder geoCoder = new Geocoder(this);
				IList<Address> gotAddresses = null;
				gotAddresses = geoCoder.GetFromLocation(t.Result.Latitude, t.Result.Longitude, 1);
				this.address = (Address)gotAddresses[0];
				this.address.Latitude = t.Result.Latitude;
				this.address.Longitude = t.Result.Longitude;
				mMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(t.Result.Latitude, t.Result.Longitude), 16));
				addCustomMarker(t.Result.Latitude, t.Result.Longitude);
			}, TaskScheduler.FromCurrentSynchronizationContext());
			//GPS OBSOLETO
            /*bool isGPSEnabled = _locationManager.IsProviderEnabled(LocationManager.GpsProvider);
            ConnectivityManager cm = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
            bool dataConnection = cm.GetAllNetworkInfo() != null;
            int resultCode = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);
            if (!isGPSEnabled||!dataConnection||ConnectionResult.Success != resultCode)
            {
                alert.SetMessage("Internet o GPS estan desactivados");
                alert.Show();
            }
            else
            {
                if (_currentLocation == null)
                {
                    alert.SetMessage("No se pudo ubicar la posicion");
                    alert.Show();
                }
                else
                {
                    Geocoder geocoder = new Geocoder(this);
                    //IList<Address> addressList = await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);
                    addCustomMarker(_currentLocation.Latitude, _currentLocation.Longitude);
                    mMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(_currentLocation.Latitude, _currentLocation.Longitude), 10));
                }
            }*/
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig); 
        }
		//GPS OBSOLETO
//        protected override void OnResume()
//        {
//            base.OnResume();
//			try {
//				_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);	
//			} catch (Exception ex) {
//				
//			}
//
//        }
//        protected override void OnPause()
//        {
//            base.OnPause();
//            _locationManager.RemoveUpdates(this);
//        }
//        void InitializeLocationManager()
//        {
//            _locationManager = (LocationManager)GetSystemService(LocationService);
//            Criteria criteriaForLocationService = new Criteria
//            {
//                Accuracy = Accuracy.Fine
//            };
//            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);
//
//            if (acceptableLocationProviders.Any())
//            {
//                _locationProvider = acceptableLocationProviders.First();
//            }
//            else
//            {
//                _locationProvider = String.Empty;
//            }
//        }
        void ibtnReload_Click(object sender, EventArgs e)
        {
            mMap.Clear();
            cliente.DownloadDataAsync(WebServices["getDenuncias"]);
        }
        void ibtnSearch_Click(object sender, EventArgs e)
        {
            buscarLocacion();
        }
        void txtBuscar_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            buscarLocacion();
        }
        public void buscarLocacion()
        {
            //if (e.ActionId == Android.Views.InputMethods.ImeAction.Done)
            //{
            Geocoder geoCoder = new Geocoder(this);
            IList<Address> gotAddresses = null;
            gotAddresses = geoCoder.GetFromLocationName(txtBuscar.Text, 1);
            if (gotAddresses.Count > 0)
            {
                address = (Address)gotAddresses[0];
                Console.WriteLine("Country name: {0}\n Extras: {1}\n Feature Name: {2}\n Latitude: {3}\n Localty: {4}\n Longitude: {5}\n Thtoughfare: {6}",
                    address.CountryName, address.Extras, address.FeatureName, address.Latitude, address.Locality, address.Longitude, address.Thoroughfare);
                mMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(address.Latitude, address.Longitude), 16));
                txtBuscar.ClearFocus();
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(txtBuscar.WindowToken, 0);
                addCustomMarker(address.Latitude, address.Longitude);
            }
            else
            {
                alert.SetMessage("No hay coincidencias");
                alert.Show();
            }
            //}
            //else
            //{
            //    e.Handled = false;
            //}
        }
        public void addCustomMarker(double lat, double lng)
        {
            if (customMarker != null)
            {
                customMarker.Remove();
            }
            customMarker = mMap.AddMarker(new MarkerOptions()
                .SetPosition(new LatLng(lat, lng))
                .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.customMarker))
                .SetSnippet("customMarker")
                .Draggable(true));
            customMarker.ShowInfoWindow();
        }
        void cliente_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
			try {
            	RunOnUiThread(() =>
                {
						
                    string json = Encoding.UTF8.GetString(e.Result);
						//eliminar datos basura generados por 000webhost
					json = json.Replace("<!-- Hosting24 Analytics Code -->","");
					json = json.Replace("<script type=\"text/javascript\" src=\"http://stats.hosting24.com/count.php\"></script>","");
					json = json.Replace("<!-- End Of Analytics Code -->","");
                    Console.WriteLine(json);
                    Denuncias = JsonConvert.DeserializeObject<List<Denuncia>>(json);
                    loadDencunciasMarkers();
                });
			} catch (Exception ex) {
				Console.WriteLine (ex.ToString());
				alert.SetMessage ("Ocurrió un error al descargar los datos");
				alert.Show ();
			}
        }

        private void btnSatellite_Click(object sender, EventArgs e)
        {
            mMap.MapType = GoogleMap.MapTypeHybrid;
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            mMap.MapType = GoogleMap.MapTypeNormal;
        }
        private void SetUpMap()
        {
            if (mMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);                
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
			double lat = 13.7310;
			double lng = -89.1610;
			if (!this.geolocator.IsGeolocationAvailable || !this.geolocator.IsGeolocationEnabled) {
				alert.SetMessage ("Internet o GPS estan desactivados");
				alert.Show ();
				LatLng latlng = new LatLng (lat, lng);
				CameraUpdate marca_camera = CameraUpdateFactory.NewLatLngZoom (latlng, 12);
				mMap.MoveCamera (marca_camera);
			} else {
				this.geolocator.GetPositionAsync (timeout: 10000).ContinueWith (t => {
					lat = t.Result.Latitude;
					lng = t.Result.Longitude;
					LatLng latlng = new LatLng (lat, lng);
					CameraUpdate marca_camera = CameraUpdateFactory.NewLatLngZoom (latlng, 12);
					mMap.MoveCamera (marca_camera);
				}, TaskScheduler.FromCurrentSynchronizationContext ());
			}
            //MarkerOptions marca = new MarkerOptions()
            //    .SetPosition(latlng)
            //    .SetTitle("El Salvador")
            //    .SetSnippet("San Salvador")
            //    .Draggable(true);
            mMap.MarkerDragEnd +=mMap_MarkerDragEnd;
            //mMap.MarkerClick +=mMap_MarkerClick;
            mMap.MarkerDrag += mMap_MarkerDrag;
            mMap.SetInfoWindowAdapter(this);
            mMap.SetOnInfoWindowClickListener(this);
        }

        void mMap_MarkerDrag(object sender, GoogleMap.MarkerDragEventArgs e)
        {
            
        }

        private void mMap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            if (e.Marker.Snippet=="customMarker")
            {
                mMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(e.Marker.Position, 12));
                //e.Marker.HideInfoWindow();
                e.Marker.ShowInfoWindow();
            }
            else
            {
                e.Marker.ShowInfoWindow();
            }
        }

        private void mMap_MarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
			Geocoder geoCoder = new Geocoder(this);
			IList<Address> gotAddresses = null;
			gotAddresses = geoCoder.GetFromLocation(e.Marker.Position.Latitude, e.Marker.Position.Longitude,1);
			if (gotAddresses.Count > 0)
			{
				address = (Address)gotAddresses[0];
				e.Marker.ShowInfoWindow();
			}
        }

        public View GetInfoContents(Marker marker)
        {
            return null;
        }

        public View GetInfoWindow(Marker marker)
        {
            View view;
			byte[] image;
            if (marker.Snippet != "customMarker")
            {
                view = LayoutInflater.Inflate(Resource.Layout.info_window, null, false);
                var denuncia = (from d in Denuncias
                                where d.iddenuncia == Convert.ToInt32(marker.Snippet)
                                select d).ToList()[0];
				if (denuncia.imagebase64.Length <= 0) {
					//Toast.MakeText (this, "Cargando...", ToastLength.Short).Show();
					clienteUpload = new WebClient ();
					NameValueCollection parametros = new NameValueCollection ();
					parametros.Add ("iddenuncia",denuncia.iddenuncia.ToString());
					clienteUpload.UploadValuesAsync (WebServices["getDenunciaPicture"],parametros);
					clienteUpload.UploadValuesCompleted += (object sender, UploadValuesCompletedEventArgs e) => {
						RunOnUiThread(() =>
							{
								string result = Encoding.UTF8.GetString(e.Result);
								//eliminando datos basura de 000webhost
								result = result.Replace("<!-- Hosting24 Analytics Code -->","");
								result = result.Replace("<script type=\"text/javascript\" src=\"http://stats.hosting24.com/count.php\"></script>","");
								result = result.Replace("<!-- End Of Analytics Code -->","");
								result = result.Replace("\n","");
								result = result.Replace("\t","");
								result = result.Trim();
								denuncia.imagebase64 = result;	
								Console.WriteLine(denuncia.imagebase64);
								if (denuncia.imagebase64 != null && denuncia.imagebase64 != "" && denuncia.imagebase64.Length > 0)
								{
									image = Convert.FromBase64String(denuncia.imagebase64);
									view.FindViewById<ImageView>(Resource.Id.denuncia_miniatura).SetImageBitmap(BitmapFactory.DecodeByteArray(image, 0, image.Length));
									marker.ShowInfoWindow();
								}
							});
					};
				}
                view.FindViewById<TextView>(Resource.Id.txtNombre).Text = denuncia.nombretipo;
                view.FindViewById<TextView>(Resource.Id.txtDireccion).Text = denuncia.ciudad + ", " + denuncia.calle;
                view.FindViewById<TextView>(Resource.Id.txtNumero).Text = "Por " + denuncia.nombre;
				if (denuncia.imagebase64 != null && denuncia.imagebase64 != "" && denuncia.imagebase64.Length > 0) {
					image = Convert.FromBase64String (denuncia.imagebase64);
					view.FindViewById<ImageView> (Resource.Id.denuncia_miniatura).SetImageBitmap (BitmapFactory.DecodeByteArray (image, 0, image.Length));
				}
            }
            else
            {
                view = LayoutInflater.Inflate(Resource.Layout.DenunciarInfoWindow, null, false);
                view.FindViewById<TextView>(Resource.Id.txtvInfoCalle).Text = address.FeatureName;
                view.FindViewById<TextView>(Resource.Id.txtvInfoCiudad).Text = address.Locality;
                view.FindViewById<TextView>(Resource.Id.txtvInfoPais).Text = address.CountryName;
            }
            return view;
        }

        public void OnInfoWindowClick(Marker marker)
        {
            if (marker.Snippet!="customMarker")
            {
                Shared.denuncia = (from d in Denuncias
                                   where d.iddenuncia == Convert.ToInt32(marker.Snippet)
                                   select d).ToList()[0];
                StartActivity(typeof(DetalleDenunciaActivity));
            }
            else
            {
                Shared.address = address;
                var intent = new Intent(this, typeof(CrearDenunciaActivity));
                StartActivityForResult(intent, 0);
            }
            Console.WriteLine("Info window has been clicked");
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                mMap.Clear();
                Denuncia denuncia = JsonConvert.DeserializeObject<Denuncia>(data.GetStringExtra("denuncia"));
                Denuncias.Add(denuncia);
                loadDencunciasMarkers();
            }
        }

        private void loadDencunciasMarkers()
        {
            foreach (var denuncia in Denuncias)
            {
                Console.WriteLine("Denuncia: {0} {1} {2} {3} {4}", denuncia.iddenuncia, denuncia.pais, denuncia.ciudad, denuncia.lat, denuncia.lng);
                mMap.AddMarker(new MarkerOptions()
                    .SetPosition(new LatLng(denuncia.lat, denuncia.lng))
                    .SetSnippet(denuncia.iddenuncia.ToString())
                    //.SetIcon(BitmapDescriptorFactory.DefaultMarker(ColorTipoDenuncia[denuncia.idtipo])));
					.SetIcon(BitmapDescriptorFactory.FromResource(MarkerTipoDenuncia[denuncia.idtipo])));
            }
        }

//        public void OnLocationChanged(Location location)
//        {
//            _currentLocation = location;
//        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }
    }
}

