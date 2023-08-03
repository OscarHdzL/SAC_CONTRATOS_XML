using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.GestionExpediente;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_expedientecontrato
    {
        Response.ResponseGeneric<List<Crudresponse>> add(tbl_gestion_expediente_contrato_add expediente);

    }
}
