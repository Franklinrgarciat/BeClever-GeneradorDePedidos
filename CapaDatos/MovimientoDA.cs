using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class MovimientoDA
    {
		private string ConexionBase = ConfigurationManager.AppSettings.Get("Conexion").ToString();
		public bool InsertarPedido(CapaEntidades.CabeceraPedido pedido)
		{
			bool guardo = false;
			SqlCommand cm = new SqlCommand();
			SqlDataReader dr = null;
			try
			{
				using (var cn = new SqlConnection(ConexionBase))
				{
					cn.Open();
					using (var tran = cn.BeginTransaction())
					{

						cm = new SqlCommand();
						cm.Connection = cn;
						cm.Transaction = tran;

						string tipo = string.Empty;
						string sucursal = string.Empty;
						string proximo = string.Empty;

						//TRAER PROXIMO NUMERO DE PEDIDO
						cm.CommandText = "SELECT TIPO, SUCURSAL, dbo.Fn_obtenerproximonumero(PROXIMO) AS PROXIMO FROM GVA43 WHERE TALONARIO = @TALONARIO";
						cm.Parameters.Clear();
						cm.Parameters.Add("@TALONARIO", SqlDbType.Int).Value = pedido.TALON_PED;
						dr = cm.ExecuteReader();
						if (dr.Read())
						{
							tipo = dr.GetString(0);
							sucursal = dr.GetString(1);
							proximo = dr.GetString(2);
						}
						else
							throw new Exception("Error al obtener el próximo número de pedido");

						dr.Close();

						pedido.NRO_PEDIDO = string.Format("{0}{1}{2}", tipo, sucursal, proximo);

						//INSERTAR CABECERA
						cm.CommandText =
							@"
								INSERT INTO GVA21
								(
									FILLER,
									APRUEBA,
									CIRCUITO,
									COD_CLIENT,
									COD_SUCURS,
									COD_TRANSP,
									COD_VENDED,
									COMENTARIO,
									COMP_STK,
									COND_VTA,
									COTIZ,
									ESTADO,
									EXPORTADO,
									FECHA_APRU,
									FECHA_ENTR,
									FECHA_PEDI,
									HORA_APRUE,
									ID_EXTERNO,
									LEYENDA_1,
									LEYENDA_2,
									LEYENDA_3,
									LEYENDA_4,
									LEYENDA_5,
									MON_CTE,
									N_LISTA,
									N_REMITO,
									NRO_O_COMP,
									NRO_PEDIDO,
									NRO_SUCURS,
									ORIGEN,
									PORC_DESC,
									REVISO_FAC,
									REVISO_PRE,
									REVISO_STK,
									TALONARIO,
									TALON_PED,
									TOTAL_PEDI,
									TIPO_ASIEN,
									MOTIVO,
									HORA,
									COD_CLASIF,
									ID_ASIENTO_MODELO_GV,
									TAL_PE_ORI,
									NRO_PE_ORI,
									FECHA_INGRESO,
									HORA_INGRESO,
									USUARIO_INGRESO,
									TERMINAL_INGRESO,
									FECHA_ULTIMA_MODIFICACION,
									HORA_ULTIMA_MODIFICACION,
									USUA_ULTIMA_MODIFICACION,
									TERM_ULTIMA_MODIFICACION,
									ID_DIRECCION_ENTREGA,
									ES_PEDIDO_WEB,
									FECHA_O_COMP,
									ACTIVIDAD_COMPROBANTE_AFIP,
									ID_ACTIVIDAD_EMPRESA_AFIP,
									TIPO_DOCUMENTO_PAGADOR,
									NUMERO_DOCUMENTO_PAGADOR,
									WEB_ORDER_ID,
									USUARIO_TIENDA,
									TIENDA,
									ORDER_ID_TIENDA,
									NRO_OC_COMP,
									TOTAL_DESC_TIENDA,
									TIENDA_QUE_VENDE,
									PORCEN_DESC_TIENDA,
									USUARIO_TIENDA_VENDEDOR,
									ID_NEXO_PEDIDOS_ORDEN
								)
								VALUES
								(
									@FILLER,
									@APRUEBA,
									@CIRCUITO,
									@COD_CLIENT,
									@COD_SUCURS,
									@COD_TRANSP,
									@COD_VENDED,
									@COMENTARIO,
									@COMP_STK,
									@COND_VTA,
									@COTIZ,
									@ESTADO,
									@EXPORTADO,
									@FECHA_APRU,
									@FECHA_ENTR,
									@FECHA_PEDI,
									@HORA_APRUE,
									@ID_EXTERNO,
									@LEYENDA_1,
									@LEYENDA_2,
									@LEYENDA_3,
									@LEYENDA_4,
									@LEYENDA_5,
									@MON_CTE,
									@N_LISTA,
									@N_REMITO,
									@NRO_O_COMP,
									@NRO_PEDIDO,
									@NRO_SUCURS,
									@ORIGEN,
									@PORC_DESC,
									@REVISO_FAC,
									@REVISO_PRE,
									@REVISO_STK,
									@TALONARIO,
									@TALON_PED,
									@TOTAL_PEDI,
									@TIPO_ASIEN,
									@MOTIVO,
									@HORA,
									@COD_CLASIF,
									@ID_ASIENTO_MODELO_GV,
									@TAL_PE_ORI,
									@NRO_PE_ORI,
									@FECHA_INGRESO,
									@HORA_INGRESO,
									@USUARIO_INGRESO,
									@TERMINAL_INGRESO,
									@FECHA_ULTIMA_MODIFICACION,
									@HORA_ULTIMA_MODIFICACION,
									@USUA_ULTIMA_MODIFICACION,
									@TERM_ULTIMA_MODIFICACION,
									@ID_DIRECCION_ENTREGA,
									@ES_PEDIDO_WEB,
									@FECHA_O_COMP,
									@ACTIVIDAD_COMPROBANTE_AFIP,
									@ID_ACTIVIDAD_EMPRESA_AFIP,
									@TIPO_DOCUMENTO_PAGADOR,
									@NUMERO_DOCUMENTO_PAGADOR,
									@WEB_ORDER_ID,
									@USUARIO_TIENDA,
									@TIENDA,
									@ORDER_ID_TIENDA,
									@NRO_OC_COMP,
									@TOTAL_DESC_TIENDA,
									@TIENDA_QUE_VENDE,
									@PORCEN_DESC_TIENDA,
									@USUARIO_TIENDA_VENDEDOR,
									@ID_NEXO_PEDIDOS_ORDEN
								)
							";
						cm.Parameters.Clear();
						cm.Parameters.Add("@FILLER", SqlDbType.VarChar).Value = pedido.FILLER;
						cm.Parameters.Add("@APRUEBA", SqlDbType.VarChar).Value = pedido.APRUEBA;
						cm.Parameters.Add("@CIRCUITO", SqlDbType.Int).Value = pedido.CIRCUITO;
						cm.Parameters.Add("@COD_CLIENT", SqlDbType.VarChar).Value = pedido.COD_CLIENT;
						cm.Parameters.Add("@COD_SUCURS", SqlDbType.VarChar).Value = pedido.COD_SUCURS;
						cm.Parameters.Add("@COD_TRANSP", SqlDbType.VarChar).Value = pedido.COD_TRANSP;
						cm.Parameters.Add("@COD_VENDED", SqlDbType.VarChar).Value = pedido.COD_VENDED;
						cm.Parameters.Add("@COMENTARIO", SqlDbType.VarChar).Value = pedido.COMENTARIO;
						cm.Parameters.Add("@COMP_STK", SqlDbType.Bit).Value = pedido.COMP_STK;
						cm.Parameters.Add("@COND_VTA", SqlDbType.Int).Value = pedido.COND_VTA;
						cm.Parameters.Add("@COTIZ", SqlDbType.Decimal).Value = pedido.COTIZ;
						cm.Parameters.Add("@ESTADO", SqlDbType.Int).Value = pedido.ESTADO;
						cm.Parameters.Add("@EXPORTADO", SqlDbType.Bit).Value = pedido.EXPORTADO;
						cm.Parameters.Add("@FECHA_APRU", SqlDbType.DateTime).Value = pedido.FECHA_APRU;
						cm.Parameters.Add("@FECHA_ENTR", SqlDbType.DateTime).Value = pedido.FECHA_ENTR;
						cm.Parameters.Add("@FECHA_PEDI", SqlDbType.DateTime).Value = pedido.FECHA_PEDI;
						cm.Parameters.Add("@HORA_APRUE", SqlDbType.VarChar).Value = pedido.HORA_APRUE;
						cm.Parameters.Add("@ID_EXTERNO", SqlDbType.VarChar).Value = pedido.ID_EXTERNO;
						cm.Parameters.Add("@LEYENDA_1", SqlDbType.VarChar).Value = pedido.LEYENDA_1;
						cm.Parameters.Add("@LEYENDA_2", SqlDbType.VarChar).Value = pedido.LEYENDA_2;
						cm.Parameters.Add("@LEYENDA_3", SqlDbType.VarChar).Value = pedido.LEYENDA_3;
						cm.Parameters.Add("@LEYENDA_4", SqlDbType.VarChar).Value = pedido.LEYENDA_4;
						cm.Parameters.Add("@LEYENDA_5", SqlDbType.VarChar).Value = pedido.LEYENDA_5;
						cm.Parameters.Add("@MON_CTE", SqlDbType.Bit).Value = pedido.MON_CTE;
						cm.Parameters.Add("@N_LISTA", SqlDbType.Int).Value = pedido.N_LISTA;
						cm.Parameters.Add("@N_REMITO", SqlDbType.VarChar).Value = pedido.N_REMITO;
						cm.Parameters.Add("@NRO_O_COMP", SqlDbType.VarChar).Value = pedido.NRO_O_COMP;
						cm.Parameters.Add("@NRO_PEDIDO", SqlDbType.VarChar).Value = pedido.NRO_PEDIDO;
						cm.Parameters.Add("@NRO_SUCURS", SqlDbType.Int).Value = pedido.NRO_SUCURS;
						cm.Parameters.Add("@ORIGEN", SqlDbType.VarChar).Value = pedido.ORIGEN;
						cm.Parameters.Add("@PORC_DESC", SqlDbType.Decimal).Value = pedido.PORC_DESC;
						cm.Parameters.Add("@REVISO_FAC", SqlDbType.VarChar).Value = pedido.REVISO_FAC;
						cm.Parameters.Add("@REVISO_PRE", SqlDbType.VarChar).Value = pedido.REVISO_PRE;
						cm.Parameters.Add("@REVISO_STK", SqlDbType.VarChar).Value = pedido.REVISO_STK;
						cm.Parameters.Add("@TALONARIO", SqlDbType.Int).Value = pedido.TALONARIO;
						cm.Parameters.Add("@TALON_PED", SqlDbType.Int).Value = pedido.TALON_PED;
						cm.Parameters.Add("@TOTAL_PEDI", SqlDbType.Decimal).Value = pedido.TOTAL_PEDI;
						cm.Parameters.Add("@TIPO_ASIEN", SqlDbType.VarChar).Value = pedido.TIPO_ASIEN;
						cm.Parameters.Add("@MOTIVO", SqlDbType.VarChar).Value = pedido.MOTIVO;
						cm.Parameters.Add("@HORA", SqlDbType.VarChar).Value = pedido.HORA;
						cm.Parameters.Add("@COD_CLASIF", SqlDbType.VarChar).Value = pedido.COD_CLASIF;
						cm.Parameters.Add("@ID_ASIENTO_MODELO_GV", SqlDbType.Int).Value = pedido.ID_ASIENTO_MODELO_GV;
						cm.Parameters.Add("@TAL_PE_ORI", SqlDbType.Int).Value = pedido.TAL_PE_ORI;
						cm.Parameters.Add("@NRO_PE_ORI", SqlDbType.VarChar).Value = pedido.NRO_PE_ORI;
						cm.Parameters.Add("@FECHA_INGRESO", SqlDbType.DateTime).Value = pedido.FECHA_INGRESO;
						cm.Parameters.Add("@HORA_INGRESO", SqlDbType.VarChar).Value = pedido.HORA_INGRESO;
						cm.Parameters.Add("@USUARIO_INGRESO", SqlDbType.VarChar).Value = pedido.USUARIO_INGRESO;
						cm.Parameters.Add("@TERMINAL_INGRESO", SqlDbType.VarChar).Value = pedido.TERMINAL_INGRESO;
						cm.Parameters.Add("@FECHA_ULTIMA_MODIFICACION", SqlDbType.DateTime).Value = pedido.FECHA_ULTIMA_MODIFICACION;
						cm.Parameters.Add("@HORA_ULTIMA_MODIFICACION", SqlDbType.VarChar).Value = pedido.HORA_ULTIMA_MODIFICACION;
						cm.Parameters.Add("@USUA_ULTIMA_MODIFICACION", SqlDbType.VarChar).Value = pedido.USUA_ULTIMA_MODIFICACION;
						cm.Parameters.Add("@TERM_ULTIMA_MODIFICACION", SqlDbType.VarChar).Value = pedido.TERM_ULTIMA_MODIFICACION;
						cm.Parameters.Add("@ID_DIRECCION_ENTREGA", SqlDbType.Int).Value = (pedido.ID_DIRECCION_ENTREGA == null) ? (object)DBNull.Value : pedido.ID_DIRECCION_ENTREGA;
						cm.Parameters.Add("@ES_PEDIDO_WEB", SqlDbType.Bit).Value = pedido.ES_PEDIDO_WEB;
						cm.Parameters.Add("@FECHA_O_COMP", SqlDbType.DateTime).Value = pedido.FECHA_O_COMP;
						cm.Parameters.Add("@ACTIVIDAD_COMPROBANTE_AFIP", SqlDbType.VarChar).Value = pedido.ACTIVIDAD_COMPROBANTE_AFIP;
						cm.Parameters.Add("@ID_ACTIVIDAD_EMPRESA_AFIP", SqlDbType.Int).Value = (pedido.ID_ACTIVIDAD_EMPRESA_AFIP == null) ? (object)DBNull.Value : pedido.ID_ACTIVIDAD_EMPRESA_AFIP;
						cm.Parameters.Add("@TIPO_DOCUMENTO_PAGADOR", SqlDbType.Int).Value = pedido.TIPO_DOCUMENTO_PAGADOR;
						cm.Parameters.Add("@NUMERO_DOCUMENTO_PAGADOR", SqlDbType.VarChar).Value = pedido.NUMERO_DOCUMENTO_PAGADOR;
						cm.Parameters.Add("@WEB_ORDER_ID", SqlDbType.Int).Value = pedido.WEB_ORDER_ID;
						cm.Parameters.Add("@USUARIO_TIENDA", SqlDbType.VarChar).Value = pedido.USUARIO_TIENDA;
						cm.Parameters.Add("@TIENDA", SqlDbType.VarChar).Value = pedido.TIENDA;
						cm.Parameters.Add("@ORDER_ID_TIENDA", SqlDbType.VarChar).Value = pedido.ORDER_ID_TIENDA;
						cm.Parameters.Add("@NRO_OC_COMP", SqlDbType.VarChar).Value = pedido.NRO_OC_COMP;
						cm.Parameters.Add("@TOTAL_DESC_TIENDA", SqlDbType.Decimal).Value = pedido.TOTAL_DESC_TIENDA;
						cm.Parameters.Add("@TIENDA_QUE_VENDE", SqlDbType.VarChar).Value = pedido.TIENDA_QUE_VENDE;
						cm.Parameters.Add("@PORCEN_DESC_TIENDA", SqlDbType.Decimal).Value = pedido.PORCEN_DESC_TIENDA;
						cm.Parameters.Add("@USUARIO_TIENDA_VENDEDOR", SqlDbType.VarChar).Value = pedido.USUARIO_TIENDA_VENDEDOR;
						cm.Parameters.Add("@ID_NEXO_PEDIDOS_ORDEN", SqlDbType.Int).Value = (pedido.ID_NEXO_PEDIDOS_ORDEN == null) ? (object)DBNull.Value : pedido.ID_NEXO_PEDIDOS_ORDEN;
						cm.ExecuteNonQuery();

						//INSERTAR DETALLE
						cm.CommandText =
							@"
								INSERT INTO GVA03
								(
									FILLER,
									CAN_EQUI_V,
									CANT_A_DES,
									CANT_A_FAC,
									CANT_PEDID,
									CANT_PEN_D,
									CANT_PEN_F,
									COD_ARTICU,
									DESCUENTO,
									N_RENGLON,
									NRO_PEDIDO,
									PEN_REM_FC,
									PEN_FAC_RE,
									PRECIO,
									TALON_PED,
									COD_CLASIF,
									CANT_A_DES_2,
									CANT_A_FAC_2,
									CANT_PEDID_2,
									CANT_PEN_D_2,
									CANT_PEN_F_2,
									PEN_REM_FC_2,
									PEN_FAC_RE_2,
									ID_MEDIDA_VENTAS,
									ID_MEDIDA_STOCK_2,
									ID_MEDIDA_STOCK,
									UNIDAD_MEDIDA_SELECCIONADA,
									COD_ARTICU_KIT,
									RENGL_PADR,
									PROMOCION,
									PRECIO_ADICIONAL_KIT,
									KIT_COMPLETO,
									INSUMO_KIT_SEPARADO,
									PRECIO_LISTA,
									PRECIO_BONIF,
									DESCUENTO_PARAM,
									PRECIO_FECHA,
									FECHA_MODIFICACION_PRECIO,
									USUARIO_MODIFICACION_PRECIO,
									TERMINAL_MODIFICACION_PRECIO,
									ID_NEXO_PEDIDOS_RENGLON_ORDEN
								)
								VALUES
								(
									@FILLER,
									@CAN_EQUI_V,
									@CANT_A_DES,
									@CANT_A_FAC,
									@CANT_PEDID,
									@CANT_PEN_D,
									@CANT_PEN_F,
									@COD_ARTICU,
									@DESCUENTO,
									@N_RENGLON,
									@NRO_PEDIDO,
									@PEN_REM_FC,
									@PEN_FAC_RE,
									@PRECIO,
									@TALON_PED,
									@COD_CLASIF,
									@CANT_A_DES_2,
									@CANT_A_FAC_2,
									@CANT_PEDID_2,
									@CANT_PEN_D_2,
									@CANT_PEN_F_2,
									@PEN_REM_FC_2,
									@PEN_FAC_RE_2,
									@ID_MEDIDA_VENTAS,
									@ID_MEDIDA_STOCK_2,
									@ID_MEDIDA_STOCK,
									@UNIDAD_MEDIDA_SELECCIONADA,
									@COD_ARTICU_KIT,
									@RENGL_PADR,
									@PROMOCION,
									@PRECIO_ADICIONAL_KIT,
									@KIT_COMPLETO,
									@INSUMO_KIT_SEPARADO,
									@PRECIO_LISTA,
									@PRECIO_BONIF,
									@DESCUENTO_PARAM,
									@PRECIO_FECHA,
									@FECHA_MODIFICACION_PRECIO,
									@USUARIO_MODIFICACION_PRECIO,
									@TERMINAL_MODIFICACION_PRECIO,
									@ID_NEXO_PEDIDOS_RENGLON_ORDEN
								)
							";
						foreach (var detalle in pedido.ListDetalle)
						{
							cm.Parameters.Clear();
							cm.Parameters.Add("@FILLER", SqlDbType.VarChar).Value = detalle.FILLER;
							cm.Parameters.Add("@CAN_EQUI_V", SqlDbType.Decimal).Value = detalle.CAN_EQUI_V;
							cm.Parameters.Add("@CANT_A_DES", SqlDbType.Decimal).Value = detalle.CANT_A_DES;
							cm.Parameters.Add("@CANT_A_FAC", SqlDbType.Decimal).Value = detalle.CANT_A_FAC;
							cm.Parameters.Add("@CANT_PEDID", SqlDbType.Decimal).Value = detalle.CANT_PEDID;
							cm.Parameters.Add("@CANT_PEN_D", SqlDbType.Decimal).Value = detalle.CANT_PEN_D;
							cm.Parameters.Add("@CANT_PEN_F", SqlDbType.Decimal).Value = detalle.CANT_PEN_F;
							cm.Parameters.Add("@COD_ARTICU", SqlDbType.VarChar).Value = detalle.COD_ARTICU;
							cm.Parameters.Add("@DESCUENTO", SqlDbType.Decimal).Value = detalle.DESCUENTO;
							cm.Parameters.Add("@N_RENGLON", SqlDbType.Int).Value = detalle.N_RENGLON;
							cm.Parameters.Add("@NRO_PEDIDO", SqlDbType.VarChar).Value = pedido.NRO_PEDIDO;
							cm.Parameters.Add("@PEN_REM_FC", SqlDbType.Decimal).Value = detalle.PEN_REM_FC;
							cm.Parameters.Add("@PEN_FAC_RE", SqlDbType.Decimal).Value = detalle.PEN_FAC_RE;
							cm.Parameters.Add("@PRECIO", SqlDbType.Decimal).Value = detalle.PRECIO;
							cm.Parameters.Add("@TALON_PED", SqlDbType.Int).Value = detalle.TALON_PED;
							cm.Parameters.Add("@COD_CLASIF", SqlDbType.VarChar).Value = detalle.COD_CLASIF;
							cm.Parameters.Add("@CANT_A_DES_2", SqlDbType.Decimal).Value = detalle.CANT_A_DES_2;
							cm.Parameters.Add("@CANT_A_FAC_2", SqlDbType.Decimal).Value = detalle.CANT_A_FAC_2;
							cm.Parameters.Add("@CANT_PEDID_2", SqlDbType.Decimal).Value = detalle.CANT_PEDID_2;
							cm.Parameters.Add("@CANT_PEN_D_2", SqlDbType.Decimal).Value = detalle.CANT_PEN_D_2;
							cm.Parameters.Add("@CANT_PEN_F_2", SqlDbType.Decimal).Value = detalle.CANT_PEN_F_2;
							cm.Parameters.Add("@PEN_REM_FC_2", SqlDbType.Decimal).Value = detalle.PEN_REM_FC_2;
							cm.Parameters.Add("@PEN_FAC_RE_2", SqlDbType.Decimal).Value = detalle.PEN_FAC_RE_2;
							cm.Parameters.Add("@ID_MEDIDA_VENTAS", SqlDbType.Int).Value = (detalle.ID_MEDIDA_VENTAS == null || detalle.ID_MEDIDA_VENTAS == -1) ? (object)DBNull.Value : detalle.ID_MEDIDA_VENTAS;
							cm.Parameters.Add("@ID_MEDIDA_STOCK_2", SqlDbType.Int).Value = (detalle.ID_MEDIDA_STOCK_2 == null || detalle.ID_MEDIDA_STOCK_2 == -1) ? (object)DBNull.Value : detalle.ID_MEDIDA_STOCK_2;
							cm.Parameters.Add("@ID_MEDIDA_STOCK", SqlDbType.Int).Value = (detalle.ID_MEDIDA_STOCK == null || detalle.ID_MEDIDA_STOCK == -1) ? (object)DBNull.Value : detalle.ID_MEDIDA_STOCK;
							cm.Parameters.Add("@UNIDAD_MEDIDA_SELECCIONADA", SqlDbType.VarChar).Value = detalle.UNIDAD_MEDIDA_SELECCIONADA;
							cm.Parameters.Add("@COD_ARTICU_KIT", SqlDbType.VarChar).Value = detalle.COD_ARTICU_KIT;
							cm.Parameters.Add("@RENGL_PADR", SqlDbType.Int).Value = detalle.RENGL_PADR;
							cm.Parameters.Add("@PROMOCION", SqlDbType.Bit).Value = detalle.PROMOCION;
							cm.Parameters.Add("@PRECIO_ADICIONAL_KIT", SqlDbType.Int).Value = detalle.PRECIO_ADICIONAL_KIT;
							cm.Parameters.Add("@KIT_COMPLETO", SqlDbType.Bit).Value = detalle.KIT_COMPLETO;
							cm.Parameters.Add("@INSUMO_KIT_SEPARADO", SqlDbType.Bit).Value = detalle.INSUMO_KIT_SEPARADO;
							cm.Parameters.Add("@PRECIO_LISTA", SqlDbType.Decimal).Value = detalle.PRECIO_LISTA;
							cm.Parameters.Add("@PRECIO_BONIF", SqlDbType.Decimal).Value = detalle.PRECIO_BONIF;
							cm.Parameters.Add("@DESCUENTO_PARAM", SqlDbType.Decimal).Value = detalle.DESCUENTO_PARAM;
							cm.Parameters.Add("@PRECIO_FECHA", SqlDbType.DateTime).Value = detalle.PRECIO_FECHA;
							cm.Parameters.Add("@FECHA_MODIFICACION_PRECIO", SqlDbType.DateTime).Value = detalle.FECHA_MODIFICACION_PRECIO;
							cm.Parameters.Add("@USUARIO_MODIFICACION_PRECIO", SqlDbType.VarChar).Value = detalle.USUARIO_MODIFICACION_PRECIO;
							cm.Parameters.Add("@TERMINAL_MODIFICACION_PRECIO", SqlDbType.VarChar).Value = detalle.TERMINAL_MODIFICACION_PRECIO;
							cm.Parameters.Add("@ID_NEXO_PEDIDOS_RENGLON_ORDEN", SqlDbType.Int).Value = (detalle.ID_NEXO_PEDIDOS_RENGLON_ORDEN == null) ? (object)DBNull.Value : detalle.ID_NEXO_PEDIDOS_RENGLON_ORDEN;
							cm.ExecuteNonQuery();
						}

						if (pedido.CLIENTE_OCASIONAL != null)
						{
							//INSERTAR CLIENTE OCASIONAL
							cm.CommandText =
								@"
								INSERT INTO GVA38
								(
									FILLER,
									ALI_ADI_IB,
									ALI_FIJ_IB,
									ALI_NOCATE,
									AL_FIJ_IB3,
									COD_PROVIN,
									C_POSTAL,
									DOMICILIO,
									E_MAIL,
									IB_L,
									IB_L3,
									II_D,
									II_IB3,
									II_L,
									IVA_D,
									IVA_L,
									LOCALIDAD,
									N_COMP,
									N_CUIT,
									N_ING_BRUT,
									N_IVA,
									PORC_EXCL,
									RAZON_SOCI,
									SOBRE_II,
									SOBRE_IVA,
									TALONARIO,
									TELEFONO_1,
									TELEFONO_2,
									TIPO,
									TIPO_DOC,
									T_COMP,
									DESTINO_DE,
									CLA_IMP_CL,
									RECIBE_DE,
									AUT_DE,
									WEB,
									COD_RUBRO,
									CTA_CLI,
									CTO_CLI,
									IDENTIF_AFIP,
									DIRECCION_ENTREGA,
									CIUDAD_ENTREGA,
									COD_PROVINCIA_ENTREGA,
									LOCALIDAD_ENTREGA,
									CODIGO_POSTAL_ENTREGA,
									TELEFONO1_ENTREGA,
									TELEFONO2_ENTREGA,
									ID_CATEGORIA_IVA,
									CONSIDERA_IVA_BASE_CALCULO_IIBB,
									CONSIDERA_IVA_BASE_CALCULO_IIBB_ADIC,
									MAIL_DE,
									FECHA_NACIMIENTO,
									SEXO
								)
								VALUES
								(
									@FILLER,
									@ALI_ADI_IB,
									@ALI_FIJ_IB,
									@ALI_NOCATE,
									@AL_FIJ_IB3,
									@COD_PROVIN,
									@C_POSTAL,
									@DOMICILIO,
									@E_MAIL,
									@IB_L,
									@IB_L3,
									@II_D,
									@II_IB3,
									@II_L,
									@IVA_D,
									@IVA_L,
									@LOCALIDAD,
									@N_COMP,
									@N_CUIT,
									@N_ING_BRUT,
									@N_IVA,
									@PORC_EXCL,
									@RAZON_SOCI,
									@SOBRE_II,
									@SOBRE_IVA,
									@TALONARIO,
									@TELEFONO_1,
									@TELEFONO_2,
									@TIPO,
									@TIPO_DOC,
									@T_COMP,
									@DESTINO_DE,
									@CLA_IMP_CL,
									@RECIBE_DE,
									@AUT_DE,
									@WEB,
									@COD_RUBRO,
									@CTA_CLI,
									@CTO_CLI,
									@IDENTIF_AFIP,
									@DIRECCION_ENTREGA,
									@CIUDAD_ENTREGA,
									@COD_PROVINCIA_ENTREGA,
									@LOCALIDAD_ENTREGA,
									@CODIGO_POSTAL_ENTREGA,
									@TELEFONO1_ENTREGA,
									@TELEFONO2_ENTREGA,
									@ID_CATEGORIA_IVA,
									@CONSIDERA_IVA_BASE_CALCULO_IIBB,
									@CONSIDERA_IVA_BASE_CALCULO_IIBB_ADIC,
									@MAIL_DE,
									@FECHA_NACIMIENTO,
									@SEXO
								)
							";
							cm.Parameters.Clear();
							cm.Parameters.Add("@FILLER", SqlDbType.VarChar, 20).Value = pedido.CLIENTE_OCASIONAL.FILLER;
							cm.Parameters.Add("@ALI_ADI_IB", SqlDbType.Int).Value = pedido.CLIENTE_OCASIONAL.ALI_ADI_IB;
							cm.Parameters.Add("@ALI_FIJ_IB", SqlDbType.Int).Value = pedido.CLIENTE_OCASIONAL.ALI_FIJ_IB;
							cm.Parameters.Add("@ALI_NOCATE", SqlDbType.Int).Value = pedido.CLIENTE_OCASIONAL.ALI_NOCATE;
							cm.Parameters.Add("@AL_FIJ_IB3", SqlDbType.Int).Value = pedido.CLIENTE_OCASIONAL.AL_FIJ_IB3;
							cm.Parameters.Add("@COD_PROVIN", SqlDbType.VarChar, 2).Value = pedido.CLIENTE_OCASIONAL.COD_PROVIN;
							cm.Parameters.Add("@C_POSTAL", SqlDbType.VarChar, 8).Value = pedido.CLIENTE_OCASIONAL.C_POSTAL;
							cm.Parameters.Add("@DOMICILIO", SqlDbType.VarChar, 30).Value = pedido.CLIENTE_OCASIONAL.DOMICILIO;
							cm.Parameters.Add("@E_MAIL", SqlDbType.VarChar, 60).Value = pedido.CLIENTE_OCASIONAL.E_MAIL;
							cm.Parameters.Add("@IB_L", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.IB_L;
							cm.Parameters.Add("@IB_L3", SqlDbType.Bit).Value = pedido.CLIENTE_OCASIONAL.IB_L3;
							cm.Parameters.Add("@II_D", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.II_D;
							cm.Parameters.Add("@II_IB3", SqlDbType.Bit).Value = pedido.CLIENTE_OCASIONAL.II_IB3;
							cm.Parameters.Add("@II_L", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.II_L;
							cm.Parameters.Add("@IVA_D", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.IVA_D;
							cm.Parameters.Add("@IVA_L", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.IVA_L;
							cm.Parameters.Add("@LOCALIDAD", SqlDbType.VarChar, 20).Value = pedido.CLIENTE_OCASIONAL.LOCALIDAD;
							cm.Parameters.Add("@N_COMP", SqlDbType.VarChar, 14).Value = pedido.NRO_PEDIDO;
							cm.Parameters.Add("@N_CUIT", SqlDbType.VarChar, 20).Value = pedido.CLIENTE_OCASIONAL.N_CUIT;
							cm.Parameters.Add("@N_ING_BRUT", SqlDbType.VarChar, 20).Value = pedido.CLIENTE_OCASIONAL.N_ING_BRUT;
							cm.Parameters.Add("@N_IVA", SqlDbType.VarChar, 160).Value = pedido.CLIENTE_OCASIONAL.N_IVA;
							cm.Parameters.Add("@PORC_EXCL", SqlDbType.Decimal).Value = pedido.CLIENTE_OCASIONAL.PORC_EXCL;
							cm.Parameters.Add("@RAZON_SOCI", SqlDbType.VarChar, 60).Value = pedido.CLIENTE_OCASIONAL.RAZON_SOCI;
							cm.Parameters.Add("@SOBRE_II", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.SOBRE_II;
							cm.Parameters.Add("@SOBRE_IVA", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.SOBRE_IVA;
							cm.Parameters.Add("@TALONARIO", SqlDbType.Int).Value = pedido.TALON_PED;
							cm.Parameters.Add("@TELEFONO_1", SqlDbType.VarChar, 30).Value = pedido.CLIENTE_OCASIONAL.TELEFONO_1;
							cm.Parameters.Add("@TELEFONO_2", SqlDbType.VarChar, 30).Value = pedido.CLIENTE_OCASIONAL.TELEFONO_2;
							cm.Parameters.Add("@TIPO", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.TIPO;
							cm.Parameters.Add("@TIPO_DOC", SqlDbType.Int).Value = pedido.CLIENTE_OCASIONAL.TIPO_DOC;
							cm.Parameters.Add("@T_COMP", SqlDbType.VarChar, 3).Value = pedido.CLIENTE_OCASIONAL.T_COMP;
							cm.Parameters.Add("@DESTINO_DE", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.DESTINO_DE;
							cm.Parameters.Add("@CLA_IMP_CL", SqlDbType.VarChar, 6).Value = pedido.CLIENTE_OCASIONAL.CLA_IMP_CL;
							cm.Parameters.Add("@RECIBE_DE", SqlDbType.Bit).Value = pedido.CLIENTE_OCASIONAL.RECIBE_DE;
							cm.Parameters.Add("@AUT_DE", SqlDbType.Bit).Value = pedido.CLIENTE_OCASIONAL.AUT_DE;
							cm.Parameters.Add("@WEB", SqlDbType.VarChar, 60).Value = pedido.CLIENTE_OCASIONAL.WEB;
							cm.Parameters.Add("@COD_RUBRO", SqlDbType.VarChar, 4).Value = pedido.CLIENTE_OCASIONAL.COD_RUBRO;
							cm.Parameters.Add("@CTA_CLI", SqlDbType.Int).Value = pedido.CLIENTE_OCASIONAL.CTA_CLI;
							cm.Parameters.Add("@CTO_CLI", SqlDbType.VarChar, 10).Value = pedido.CLIENTE_OCASIONAL.CTO_CLI;
							cm.Parameters.Add("@IDENTIF_AFIP", SqlDbType.VarChar, 50).Value = pedido.CLIENTE_OCASIONAL.IDENTIF_AFIP;
							cm.Parameters.Add("@DIRECCION_ENTREGA", SqlDbType.VarChar, 200).Value = pedido.CLIENTE_OCASIONAL.DIRECCION_ENTREGA;
							cm.Parameters.Add("@CIUDAD_ENTREGA", SqlDbType.VarChar, 100).Value = pedido.CLIENTE_OCASIONAL.CIUDAD_ENTREGA;
							cm.Parameters.Add("@COD_PROVINCIA_ENTREGA", SqlDbType.VarChar, 2).Value = pedido.CLIENTE_OCASIONAL.COD_PROVINCIA_ENTREGA;
							cm.Parameters.Add("@LOCALIDAD_ENTREGA", SqlDbType.VarChar, 100).Value = pedido.CLIENTE_OCASIONAL.LOCALIDAD_ENTREGA;
							cm.Parameters.Add("@CODIGO_POSTAL_ENTREGA", SqlDbType.VarChar, 10).Value = pedido.CLIENTE_OCASIONAL.CODIGO_POSTAL_ENTREGA;
							cm.Parameters.Add("@TELEFONO1_ENTREGA", SqlDbType.VarChar, 100).Value = pedido.CLIENTE_OCASIONAL.TELEFONO1_ENTREGA;
							cm.Parameters.Add("@TELEFONO2_ENTREGA", SqlDbType.VarChar, 100).Value = pedido.CLIENTE_OCASIONAL.TELEFONO2_ENTREGA;
							cm.Parameters.Add("@ID_CATEGORIA_IVA", SqlDbType.Int).Value = pedido.CLIENTE_OCASIONAL.ID_CATEGORIA_IVA;
							cm.Parameters.Add("@CONSIDERA_IVA_BASE_CALCULO_IIBB", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.CONSIDERA_IVA_BASE_CALCULO_IIBB;
							cm.Parameters.Add("@CONSIDERA_IVA_BASE_CALCULO_IIBB_ADIC", SqlDbType.VarChar, 1).Value = pedido.CLIENTE_OCASIONAL.CONSIDERA_IVA_BASE_CALCULO_IIBB_ADIC;
							cm.Parameters.Add("@MAIL_DE", SqlDbType.VarChar, 255).Value = pedido.CLIENTE_OCASIONAL.MAIL_DE;
							cm.Parameters.Add("@FECHA_NACIMIENTO", SqlDbType.DateTime).Value = pedido.CLIENTE_OCASIONAL.FECHA_NACIMIENTO;
							cm.Parameters.Add("@SEXO", SqlDbType.VarChar).Value = DBNull.Value;

							cm.ExecuteNonQuery();
						}

						//GUARDAR PROXIMO NUMERO DE PEDIDO
						cm.CommandText =
							@"UPDATE GVA43 SET PROXIMO = dbo.Fn_encryptarproximonumero(@PROXIMO) WHERE TALONARIO = @TALONARIO";
						cm.Parameters.Clear();
						cm.Parameters.Add("@PROXIMO", SqlDbType.VarChar).Value = (Convert.ToInt32(proximo) + 1).ToString().PadLeft(8, '0');
						cm.Parameters.Add("@TALONARIO", SqlDbType.Int).Value = pedido.TALON_PED;
						cm.ExecuteNonQuery();

						guardo = true;
						tran.Commit();
						cn.Close();
					}
				}
			}
			catch { throw; }
			return guardo;
		}

		public void RecomponerSaldosStock()
		{
			SqlCommand cm = new SqlCommand();
			try
			{
				using (var cn = new SqlConnection(ConexionBase))
				{
					cn.Open();
					using (var tran = cn.BeginTransaction())
					{

						cm = new SqlCommand();
						cm.Connection = cn;
						cm.Transaction = tran;

						//SI COMPROMETE STOCK, EJECUTAR EL STORE DE TANGO
						cm.CommandText =
							@"EXEC sp_RecomposicionSaldosStock";
						cm.CommandTimeout = 0;
						cm.ExecuteNonQuery();

						tran.Commit();
						cn.Close();
					}
				}
			}
			catch { throw; }
		}

		public string TraerDatos(string sTabla, string sCampoAtraer, bool bTextoCampoAtraer, string sCampoABuscar, bool bTexto, string sValorAComparar)
		{
			SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("Conexion").ToString());

			string sSQL;
			SqlCommand cm = new SqlCommand();
			SqlDataReader dr;
			string sValor;


			try
			{
				cn.Open();
				if (bTexto == false)
				{
					sSQL = "SELECT " + sCampoAtraer + " FROM " + sTabla + " WHERE " + sCampoABuscar + "=" + sValorAComparar;
				}
				else
				{
					sSQL = "SELECT " + sCampoAtraer + " FROM " + sTabla + " WHERE " + sCampoABuscar + "='" + sValorAComparar + "'";
				}
				cm.Connection = cn;
				cm.CommandText = sSQL;
				dr = cm.ExecuteReader();



				if (dr.Read())
				{
					sValor = dr[0].ToString();
				}
				else if (bTextoCampoAtraer == true)
				{
					sValor = "";
				}
				else
				{
					sValor = "";
				}
			}
			catch
			{
				if (bTextoCampoAtraer == true)
					sValor = "";
				else
					sValor = "";
			}
			if (cn.State == ConnectionState.Open)
				cn.Close();
			return sValor;
		}
	}
}
