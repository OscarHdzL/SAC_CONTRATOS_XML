using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.GestionExpediente;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_siderbar
    {
        Response.ResponseGeneric<List<vs_siderbar>> Consultar(String rol);

    }
}
