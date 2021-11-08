using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class ArchivoExcel
    {
        public string IdentificacionBeClever { get; set; }
        public string NumeroDeLote { get; set; }
        public string NumeroDeOperacion { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Dni { get; set; }
        public string Cuit { get; set; }
        public string Nombre { get; set; }
        public string CategoriaIva { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string CP { get; set; }
        public string Domicilio { get; set; }
        public int NumeroCuota { get; set; }
        public int Cuotas { get; set; }
        public string Moneda { get; set; }
        public string ConceptoTablaEquivalencia { get; set; }
        public string Descripcion { get; set; }
        public double ImporteSinIva { get; set; }
        public double Iva { get; set; }
        public double ImporteConIva { get; set; }
        public string Bonificacion { get; set; }

    }
}
