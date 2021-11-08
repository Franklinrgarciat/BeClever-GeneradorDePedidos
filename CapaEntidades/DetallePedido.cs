using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class DetallePedido
    {
		public int ID_GVA03 { get; set; }
		public string FILLER { get; set; }
		public decimal CAN_EQUI_V { get; set; }
		public decimal CANT_A_DES { get; set; }
		public decimal CANT_A_FAC { get; set; }
		public decimal CANT_PEDID { get; set; }
		public decimal CANT_PEN_D { get; set; }
		public decimal CANT_PEN_F { get; set; }
		public string COD_ARTICU { get; set; }
		public decimal DESCUENTO { get; set; }
		public int N_RENGLON { get; set; }
		public string NRO_PEDIDO { get; set; }
		public decimal PEN_REM_FC { get; set; }
		public decimal PEN_FAC_RE { get; set; }
		public decimal PRECIO { get; set; }
		public int TALON_PED { get; set; }
		public string COD_CLASIF { get; set; }
		public decimal CANT_A_DES_2 { get; set; }
		public decimal CANT_A_FAC_2 { get; set; }
		public decimal CANT_PEDID_2 { get; set; }
		public decimal CANT_PEN_D_2 { get; set; }
		public decimal CANT_PEN_F_2 { get; set; }
		public decimal PEN_REM_FC_2 { get; set; }
		public decimal PEN_FAC_RE_2 { get; set; }
		public int? ID_MEDIDA_VENTAS { get; set; }
		public int? ID_MEDIDA_STOCK_2 { get; set; }
		public int? ID_MEDIDA_STOCK { get; set; }
		public string UNIDAD_MEDIDA_SELECCIONADA { get; set; }
		public string COD_ARTICU_KIT { get; set; }
		public int RENGL_PADR { get; set; }
		public bool PROMOCION { get; set; }
		public decimal PRECIO_ADICIONAL_KIT { get; set; }
		public bool KIT_COMPLETO { get; set; }
		public bool INSUMO_KIT_SEPARADO { get; set; }
		public decimal PRECIO_LISTA { get; set; }
		public decimal PRECIO_BONIF { get; set; }
		public decimal DESCUENTO_PARAM { get; set; }
		public DateTime PRECIO_FECHA { get; set; }
		public DateTime FECHA_MODIFICACION_PRECIO { get; set; }
		public string USUARIO_MODIFICACION_PRECIO { get; set; }
		public string TERMINAL_MODIFICACION_PRECIO { get; set; }
		public int? ID_NEXO_PEDIDOS_RENGLON_ORDEN { get; set; }
	}
}
