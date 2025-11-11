using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Perfil
    {
        public int PerfilID { get; set; }
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UrlFotoPerfil { get; set; }
        public string Localidad { get; set; }
        public Usuario Usuario { get; set; }
        public Perfil() { }
        public string NombreCompleto => $"{Nombre} {Apellido}";

    }

}


