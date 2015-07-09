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
using Android.Graphics;

namespace Alertapp
{
    [Activity(Label = "DetalleDenuncia")]
    public class DetalleDenunciaActivity : Activity
    {
        private TextView txtvTipo, txtvAutor, txtvDescripcion, txtvPais, txtvCiudad, txtvCalle;
        private ImageView imgFoto;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DetalleDenuncia);
            // Create your application here
            if (Shared.denuncia != null)
            {
                var metrics = Resources.DisplayMetrics;
                var WidthInDp = metrics.WidthPixels / Resources.DisplayMetrics.Density;
                txtvTipo = FindViewById<TextView>(Resource.Id.txtvTipo);
                txtvTipo.Text = Shared.denuncia.nombretipo;
                txtvAutor = FindViewById<TextView>(Resource.Id.txtvAutor);
                txtvAutor.Text = Shared.denuncia.nombre;
                txtvDescripcion = FindViewById<TextView>(Resource.Id.txtvDescripcion);
                txtvDescripcion.Text = Shared.denuncia.descripcion;
                txtvPais = FindViewById<TextView>(Resource.Id.txtvPais);
                txtvPais.Text = Shared.denuncia.pais;
                txtvCiudad = FindViewById<TextView>(Resource.Id.txtvCiudad);
                txtvCiudad.Text = Shared.denuncia.ciudad;
                txtvCalle = FindViewById<TextView>(Resource.Id.txtvCalle);
                txtvCalle.Text = Shared.denuncia.calle;
                imgFoto = FindViewById<ImageView>(Resource.Id.imgFoto);
                byte[] image;
                if (Shared.denuncia.imagebase64 != null && Shared.denuncia.imagebase64 != "")
	            {
                    image = Convert.FromBase64String(Shared.denuncia.imagebase64);
                    imgFoto.SetImageBitmap(BitmapFactory.DecodeByteArray(image, 0, image.Length));
                }
            }
        }
    }
}