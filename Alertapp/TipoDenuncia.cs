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
    public class TipoDenuncia
    {
        public int idtipo;
        public string nombre;
        public TipoDenuncia(int idtipo, string nombre)
        {
            this.idtipo = idtipo;
            this.nombre = nombre;
        }
    }
}