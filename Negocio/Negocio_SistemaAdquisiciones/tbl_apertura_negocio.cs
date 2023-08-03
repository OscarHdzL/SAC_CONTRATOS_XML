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
    public class tbl_apertura_negocio : crud_apertura
    {
        private tbl_apertura_datos _apertura = new tbl_apertura_datos();
        
        public ResponseGeneric<List<Crudresponse>> Add(tbl_apertura _tbl_apertura)
        {
            try
            {
                if (_tbl_apertura.id == null || _tbl_apertura.id == Guid.Empty.ToString() || _tbl_apertura.id == "")
                {
                    _tbl_apertura.id = Guid.NewGuid().ToString();
                }
                return _apertura.Add(_tbl_apertura);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_Municipio(Guid id_estado)
        {
            try
            {
                return _apertura.Get_Municipio(id_estado);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }



        
        public ResponseGeneric<Crudresponse> DeclararDesierta(Guid solicitud)
        {
            try
            {
               
                return _apertura.DeclararDesierta(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }
    }
}