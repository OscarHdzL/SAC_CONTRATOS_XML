using Modelos.Modelos.PreguntasFormulario;
using Modelos.Modelos.Verificacion;
using Modelos.Modelos.VerificacionContrato;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Interfaz
{
    public interface crud_verificacioncontrato
    {
        //Response.Response Guardar(T entidad);
        //    Response.Response Modificar(T entidad);
        //    Response.Response Eliminar(T entidad);
        Response.ResponseGeneric<tbl_verificacion_contrato> ConsultarVerficacionPregunta(String Contrato, String Pregunta);
        Response.ResponseGeneric<List<lista_verificados>> Consultar(string Dependencia, string Contrato);
        Response.ResponseGeneric<List<lista_verificados>> Consultar_SinContrato(string Dependencia);
    }
}
