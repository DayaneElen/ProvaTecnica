using System;
using System.ComponentModel.DataAnnotations;

namespace ProvaTecnica.Models
{
    public class DtoRegistroPonto
    {
        public int Id { get; set; }

        public DateTime Registro { get; set; }

        public string NomeUsuario { get; set; }

        public string Tipo { get; set; }

    }
}
