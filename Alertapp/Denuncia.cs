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

namespace Alertapp
{
    public class Denuncia
    {
        //denuncia
        public int iddenuncia { get; set; }
        public int idusuario { get; set; }
        public int idtipo { get; set; }
        public string pais { get; set; }
        public string ciudad { get; set; }
        public string calle { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public string descripcion { get; set; }
        public string imagebase64 { get; set; }
        public string creado_en { get; set; }
        //usuario
        public string nombre { get; set; }
        public string email { get; set; }
        public string imagebase64_usuario { get; set; }
        //tipo
        public string nombretipo { get; set; }
    }
}