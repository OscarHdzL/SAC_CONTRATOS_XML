using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using
System.Text;

namespace Negocio_SistemaAdquisiciones
{
    public class tbl_partida_area_negocio
    {
        private tbl_partida_area_acceso_datos _partida_area = new tbl_partida_area_acceso_datos();
        //private tbl_tiposcontrato_acceso_datos _TiposContratos = new tbl_tiposcontrato_acceso_datos();

        public ResponseGeneric<List<DropDownList>> FillDrop(String area, String ejercicio)
        {
            try
            {
                return _partida_area.FillDrop(area, ejercicio);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<monto_seleccionado_area_partida> MontoSeleccionado_area_partida(String area, String partida)
        {
            try
            {
                return _partida_area.MontoSeleccionado_area_partida(area, partida);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<monto_seleccionado_area_partida>(ex);
            }
        }

    }
}