using AccesoDatos_AdminContratos.ModelosCFDI;
using Modelos.Modelos;
using Modelos.Modelos.VerificacionContrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoDatos_AdminContratos
{
    public class CFDI_ContratoDatos
    {
        public saciiContext contexto = new saciiContext();

        public Respuesta response = new Respuesta();


        public List<TblCfdiContratos> ListarCFDI() {

            try
            {
                var lista = contexto.TblCfdiContratos.ToList();
                return lista;
            }
            catch (Exception ex)
            {
                return new List<TblCfdiContratos>();
            }

            
        }


        public List<TblCfdiContratos> ListarCFDIByContrato(string idContrato)
        {

            try
            {
                var lista = contexto.TblCfdiContratos.Where(x=>x.TblContratoId == idContrato).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                return new List<TblCfdiContratos>();
            }


        }


        public List<TblCfdiContratos> ListarCFDIById(string id)
        {

            try
            {
                var lista = contexto.TblCfdiContratos.Where(x => x.Id == id).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                return new List<TblCfdiContratos>();
            }


        }



        public Respuesta InsertarCFDI(DatosXmlInput datos)
        {

            try
            {
                TblCfdiContratos obj = new TblCfdiContratos();

                obj.Id = Guid.NewGuid().ToString();
                obj.TblContratoId = datos.tbl_contrato_id;
                obj.Folio = datos.folio;
                obj.Serie = datos.serie;
                obj.Fecha = datos.fecha;
                obj.FormaDePago = datos.formaDePago;
                obj.Total = datos.total;
                obj.SubTotal = datos.subTotal;
                obj.Moneda = datos.moneda;
                obj.CondicionesDePago = datos.condicionesDePago;
                obj.MetodoDePago = datos.metodoDePago;
                obj.TipoDeComprobante = datos.tipoDeComprobante;
                obj.LugarExpedicion = datos.LugarExpedicion;
                obj.RfcEmisor = datos.rfcEmisor;
                obj.NombreEmisor = datos.nombreEmisor;
                obj.CalleEmisor = datos.calleEmisor;
                obj.NoInteriorEmisor = datos.noInteriorEmisor;
                obj.NoExteriorEmisor = datos.noExteriorEmisor;
                obj.ColoniaEmisor = datos.coloniaEmisor;
                obj.MunicipioEmisor = datos.municipioEmisor;
                obj.EstadoEmisor = datos.estadoEmisor;
                obj.PaisEmisor = datos.paisEmisor;
                obj.CodigoPostalEmisor = datos.codigoPostalEmisor;
                obj.RegimenEmisor = datos.RegimenEmisor;
                obj.RfcReceptor = datos.rfcReceptor;
                obj.NombreReceptor = datos.nombreReceptor;
                obj.CalleReceptor = datos.calleReceptor;
                obj.NoInteriorReceptor = datos.noInteriorReceptor;
                obj.NoExteriorReceptor = datos.noExteriorReceptor;
                obj.ColoniaReceptor = datos.coloniaReceptor;
                obj.MunicipioReceptor = datos.municipioReceptor;
                obj.EstadoReceptor = datos.estadoReceptor;
                obj.PaisReceptor = datos.paisReceptor;
                obj.CodigoPostalReceptor = datos.codigoPostalReceptor;
                obj.Conceptos = datos.conceptos_cadena;
                obj.Traslados = "";
                obj.Retenciones = "";
                obj.CadenaXml = datos.xml_cadena;
                //obj.Inclusion = new DateTime();
                obj.Activo = 1;


                var insercion = contexto.TblCfdiContratos.Add(obj);
                contexto.SaveChanges();
                    this.response.exito = true;
                    this.response.mensaje = "Se guardo correctamente";
                    this.response.respuesta = insercion.Entity.Id;
                }
            catch (Exception ex) {
                    this.response.exito = false;
                    this.response.mensaje = ex.InnerException.Message;
                    this.response.respuesta = null;
                }
          
            return this.response;
        }

        public Respuesta ActualizarCFDI(DatosXmlInput datos)
        {

            try
            {

                

                var registro = contexto.TblCfdiContratos.Where(x => x.Id == datos.id).FirstOrDefault();


                if (registro == null)
                {
                    throw new Exception("El id no existe");
                }


                TblCfdiContratos obj = new TblCfdiContratos();

                
                registro.TblContratoId = datos.tbl_contrato_id;
                registro.Folio = datos.folio;
                registro.Serie = datos.serie;
                registro.Fecha = datos.fecha;
                registro.FormaDePago = datos.formaDePago;
                registro.Total = datos.total;
                registro.SubTotal = datos.subTotal;
                registro.Moneda = datos.moneda;
                registro.CondicionesDePago = datos.condicionesDePago;
                registro.MetodoDePago = datos.metodoDePago;
                registro.TipoDeComprobante = datos.tipoDeComprobante;
                registro.LugarExpedicion = datos.LugarExpedicion;
                registro.RfcEmisor = datos.rfcEmisor;
                registro.NombreEmisor = datos.nombreEmisor;
                registro.CalleEmisor = datos.calleEmisor;
                registro.NoInteriorEmisor = datos.noInteriorEmisor;
                registro.NoExteriorEmisor = datos.noExteriorEmisor;
                registro.ColoniaEmisor = datos.coloniaEmisor;
                registro.MunicipioEmisor = datos.municipioEmisor;
                registro.EstadoEmisor = datos.estadoEmisor;
                registro.PaisEmisor = datos.paisEmisor;
                registro.CodigoPostalEmisor = datos.codigoPostalEmisor;
                registro.RegimenEmisor = datos.RegimenEmisor;
                registro.RfcReceptor = datos.rfcReceptor;
                registro.NombreReceptor = datos.nombreReceptor;
                registro.CalleReceptor = datos.calleReceptor;
                registro.NoInteriorReceptor = datos.noInteriorReceptor;
                registro.NoExteriorReceptor = datos.noExteriorReceptor;
                registro.ColoniaReceptor = datos.coloniaReceptor;
                registro.MunicipioReceptor = datos.municipioReceptor;
                registro.EstadoReceptor = datos.estadoReceptor;
                registro.PaisReceptor = datos.paisReceptor;
                registro.CodigoPostalReceptor = datos.codigoPostalReceptor;
                registro.Conceptos = datos.conceptos_cadena;
                registro.Traslados = "";
                registro.Retenciones = "";
                registro.CadenaXml = datos.xml_cadena;

                var insercion = contexto.TblCfdiContratos.Update(registro);
                contexto.SaveChanges();
                this.response.exito = true;
                this.response.mensaje = "Se actualizo correctamente";
                this.response.respuesta = insercion.Entity.Id;
            }
            catch (Exception ex)
            {
                this.response.exito = false;
                this.response.mensaje = ex.InnerException.Message;
                this.response.respuesta = null;
            }

            return this.response;
        }




    }
}
