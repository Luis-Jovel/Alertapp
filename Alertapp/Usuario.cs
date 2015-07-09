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
    class Usuario
    {
        public int idusuario { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string imagebase64 { get; set; }
    }
}