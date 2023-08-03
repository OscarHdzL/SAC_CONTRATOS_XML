using Modelos.Modelos;

using Modelos.Modelos.Contrato;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_plancomunicacion
    {        
        Response.ResponseGeneric<List<tbl_pc_mensaje>> Consultar_MensajesComunicacion_contrato(String contrato);
        Response.ResponseGeneric<List<tbl_pc_contratante>> Consultar_Contratante_contrato(String contrato);
        Response.ResponseGeneric<List<tbl_pc_proveedor>> Consultar_Proveedor_contrato(String contrato);
    }
}
