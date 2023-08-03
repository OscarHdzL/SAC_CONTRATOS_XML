using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class sp_plan_entrega_negocio
    {
        private sp_plan_entrega_acceso_datos NegocionPlanEntrega = new sp_plan_entrega_acceso_datos();

        static readonly String PasswordHash = "@PMS|FM0";
        static readonly String SaltKey = "S@LT&KEY";
        static readonly String VIKey = "@6D65GKJFZA4GSSD";

        private readonly ILoggerManager _logger;

        public sp_plan_entrega_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<Crudresponse>> addheader(EstructuraPalnEntrega EstructuraPalnEntrega_, int accion)
        {
            try
            {
                EstructuraPalnEntrega_.Header.p_id = Guid.NewGuid();
                EstructuraPalnEntrega_.Header.p_opt = accion;
                ResponseGeneric<List<CrudresponseIdentificador>> Step1 = NegocionPlanEntrega.sp_plan_entrega_header(EstructuraPalnEntrega_.Header);
                if (Step1.Response[0].id == Guid.Empty)
                {
                    return new ResponseGeneric<List<Crudresponse>>("No se completo Step 1");
                }
                Guid idPlanEntrega = Step1.Response[0].id;
                foreach (UbicacionProductos ubicacionProductos in EstructuraPalnEntrega_.UbicacionesProductos)
                {
                    ResponseGeneric<List<CrudresponseIdentificador>> Step2 = NegocionPlanEntrega.sp_plan_entrega_Ubicaciones(
                        idPlanEntrega,
                        ubicacionProductos.tbl_ubicacion_id.ToString(),
                        accion);
                    foreach (sp_plan_entrega_producto _Plan_Entrega_Producto in ubicacionProductos.productos)
                    {
                        _Plan_Entrega_Producto.p_id = Guid.NewGuid();
                        _Plan_Entrega_Producto.p_tbl_ubicacion_plan_entrega_id = Step2.Response[0].id;
                        _Plan_Entrega_Producto.p_opt = accion;
                        ResponseGeneric<List<Crudresponse>> Step3 = NegocionPlanEntrega.sp_plan_entrega_Producto(
                            _Plan_Entrega_Producto
                        );
                        ResponseGeneric<List<Crudresponse>> Step4 = NegocionPlanEntrega.str_plan_entrega_ejecutor(
                            accion,
                            _Plan_Entrega_Producto.p_tbl_ubicacion_plan_entrega_id,
                            ubicacionProductos.EjecutorPorUbicacion
                        );
                    }
                }
                Crudresponse Response_ = new Crudresponse();
                Response_.cod = "success";
                Response_.msg = "Se agrego un nuevo registro";
                List<Crudresponse> crud = new List<Crudresponse>();
                crud.Add(Response_);
                return new ResponseGeneric<List<Crudresponse>>(crud);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> modifyHeader(EstructuraPalnEntrega EstructuraPalnEntrega_, int accion)
        {
            try
            {
                EstructuraPalnEntrega_.Header.p_opt = accion;
                ResponseGeneric<List<CrudresponseIdentificador>> Step1 = NegocionPlanEntrega.sp_plan_entrega_header(EstructuraPalnEntrega_.Header);

                if (Step1.Response.FirstOrDefault().cod != "success")
                    return new ResponseGeneric<List<Crudresponse>>("No se completo Step 1");

                Guid idPlanEntrega = EstructuraPalnEntrega_.Header.p_id;

                foreach (UbicacionProductos ubicacionProductos in EstructuraPalnEntrega_.UbicacionesProductos)
                {
                    int accionDelete = 4;
                    int accionInsert = 2;
                    
                    NegocionPlanEntrega.sp_plan_entrega_Ubicaciones(
                        idPlanEntrega,
                        ubicacionProductos.tbl_ubicacion_id.ToString(),
                        accionDelete);

                    ResponseGeneric<List<CrudresponseIdentificador>> Step2 = NegocionPlanEntrega.sp_plan_entrega_Ubicaciones(
                        idPlanEntrega,
                        ubicacionProductos.tbl_ubicacion_id.ToString(),
                        accionInsert);

                    foreach (sp_plan_entrega_producto _Plan_Entrega_Producto in ubicacionProductos.productos)
                    {
                        //Insert
                        _Plan_Entrega_Producto.p_id = Guid.NewGuid();
                        _Plan_Entrega_Producto.p_tbl_ubicacion_plan_entrega_id = Step2.Response.FirstOrDefault().id;
                        _Plan_Entrega_Producto.p_opt = accionInsert;

                        ResponseGeneric<List<Crudresponse>> Step3 = NegocionPlanEntrega.sp_plan_entrega_Producto(_Plan_Entrega_Producto);

                        ResponseGeneric<List<Crudresponse>> Step4 = NegocionPlanEntrega.str_plan_entrega_ejecutor(
                            accionInsert,
                            _Plan_Entrega_Producto.p_tbl_ubicacion_plan_entrega_id,
                            ubicacionProductos.EjecutorPorUbicacion
                        );
                    }
                }

                Crudresponse Response_ = new Crudresponse();
                Response_.cod = "success";
                Response_.msg = "Se agrego un nuevo registro";
                List<Crudresponse> crud = new List<Crudresponse>();
                crud.Add(Response_);
                return new ResponseGeneric<List<Crudresponse>>(crud);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> get_plan_entrega_select(Guid id, String opcion)
        {
           return   NegocionPlanEntrega.get_plan_entrega_select(id, "Confirmados");
        }
        public ResponseGeneric<List<EstructuraPalnEntregaCumplio>> get_plan_entrega(Guid id, String opcion)
        {
            ResponseGeneric<List<vs_plan_entrega>> vs_plan_entrega_get = NegocionPlanEntrega.get_plan_entrega(id,opcion);
            ResponseGeneric<List<vs_plan_entrega>> vs_plan_entrega_get_agrupado = NegocionPlanEntrega.get_plan_entrega(id, "contratoagrupado");

            List<vs_plan_entrega> Lista = vs_plan_entrega_get.Response;
            List<EstructuraPalnEntregaCumplio> ListestructuraPlanEntrega = new List<EstructuraPalnEntregaCumplio>();
            //Lista de encabezados
            List<sp_plan_entrega_input> header_ = new List<sp_plan_entrega_input>();

            header_ = (
                           vs_plan_entrega_get_agrupado.Response.Where(x=> x.tbl_contrato_id == id).Select(x =>
                            new sp_plan_entrega_input
                            {
                                p_opt = 0,
                                p_id = x.tbl_plan_entrega_id,
                                p_tbl_contrato_servidor_resp_id = x.tbl_contrato_servidor_resp_id,
                                p_identificador = x.identificador,
                                p_periodo = x.periodo,
                                p_descripcion = x.descripcion,
                                p_fecha_ejecucion = x.fecha_ejecucion,
                                p_activo = "1",
                                p_tipo_entrega = x.tipo_entrega
                            }
                           ).ToList<sp_plan_entrega_input>()

                     ); 

            //Recorremos encabezados para buscar ubicaciones
            header_ = header_.Distinct().ToList();
            foreach (sp_plan_entrega_input item in header_)
            {
                List<UbicacionProductos> Ubicaciones = new List<UbicacionProductos>();
                
                List<Guid> Ubicaciones_list = Lista.Where(x => x.tbl_plan_entrega_id == item.p_id)
                    .Select(x => x.tbl_ubicacion_id).Distinct().ToList();
                //Recorremos ubicaciones y buscamos productos
                foreach (Guid idUbicacion_ in Ubicaciones_list)
                {
                    UbicacionProductos Ubicacion_item = new UbicacionProductos();
                    Ubicacion_item.tbl_ubicacion_id = idUbicacion_;
                    Ubicacion_item.EjecutorPorUbicacion = (
                            from Ejecutor in Lista
                            where Ejecutor.tbl_plan_entrega_id == item.p_id && Ejecutor.tbl_ubicacion_id == idUbicacion_
                            select Ejecutor.tbl_plan_entrega_ejecutor_id
                        ).FirstOrDefault();
                    Ubicacion_item.Ejecutor_nombre = (
                            from Ejecutor in Lista
                            where Ejecutor.tbl_plan_entrega_id == item.p_id && Ejecutor.tbl_ubicacion_id == idUbicacion_
                            select Ejecutor.Ejecutor_nombre
                        ).FirstOrDefault();
                    Ubicacion_item.productos = (
                            from Producto_ in Lista
                            where Producto_.tbl_plan_entrega_id == item.p_id && Producto_.tbl_ubicacion_id == idUbicacion_
                            select new sp_plan_entrega_producto
                            {
                                p_opt = 0,
                                p_id = Producto_.tbl_plan_entrega_producto_id,
                                p_tbl_contrato_producto_id = Producto_.tbl_contrato_producto_id,
                                p_tbl_ubicacion_plan_entrega_id = Producto_.tbl_ubicacion_plan_entrega_id,
                                p_estatus = "1",
                                p_cantidad = Producto_.cantidad,
                                p_detalle_actividad = Producto_.detalle_actividad,
                                p_tipo = Producto_.tipo,
                                cumplio = Producto_.cumplio,
                                p_monto = Producto_.monto!=null? Producto_.monto.ToString():"",
                                p_monto_iva = Producto_.monto_iva!=null? Producto_.monto_iva.ToString():"",
                                p_total = Producto_.total!=null? Producto_.total.ToString():""
                            }
                        ).Distinct().ToList();
                    Ubicaciones.Add(Ubicacion_item);
                }
                EstructuraPalnEntregaCumplio tot_tmp = new EstructuraPalnEntregaCumplio();
                tot_tmp.Header = new sp_plan_entrega_input();
                tot_tmp.UbicacionesProductos = new List<UbicacionProductos>();
                //vaciar header
                tot_tmp.Header = item;
                tot_tmp.UbicacionesProductos = Ubicaciones;
            
                tot_tmp.cumplio = NegocionPlanEntrega.sp_get_obligaciones_incumplidas(vs_plan_entrega_get.Response[0].tbl_plan_entrega_id).Response[0].conteo;
                ListestructuraPlanEntrega.Add(tot_tmp);

            }
            return new ResponseGeneric<List<EstructuraPalnEntregaCumplio>>(ListestructuraPlanEntrega);
        }
        public ResponseGeneric<List<plan_entrega_detalle_producto_cuerpo>> get_plan_entrega_detalle_producto(Guid id) 
        {
            try
            {
                var response = NegocionPlanEntrega.get_plan_entrega_detalle_productos(id);
                plan_entrega_detalle_producto_cuerpo plan_Entrega_Detalle = new plan_entrega_detalle_producto_cuerpo();
                if (response.Response.Count > 0) {
                    var listaDetalle = response.Response;
                    plan_Entrega_Detalle.header = new plan_entrega_detalle_producto_header();

                    plan_Entrega_Detalle.header.tbl_plan_entrega_id = listaDetalle[0].tbl_plan_entrega_id;
                    plan_Entrega_Detalle.header.tbl_contrato_servidor_resp_id = listaDetalle[0].tbl_contrato_servidor_resp_id;
                    plan_Entrega_Detalle.header.Responsable_PE = listaDetalle[0].Responsable_PE;
                    plan_Entrega_Detalle.header.identificador = listaDetalle[0].identificador;
                    plan_Entrega_Detalle.header.periodo = listaDetalle[0].periodo;
                    plan_Entrega_Detalle.header.descripcion = listaDetalle[0].descripcion;
                    plan_Entrega_Detalle.header.fecha_ejecucion = listaDetalle[0].fecha_ejecucion;
                    plan_Entrega_Detalle.header.tbl_plan_entrega_inclusion = listaDetalle[0].tbl_plan_entrega_inclusion;
                    plan_Entrega_Detalle.header.tbl_plan_entrega_activo = listaDetalle[0].tbl_plan_entrega_activo;
                    plan_Entrega_Detalle.header.tipo_entrega = listaDetalle[0].tipo_entrega;

                    plan_Entrega_Detalle.ubicaciones = new List<plan_entrega_detalle_producto_ubicacion>();
                    var ubicaciones = listaDetalle.GroupBy(g => g.tbl_ubicacion_id).Select(g => g.First()).ToList();
                    foreach (var ub in ubicaciones) {
                        plan_entrega_detalle_producto_ubicacion ubicacion = new plan_entrega_detalle_producto_ubicacion()
                        {
                            tbl_ubicacion_id = ub.tbl_ubicacion_id,
                            Ejecutor_nombre = ub.Ejecutor_nombre,
                            clave_ubicacion = ub.clave_ubicacion,
                            unidad_ubicacion = ub.unidad_ubicacion,
                            direccion_ubicacion = ub.direccion_ubicacion,
                            tbl_plan_entrega_ejecutor_id = ub.tbl_plan_entrega_ejecutor_id,
                            listado_productos = new List<plan_entrega_detalle_producto>()
                        };
                        foreach (var producto in listaDetalle) {
                            if (ubicacion.tbl_ubicacion_id == producto.tbl_ubicacion_id&&producto.tbl_plan_entrega_producto_id.ToString()!= "00000000-0000-0000-0000-000000000000") {
                                plan_entrega_detalle_producto prod = new plan_entrega_detalle_producto()
                                {
                                  tbl_plan_entrega_producto_id = producto.tbl_plan_entrega_producto_id,
                                  tbl_contrato_producto_id = producto.tbl_contrato_producto_id,
                                  tbl_ubicacion_plan_entrega_id = producto.tbl_ubicacion_plan_entrega_id,
                                  tbl_plan_entrega_producto_estatus = producto.tbl_plan_entrega_producto_estatus,
                                  tbl_plan_entrega_producto_inclusion = producto.tbl_plan_entrega_producto_inclusion,
                                  cantidad = producto.cantidad,
                                  detalle_actividad = producto.detalle_actividad,
                                  tipo = producto.tipo,
                                  cumplio = producto.cumplio,
                                  tbl_contrato_id = producto.tbl_contrato_id,
                                  monto = producto.monto,
                                  monto_iva = producto.monto_iva,
                                  total = producto.total,
                                  tbl_producto_servicio_id = producto.tbl_producto_servicio_id,
                                  clave_producto = producto.clave_producto,
                                  elemento = producto.elemento
                                };
                                ubicacion.listado_productos.Add(prod);
                            }
                        }
                        plan_Entrega_Detalle.ubicaciones.Add(ubicacion);
                    }
                    Console.WriteLine();
                }
                List<plan_entrega_detalle_producto_cuerpo> listado = new List<plan_entrega_detalle_producto_cuerpo>();
                listado.Add(plan_Entrega_Detalle);
                return new ResponseGeneric<List<plan_entrega_detalle_producto_cuerpo>>(listado);
            }
            catch (Exception ex) {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<plan_entrega_detalle_producto_cuerpo>>(ex);
            }
        }


        /***********************************************************************************************/
        public ResponseGeneric<List<EstructuraPalnEntregaCumplio_ejec>> get_plan_entrega_lista(Guid id, String opcion, string usuario)
        {
            try {

                ResponseGeneric<List<vs_plan_entrega_ejec>> vs_plan_entrega_get = NegocionPlanEntrega.get_plan_entrega_ejec(id, opcion, usuario);
                ResponseGeneric<List<vs_plan_entrega_ejec>> vs_plan_entrega_get_agrupado = NegocionPlanEntrega.get_plan_entrega_ejec(id, "contratoagrupado", usuario);

                List<vs_plan_entrega_ejec> Lista = vs_plan_entrega_get.Response;
                List<EstructuraPalnEntregaCumplio_ejec> ListestructuraPlanEntrega = new List<EstructuraPalnEntregaCumplio_ejec>();
                //Lista de encabezados
                List<sp_plan_entrega_input_ejec> header_ = new List<sp_plan_entrega_input_ejec>();

                header_ = (
                               vs_plan_entrega_get_agrupado.Response.Where(x => x.tbl_contrato_id == id).Select(x =>
                                 new sp_plan_entrega_input_ejec
                                 {
                                     p_opt = 0,
                                     p_id = x.tbl_plan_entrega_id,
                                     p_tbl_contrato_servidor_resp_id = x.tbl_contrato_servidor_resp_id,
                                     p_identificador = x.identificador,
                                     p_periodo = x.periodo,
                                     p_descripcion = x.descripcion,
                                     p_fecha_ejecucion = x.fecha_ejecucion,
                                     p_activo = "1",
                                     p_tipo_entrega = x.tipo_entrega,
                                     p_cumplio_pe = x.cumplio_pe,
                                     p_ejecucion = x.ejecutado
                                 }
                               ).ToList<sp_plan_entrega_input_ejec>()

                         );

                //Recorremos encabezados para buscar ubicaciones
                header_ = header_.Distinct().ToList();
                foreach (sp_plan_entrega_input_ejec item in header_)
                {
                    List<UbicacionProductos> Ubicaciones = new List<UbicacionProductos>();

                    List<Guid> Ubicaciones_list = Lista.Where(x => x.tbl_plan_entrega_id == item.p_id)
                        .Select(x => x.tbl_ubicacion_id).Distinct().ToList();
                    //Recorremos ubicaciones y buscamos productos
                    foreach (Guid idUbicacion_ in Ubicaciones_list)
                    {
                        UbicacionProductos Ubicacion_item = new UbicacionProductos();
                        Ubicacion_item.tbl_ubicacion_id = idUbicacion_;
                        Ubicacion_item.EjecutorPorUbicacion = (
                                from Ejecutor in Lista
                                where Ejecutor.tbl_plan_entrega_id == item.p_id && Ejecutor.tbl_ubicacion_id == idUbicacion_
                                select Ejecutor.tbl_plan_entrega_ejecutor_id
                            ).FirstOrDefault();
                        Ubicacion_item.productos = (
                                from Producto_ in Lista
                                where Producto_.tbl_plan_entrega_id == item.p_id && Producto_.tbl_ubicacion_id == idUbicacion_
                                select new sp_plan_entrega_producto
                                {
                                    p_opt = 0,
                                    p_id = Producto_.tbl_plan_entrega_producto_id,
                                    p_tbl_contrato_producto_id = Producto_.tbl_contrato_producto_id,
                                    p_tbl_ubicacion_plan_entrega_id = Producto_.tbl_ubicacion_plan_entrega_id,
                                    p_estatus = "1",
                                    p_cantidad = Producto_.cantidad,
                                    p_detalle_actividad = Producto_.detalle_actividad,
                                    p_tipo = Producto_.tipo,
                                    cumplio = Producto_.cumplio
                                }
                            ).Distinct().ToList();
                        if (Ubicacion_item.tbl_ubicacion_id.ToString() != "00000000-0000-0000-0000-000000000000") {
                            Ubicaciones.Add(Ubicacion_item);
                        }
                    }
                    EstructuraPalnEntregaCumplio_ejec tot_tmp = new EstructuraPalnEntregaCumplio_ejec();
                    tot_tmp.Header = new sp_plan_entrega_input_ejec();
                    tot_tmp.UbicacionesProductos = new List<UbicacionProductos>();
                    //vaciar header
                    tot_tmp.Header = item;
                    tot_tmp.UbicacionesProductos = Ubicaciones;

                    tot_tmp.cumplio = NegocionPlanEntrega.sp_get_obligaciones_incumplidas(vs_plan_entrega_get.Response[0].tbl_plan_entrega_id).Response[0].conteo;
                    tot_tmp.token = NegocionPlanEntrega.sp_get_token_confirmacion(item.p_id.ToString()).Response[0].token.ToString(); 
                    ListestructuraPlanEntrega.Add(tot_tmp);

                }
                return new ResponseGeneric<List<EstructuraPalnEntregaCumplio_ejec>>(ListestructuraPlanEntrega);

            }
            catch (Exception ex) {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<EstructuraPalnEntregaCumplio_ejec>>(ex);
            }
        }

        public String Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public String Decrypt(String encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        public ResponseGeneric<dynamic> deleted_file(string token_id)
        {
            try
            {
                ResponseGeneric<dynamic> response = NegocionPlanEntrega.deleted_file(token_id);

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
        }

        public ResponseGeneric<dynamic> deleted_file_archivo(string token)
        {
            try
            {
                ResponseGeneric<dynamic> response = NegocionPlanEntrega.deleted_file_archivo(token);

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
        }

        /***********************************************************************************************/


        public ResponseGeneric<List<tbl_proveedor>> get_plan_entrega_contrato(Guid tbl_contrato_id)
        {
            ResponseGeneric<List<tbl_proveedor>> Proveedor = NegocionPlanEntrega.get_proveedor_contrato(tbl_contrato_id);
            return Proveedor;
        }
        public ResponseGeneric<List<Token_confirmacion>> get_token_cumplimiento(string tbl_plan_entrega_id)
        {
            ResponseGeneric<List<Token_confirmacion>> token = NegocionPlanEntrega.sp_get_token_confirmacion(tbl_plan_entrega_id);
            return token;
        }
        public ResponseGeneric<List<tbl_obligacion_cls>> get_obligacion_producto(Guid tbl_plan_entrega_id_, Guid tbl_producto_servicio_id)
        {
            ResponseGeneric<List<tbl_obligacion_cls>> tbl_obligacion = NegocionPlanEntrega.get_obligacion_producto(tbl_plan_entrega_id_, tbl_producto_servicio_id);
            return tbl_obligacion;
        }
        public ResponseGeneric<List<tbl_obligacion_cls_PE>> get_obligacion_producto_ubic(Guid tbl_plan_entrega_id_, Guid tbl_producto_servicio_id)
        {
            ResponseGeneric<List<tbl_obligacion_cls_PE>> tbl_obligacion = NegocionPlanEntrega.get_obligacion_producto_ejec(tbl_plan_entrega_id_, tbl_producto_servicio_id);
            return tbl_obligacion;
        }
        public ResponseGeneric<List<DropDownList>> get_ubicacion_servidor(Guid tbl_contrato_id)
        {
            ResponseGeneric<List<DropDownList>> Proveedor = NegocionPlanEntrega.get_ubicacion_servidor(tbl_contrato_id);
            return Proveedor;
        }
        public ResponseGeneric<List<Crudresponse>> Add_Archivo_PE(sp_tbl_archivosPE tbl_ArchivosPE)
        {
            try
            {
                tbl_ArchivosPE.id_ = Guid.NewGuid();
                return NegocionPlanEntrega.add_archivosPE(tbl_ArchivosPE);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<File_name>> _sp_download_filename(string entrega)
        {
            try
            {
                return NegocionPlanEntrega._sp_download_filename(entrega);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<File_name>>(ex);
            }
        }

    }
}
