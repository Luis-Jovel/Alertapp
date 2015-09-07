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
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.IO;
using Android.Graphics;
using Xamarin.Media;
using System.Threading.Tasks;

namespace Alertapp
{
    [Activity(Label = "Crear Denuncia", Theme = "@style/MyTheme")]
    public class CrearDenunciaActivity : Activity
    {
        private Spinner spnTipoNombre;
        private TextView txtvInsertPais, txtvInsertAutor, txtvinsertCiudad, txtvInsertCalle;
        private EditText txtDescripcion;
        private Button btnInsertDenuncia;
        private ImageView imgFoto, selectedFoto;
        private Bitmap bitmap = null;
        private string[] tipos;
        private int[] ids;
        private int selectedId;
        private WebClient cliente;
		AlertDialog alert;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetTheme(Android.Resource.Style.ThemeLight);
            SetContentView(Resource.Layout.CrearDenuncia);
			//Alert message
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			alert = builder.Create();
            if (Shared.address != null)
            {
                spnTipoNombre = FindViewById<Spinner>(Resource.Id.spnTipoNombre);
                spnTipoNombre.RequestFocus();
                tipos = new string[]{
                    "Corte de servicio",
                    "Fuga de Agua",
                    "Daño a Infraestructura",
                    "Otros"
                };
                ids = new int[] { 1, 2, 3, 4 };
                ArrayAdapter<string> spinnerArrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, tipos);
                spinnerArrayAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spnTipoNombre.Adapter = spinnerArrayAdapter;
                spnTipoNombre.ItemSelected += spnTipoNombre_ItemSelected;
                txtDescripcion = FindViewById<EditText>(Resource.Id.txtInsertDescripcion);
                txtvInsertPais = FindViewById<TextView>(Resource.Id.txtvInsertPais);
                txtvInsertAutor = FindViewById<TextView>(Resource.Id.txtvInsertAutor);
                txtvinsertCiudad = FindViewById<TextView>(Resource.Id.txtvInsertCiudad);
                txtvInsertCalle = FindViewById<TextView>(Resource.Id.txtvInsertCalle);
                btnInsertDenuncia = FindViewById<Button>(Resource.Id.btnInsertDenuncia);
                imgFoto = FindViewById<ImageView>(Resource.Id.imgInsertFoto);
                txtvInsertPais.Text = Shared.address.CountryName;
                txtvinsertCiudad.Text = Shared.address.Locality;
                txtvInsertCalle.Text = Shared.address.FeatureName;
                txtvInsertAutor.Text = "Luis Jovel";
                btnInsertDenuncia.Click += btnInsertDenuncia_Click;
                imgFoto.Click += imgFoto_Click;
            }
        }

        void imgFoto_Click(object sender, EventArgs e)
        {
            selectedFoto = (ImageView) sender;
            /*Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            this.StartActivityForResult(Intent.CreateChooser(intent, "Seleccionar Imagen"), 0);*/
			var picker = new MediaPicker (this);
			if (!picker.IsCameraAvailable) {
				alert.SetMessage("No se encontró la cámara");
				alert.Show();
			} else {
				var intent = picker.GetTakePhotoUI (new StoreCameraMediaOptions {
					Name = "text.jpg"/*,
					Directory = "Alertapp"*/
				});
				StartActivityForResult (intent, 1);
			}
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
//            base.OnActivityResult(requestCode, resultCode, data);
//            if (resultCode == Result.Ok)
//            {
//                Stream stream = ContentResolver.OpenInputStream(data.Data);
//                bitmap = BitmapFactory.DecodeStream(stream);
//                selectedFoto.SetImageBitmap(bitmap);
//            }
			// User canceled
			if (resultCode == Result.Canceled)
				return;

			data.GetMediaFileExtraAsync (this).ContinueWith (t => {
				//Console.WriteLine (t.Result.Path);
				DecodeBitmapAsync (t.Result.Path, 400, 400).ContinueWith (th => {
					this.imgFoto.SetImageBitmap (this.bitmap = th.Result);
				}, TaskScheduler.FromCurrentSynchronizationContext());
			}, TaskScheduler.FromCurrentSynchronizationContext());
        }
		private static Task<Bitmap> DecodeBitmapAsync (string path, int desiredWidth, int desiredHeight)
		{
			return Task.Factory.StartNew (() => {
				BitmapFactory.Options options = new BitmapFactory.Options();
				options.InJustDecodeBounds = true;
				BitmapFactory.DecodeFile (path, options);

				int height = options.OutHeight;
				int width = options.OutWidth;

				int sampleSize = 1;
				if (height > desiredHeight || width > desiredWidth) {
					int heightRatio = (int)Math.Round ((float)height / (float)desiredHeight);
					int widthRatio = (int)Math.Round ((float)width / (float)desiredWidth);
					sampleSize = Math.Min (heightRatio, widthRatio);
				}

				options = new BitmapFactory.Options();
				options.InSampleSize = sampleSize;

				return BitmapFactory.DecodeFile (path, options);
			});
		}
        void btnInsertDenuncia_Click(object sender, EventArgs e)
        {
			Toast.MakeText (this, "Creando denuncia...", ToastLength.Short).Show();
            cliente = new WebClient();
            NameValueCollection parametros = new NameValueCollection();
            parametros.Add("idusuario", "1");
            parametros.Add("idtipo", selectedId.ToString());
            parametros.Add("pais", Shared.address.CountryName);
            parametros.Add("ciudad", Shared.address.Locality);
            parametros.Add("calle", Shared.address.FeatureName);
            parametros.Add("lat", Shared.address.Latitude.ToString());
            parametros.Add("lng", Shared.address.Longitude.ToString());
            parametros.Add("descripcion", txtDescripcion.Text);
            if (bitmap != null)
            {
                MemoryStream memStream = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Webp, 100, memStream);
                byte[] fotoData = memStream.ToArray();
                parametros.Add("imagebase64", Convert.ToBase64String(fotoData));
            }
            else
            {
                parametros.Add("imagebase64", null);
            }
            cliente.UploadValuesAsync(MainActivity.WebServices["setDenuncia"],parametros);
            cliente.UploadValuesCompleted += cliente_UploadValuesCompleted;
        }

        void cliente_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() => {
                string json = Encoding.UTF8.GetString(e.Result);
                if (json != "error")
                {
                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.PutExtra("denuncia", json);
                    SetResult(Result.Ok, intent);
                    Finish();
                }
                else
                {
                    alert.SetMessage("Error durante la creacion de denuncia, intentalo denuevo");
                    alert.Show();
                }
                
            });
        }

        void spnTipoNombre_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selectedId = ids[e.Position];
            Console.WriteLine(selectedId);
        }
    }
}