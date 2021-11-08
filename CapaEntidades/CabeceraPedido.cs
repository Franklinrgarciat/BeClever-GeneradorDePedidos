using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class CabeceraPedido
    {
		public ClienteOcasional CLIENTE_OCASIONAL { get; set; }
		public int ID_GVA21 { get; set; }
		public string FILLER { get; set; }
		public string APRUEBA { get; set; }
		public int CIRCUITO { get; set; }
		public string COD_CLIENT { get; set; }
		public string COD_SUCURS { get; set; }
		public string COD_TRANSP { get; set; }
		public string COD_VENDED { get; set; }
		public string COMENTARIO { get; set; }
		public bool COMP_STK { get; set; }
		public int COND_VTA { get; set; }
		public decimal COTIZ { get; set; }
		public int ESTADO { get; set; }
		public bool EXPORTADO { get; set; }
		public DateTime FECHA_APRU { get; set; }
		public DateTime FECHA_ENTR { get; set; }
		public DateTime FECHA_PEDI { get; set; }
		public string HORA_APRUE { get; set; }
		public string ID_EXTERNO { get; set; }
		public string LEYENDA_1 { get; set; }
		public string LEYENDA_2 { get; set; }
		public string LEYENDA_3 { get; set; }
		public string LEYENDA_4 { get; set; }
		public string LEYENDA_5 { get; set; }
		public bool MON_CTE { get; set; }
		public int N_LISTA { get; set; }
		public string N_REMITO { get; set; }
		public string NRO_O_COMP { get; set; }
		public string NRO_PEDIDO { get; set; }
		public int NRO_SUCURS { get; set; }
		public string ORIGEN { get; set; }
		public decimal PORC_DESC { get; set; }
		public string REVISO_FAC { get; set; }
		public string REVISO_PRE { get; set; }
		public string REVISO_STK { get; set; }
		public int TALONARIO { get; set; }
		public int TALON_PED { get; set; }
		public decimal TOTAL_PEDI { get; set; }
		public string TIPO_ASIEN { get; set; }
		public string MOTIVO { get; set; }
		public string HORA { get; set; }
		public string COD_CLASIF { get; set; }
		public int ID_ASIENTO_MODELO_GV { get; set; }
		public int TAL_PE_ORI { get; set; }
		public string NRO_PE_ORI { get; set; }
		public DateTime FECHA_INGRESO { get; set; }
		public string HORA_INGRESO { get; set; }
		public string USUARIO_INGRESO { get; set; }
		public string TERMINAL_INGRESO { get; set; }
		public DateTime FECHA_ULTIMA_MODIFICACION { get; set; }
		public string HORA_ULTIMA_MODIFICACION { get; set; }
		public string USUA_ULTIMA_MODIFICACION { get; set; }
		public string TERM_ULTIMA_MODIFICACION { get; set; }
		public int? ID_DIRECCION_ENTREGA { get; set; }
		public bool ES_PEDIDO_WEB { get; set; }
		public DateTime FECHA_O_COMP { get; set; }
		public string ACTIVIDAD_COMPROBANTE_AFIP { get; set; }
		public int? ID_ACTIVIDAD_EMPRESA_AFIP { get; set; }
		public int TIPO_DOCUMENTO_PAGADOR { get; set; }
		public string NUMERO_DOCUMENTO_PAGADOR { get; set; }
		public int WEB_ORDER_ID { get; set; }
		public string USUARIO_TIENDA { get; set; }
		public string TIENDA { get; set; }
		public string ORDER_ID_TIENDA { get; set; }
		public string NRO_OC_COMP { get; set; }
		public decimal TOTAL_DESC_TIENDA { get; set; }
		public string TIENDA_QUE_VENDE { get; set; }
		public decimal PORCEN_DESC_TIENDA { get; set; }
		public string USUARIO_TIENDA_VENDEDOR { get; set; }
		public int? ID_NEXO_PEDIDOS_ORDEN { get; set; }

		public List<DetallePedido> ListDetalle { get; set; }
		public List<ClienteOcasional> ClienteOcasional { get; set; }
	}
}
