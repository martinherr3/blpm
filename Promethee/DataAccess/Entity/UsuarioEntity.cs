using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class UsuarioEntity
    {
        public int idUsuario { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }

        public string username { get; set; }
        public string password { get; set; }

    }
}
