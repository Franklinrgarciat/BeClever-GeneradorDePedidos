using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaDatos;
using System.Windows.Forms;
using System.Configuration;

namespace CapaNegocio
{
    public class NegPedido
    {
        public List<ArchivoExcel> ArchivoExcelDatos(string ruta)
        {
            try
            {
                List<ArchivoExcel> DatosExcel = new List<ArchivoExcel>();
                using(var reader = new StreamReader(ruta))
                {
                    bool SaltarPrimeraFila = true;
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        if (SaltarPrimeraFila)
                        {
                            SaltarPrimeraFila = false;
                        }
                        else
                        {
                            ArchivoExcel daExcel = new ArchivoExcel();
                            daExcel.IdentificacionBeClever = linea[0].ToString();
                            daExcel.NumeroDeLote = linea[1].ToString();
                            daExcel.NumeroDeOperacion = linea[2].ToString();
                            daExcel.FechaEmision = Convert.ToDateTime(linea[3].ToString()).Date;
                            daExcel.Dni = linea[4].ToString();
                            daExcel.Cuit = linea[5].ToString();
                            daExcel.Nombre = linea[6].ToString();
                            daExcel.CategoriaIva = linea[7].ToString();
                            daExcel.Provincia = linea[8].ToString();
                            daExcel.Localidad = linea[9].ToString();
                            daExcel.CP = linea[10].ToString();
                            daExcel.Domicilio = linea[11].ToString();
                            daExcel.NumeroCuota = Convert.ToInt32(linea[12].ToString());
                            daExcel.Cuotas = Convert.ToInt32(linea[13].ToString());
                            daExcel.Moneda = linea[14].ToString();
                            daExcel.ConceptoTablaEquivalencia = linea[15].ToString();
                            daExcel.Descripcion = linea[16].ToString();
                            daExcel.ImporteSinIva = Convert.ToDouble(linea[17].ToString());
                            daExcel.Iva = Convert.ToDouble(linea[18].ToString());
                            daExcel.ImporteConIva = Convert.ToDouble(linea[19].ToString());
                            daExcel.Bonificacion = linea[20].ToString();

                            DatosExcel.Add(daExcel);
                        }
                    }
                }
                return DatosExcel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cierre el archivo excel antes de exportar", Convert.ToString(MessageBoxIcon.Error));
                throw ex;
            }
        }
        public List<string> InsertarDatosDelExcel(List<ArchivoExcel> listaExcel)
        {
            List<string> ListaErrores = new List<string>();
            try
            {
                CapaDatos.MovimientoDA movimientoDA = new MovimientoDA();
                List<string> CompNoPasados = new List<string>();
                foreach (var DatosLista in listaExcel)
                {
                    try
                    {
                        var identificadorBeCleverTango = movimientoDA.TraerDatos("GVA21", "LEYENDA_1", true, "LEYENDA_1", true, DatosLista.IdentificacionBeClever);
                        if(identificadorBeCleverTango != DatosLista.IdentificacionBeClever)
                        {
                            var id_asiento_modelo_gv = movimientoDA.TraerDatos("ASIENTO_MODELO_GV", "ID_ASIENTO_MODELO_GV", true, "DESC_ASIENTO_MODELO_GV", true, DatosLista.NumeroDeLote);
                            var cod_asiento_modelo_gv = movimientoDA.TraerDatos("ASIENTO_MODELO_GV", "COD_ASIENTO_MODELO_GV", true, "DESC_ASIENTO_MODELO_GV", true, DatosLista.NumeroDeLote);
                            // SI id_modelo_asiento_gv es = 0, LANZAMOS LA EXCEPCION CON EL NUMERO DE LOTE.
                            if (id_asiento_modelo_gv.Equals(0))
                            {
                                throw new Exception("El asiento para el número de lote:" + DatosLista.NumeroDeLote + "no esta creado en tango");
                            }

                            var COD_ARTICU = DatosLista.ConceptoTablaEquivalencia;
                            // SI NO EXISTE LANZAMOS EXCEPCION
                            if (movimientoDA.TraerDatos("STA11", "COD_ARTICU", true, "COD_ARTICU", true, COD_ARTICU) == "")
                            {
                                throw new Exception("No se agrego el código de articulo: " + DatosLista.ConceptoTablaEquivalencia + "porque no se encuentra registrado en tango");
                            }

                            //--------------- INSERTO DATOS DE CABECERA DE PEDIDOS DE TABLA GVA21--------------------

                            CabeceraPedido cabecera = new CabeceraPedido();

                            cabecera.FILLER = "";
                            cabecera.APRUEBA = "SUPERVISOR";
                            cabecera.CIRCUITO = 1;
                            cabecera.COD_CLIENT = "000000";
                            cabecera.COD_SUCURS = "01";
                            cabecera.COD_TRANSP = movimientoDA.TraerDatos("GVA14", "COD_TRANSP", true, "COD_CLIENT", true, cabecera.COD_CLIENT);
                            cabecera.COD_VENDED = movimientoDA.TraerDatos("GVA14", "COD_VENDED", true, "COD_CLIENT", true, cabecera.COD_CLIENT);
                            cabecera.COMENTARIO = "";
                            cabecera.COMP_STK = false;
                            cabecera.COND_VTA = 0;
                            cabecera.COTIZ = 1;
                            cabecera.ESTADO = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ESTADO").ToString());
                            cabecera.EXPORTADO = false;
                            cabecera.FECHA_APRU = DateTime.ParseExact("18000101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;
                            cabecera.FECHA_ENTR = DateTime.ParseExact("18000101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;
                            cabecera.FECHA_PEDI = DatosLista.FechaEmision;
                            cabecera.HORA_APRUE = "0";
                            cabecera.ID_EXTERNO = "";
                            cabecera.LEYENDA_1 = DatosLista.IdentificacionBeClever;
                            cabecera.LEYENDA_2 = DatosLista.NumeroDeLote;
                            cabecera.LEYENDA_3 = DatosLista.NumeroDeOperacion;
                            cabecera.LEYENDA_4 = Convert.ToString(DatosLista.NumeroCuota);
                            cabecera.LEYENDA_5 = Convert.ToString(DatosLista.Cuotas);
                            if (DatosLista.Moneda.Equals("ARS"))
                            {
                                cabecera.MON_CTE = true;
                            }
                            else
                            {
                                cabecera.MON_CTE = false;
                            }
                            cabecera.N_LISTA = 1;
                            cabecera.N_REMITO = "";
                            cabecera.NRO_O_COMP = "";
                            cabecera.NRO_PEDIDO = "";
                            cabecera.NRO_SUCURS = 0;
                            cabecera.ORIGEN = "";
                            cabecera.PORC_DESC = Convert.ToDecimal(DatosLista.Bonificacion);
                            if (cabecera.ESTADO.Equals(1))
                            {
                                cabecera.REVISO_FAC = "I";
                                cabecera.REVISO_PRE = "I";
                                cabecera.REVISO_STK = "I";
                            }
                            else
                            {
                                cabecera.REVISO_FAC = "A";
                                cabecera.REVISO_PRE = "A";
                                cabecera.REVISO_STK = "A";
                            }
                            cabecera.TALONARIO = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TALONARIO").ToString());
                            cabecera.TALON_PED = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TALON_PED").ToString());
                            cabecera.TOTAL_PEDI = Convert.ToDecimal(DatosLista.ImporteSinIva);
                            cabecera.TIPO_ASIEN = cod_asiento_modelo_gv;
                            cabecera.MOTIVO = "";
                            cabecera.HORA = "";
                            cabecera.COD_CLASIF = "";
                            cabecera.ID_ASIENTO_MODELO_GV = Convert.ToInt32(id_asiento_modelo_gv);
                            cabecera.TAL_PE_ORI = 0;
                            cabecera.NRO_PE_ORI = "";
                            cabecera.FECHA_INGRESO = DatosLista.FechaEmision;
                            cabecera.HORA_INGRESO = "";
                            cabecera.USUARIO_INGRESO = "Seincomp-Systems";
                            cabecera.TERMINAL_INGRESO = "";
                            cabecera.FECHA_ULTIMA_MODIFICACION = DateTime.ParseExact("18000101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;
                            cabecera.HORA_ULTIMA_MODIFICACION = "";
                            cabecera.USUA_ULTIMA_MODIFICACION = "";
                            cabecera.TERM_ULTIMA_MODIFICACION = "";
                            cabecera.ID_DIRECCION_ENTREGA = 0;
                            cabecera.ES_PEDIDO_WEB = false;
                            cabecera.FECHA_O_COMP = DateTime.ParseExact("18000101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;
                            cabecera.ACTIVIDAD_COMPROBANTE_AFIP = "";
                            cabecera.ID_ACTIVIDAD_EMPRESA_AFIP = null;
                            cabecera.TIPO_DOCUMENTO_PAGADOR = 0;
                            cabecera.NUMERO_DOCUMENTO_PAGADOR = null;
                            cabecera.WEB_ORDER_ID = 0;
                            cabecera.USUARIO_TIENDA = null;
                            cabecera.TIENDA = "";
                            cabecera.ORDER_ID_TIENDA = "";
                            cabecera.NRO_OC_COMP = "";
                            cabecera.TOTAL_DESC_TIENDA = 0;
                            cabecera.TIENDA_QUE_VENDE = "";
                            cabecera.PORCEN_DESC_TIENDA = 0;
                            cabecera.USUARIO_TIENDA_VENDEDOR = "";
                            cabecera.ID_NEXO_PEDIDOS_ORDEN = null;


                            //---------- INSERTO DETALLES DE PEDIDO EN TABLA GVA03------------------------------

                            DetallePedido detallePedido = new DetallePedido();
                            detallePedido.FILLER = "";
                            detallePedido.CAN_EQUI_V = 1;
                            detallePedido.CANT_A_DES = 1;
                            detallePedido.CANT_A_FAC = 1;
                            detallePedido.CANT_PEDID = 1;
                            detallePedido.CANT_PEN_D = 1;
                            detallePedido.CANT_PEN_F = 1;
                            detallePedido.COD_ARTICU = COD_ARTICU;
                            detallePedido.DESCUENTO = 0;
                            detallePedido.N_RENGLON = 1;
                            detallePedido.NRO_PEDIDO = DatosLista.NumeroDeOperacion;
                            detallePedido.PEN_REM_FC = 1;
                            detallePedido.PEN_FAC_RE = 1;
                            detallePedido.PRECIO = Convert.ToDecimal(DatosLista.ImporteSinIva);
                            detallePedido.TALON_PED = cabecera.TALON_PED;
                            detallePedido.COD_CLASIF = "";
                            detallePedido.CANT_A_DES_2 = 0;
                            detallePedido.CANT_A_FAC_2 = 0;
                            detallePedido.CANT_PEDID_2 = 0;
                            detallePedido.CANT_PEN_D_2 = 0;
                            detallePedido.CANT_PEN_F_2 = 0;
                            detallePedido.PEN_REM_FC_2 = 0;
                            detallePedido.PEN_FAC_RE_2 = 0;
                            detallePedido.ID_MEDIDA_VENTAS = Convert.ToInt32(movimientoDA.TraerDatos("STA11", "ID_MEDIDA_VENTAS", true, "COD_ARTICU", true, COD_ARTICU));
                            detallePedido.ID_MEDIDA_STOCK_2 = Convert.ToInt32(movimientoDA.TraerDatos("STA11", "ID_MEDIDA_STOCK_2", true, "COD_ARTICU", true, COD_ARTICU));
                            detallePedido.ID_MEDIDA_STOCK = null; Convert.ToInt32(movimientoDA.TraerDatos("STA11", "ID_MEDIDA_STOCK", true, "COD_ARTICU", true, COD_ARTICU));
                            detallePedido.UNIDAD_MEDIDA_SELECCIONADA = "P";
                            detallePedido.COD_ARTICU_KIT = null;
                            detallePedido.RENGL_PADR = 0;
                            detallePedido.PROMOCION = false;
                            detallePedido.PRECIO_ADICIONAL_KIT = 0;
                            detallePedido.KIT_COMPLETO = false;
                            detallePedido.INSUMO_KIT_SEPARADO = false;
                            detallePedido.PRECIO_LISTA = 0;
                            detallePedido.PRECIO_BONIF = 0;
                            detallePedido.DESCUENTO_PARAM = 0;
                            detallePedido.PRECIO_FECHA = DateTime.ParseExact("18000101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;
                            detallePedido.FECHA_MODIFICACION_PRECIO = DateTime.ParseExact("18000101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;
                            detallePedido.USUARIO_MODIFICACION_PRECIO = "";
                            detallePedido.TERMINAL_MODIFICACION_PRECIO = "";
                            detallePedido.ID_NEXO_PEDIDOS_RENGLON_ORDEN = 0;

                            cabecera.ListDetalle.Add(detallePedido);

                            //------------ INSERTAMOS DATOS DE CLIENTE OCASIONAL-----------------------------------

                            ClienteOcasional ocasional = new ClienteOcasional();
                            ocasional.FILLER = "";
                            ocasional.ALI_ADI_IB = 0;
                            ocasional.ALI_FIJ_IB = 0;
                            ocasional.ALI_NOCATE = 0;
                            ocasional.AL_FIJ_IB3 = 0;
                            ocasional.COD_PROVIN = DatosLista.Provincia;
                            ocasional.C_POSTAL = DatosLista.CP;
                            ocasional.DOMICILIO = DatosLista.Domicilio;
                            ocasional.E_MAIL = "";
                            ocasional.IB_L = "N";
                            ocasional.IB_L3 = false;
                            ocasional.II_D = "N";
                            ocasional.II_IB3 = false;
                            ocasional.II_L = "S";
                            switch (DatosLista.CategoriaIva)
                            {
                                case "CF":
                                    ocasional.IVA_D = "N";
                                    ocasional.IVA_L = "S";
                                    break;
                                case "EX":
                                    ocasional.IVA_D = "N";
                                    ocasional.IVA_L = "E";
                                    break;
                                case "EXE":
                                    ocasional.IVA_D = "N";
                                    ocasional.IVA_L = "N";
                                    break;
                                case "RI":
                                    ocasional.IVA_D = "S";
                                    ocasional.IVA_L = "S";
                                    break;
                                case "RS":
                                    ocasional.IVA_D = "S";
                                    ocasional.IVA_L = "M";
                                    break;
                                default:
                                    ocasional.IVA_D = null;
                                    ocasional.IVA_L = null;
                                    break;
                            }
                            ocasional.LOCALIDAD = DatosLista.Localidad;
                            ocasional.N_COMP = "";
                            ocasional.N_CUIT = DatosLista.Cuit;
                            ocasional.N_ING_BRUT = "";
                            ocasional.N_IVA = "";
                            ocasional.PORC_EXCL = 0;
                            ocasional.RAZON_SOCI = DatosLista.Nombre;
                            ocasional.SOBRE_II = "N";
                            ocasional.SOBRE_IVA = "N";
                            ocasional.TALONARIO = cabecera.TALON_PED;
                            ocasional.TELEFONO_1 = "";
                            ocasional.TELEFONO_2 = "";
                            ocasional.TIPO = "";
                            if (DatosLista.Cuit.Length <= 8)
                            {
                                ocasional.TIPO_DOC = 96;
                            }
                            else
                            {
                                ocasional.TIPO_DOC = 80;
                            }
                            ocasional.T_COMP = "PED";
                            ocasional.DESTINO_DE = "";
                            ocasional.CLA_IMP_CL = "";
                            ocasional.RECIBE_DE = false;
                            ocasional.AUT_DE = false;
                            ocasional.WEB = null;
                            ocasional.COD_RUBRO = "";
                            ocasional.CTA_CLI = 0;
                            ocasional.CTO_CLI = "";
                            ocasional.IDENTIF_AFIP = "";
                            ocasional.DIRECCION_ENTREGA = DatosLista.Domicilio;
                            ocasional.CIUDAD_ENTREGA = DatosLista.Localidad;
                            ocasional.COD_PROVINCIA_ENTREGA = "";
                            ocasional.LOCALIDAD_ENTREGA = "";
                            ocasional.CODIGO_POSTAL_ENTREGA = "";
                            ocasional.TELEFONO1_ENTREGA = "";
                            ocasional.TELEFONO2_ENTREGA = "";
                            ocasional.ID_CATEGORIA_IVA = Convert.ToInt32(DatosLista.CategoriaIva);
                            ocasional.CONSIDERA_IVA_BASE_CALCULO_IIBB = "N";
                            ocasional.CONSIDERA_IVA_BASE_CALCULO_IIBB_ADIC = "N";
                            ocasional.MAIL_DE = "";
                            ocasional.FECHA_NACIMIENTO = DateTime.ParseExact("18000101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;
                            ocasional.SEXO = "";

                            cabecera.ClienteOcasional.Add(ocasional);

                            MovimientoDA movimientoPedido = new MovimientoDA();
                            movimientoPedido.InsertarPedido(cabecera);

                        }

                    }
                    catch (Exception ex)
                    {
                        ListaErrores.Add(ex.Message);
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaErrores;
        }
    }
}
