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
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetTheme(Android.Resource.Style.ThemeLight);
            SetContentView(Resource.Layout.CrearDenuncia);
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
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            this.StartActivityForResult(Intent.CreateChooser(intent, "Seleccionar Imagen"), 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                Stream stream = ContentResolver.OpenInputStream(data.Data);
                bitmap = BitmapFactory.DecodeStream(stream);
                selectedFoto.SetImageBitmap(bitmap);
            }
        }
        void btnInsertDenuncia_Click(object sender, EventArgs e)
        {
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
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    AlertDialog alert = builder.Create();
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