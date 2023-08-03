using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using AccesoDatos_AdminContratos;
using System.Linq;
using OfficeOpenXml;
using System.IO;
using Newtonsoft.Json;

namespace Negocio_AdminContratos
{
    public class Carga_Masiva_negocio
    {



        public ResponseCMAarea CM_(String Areas, Guid p_tbl_dependencia_id)
        {
                String Tmpid = Guid.NewGuid().ToString();
                Dictionary<String, String> Dictionary__ = new Dictionary<String, String>();
                Dictionary__.Add("p_opt", "2");
                Dictionary__.Add("p_id", Tmpid);
                Dictionary__.Add("p_tbl_dependencia_id", p_tbl_dependencia_id.ToString());
                Dictionary__.Add("p_area", Areas);
                Dictionary__.Add("p_total_personal", "0");
                Dictionary__.Add("p_sueldo_promedio", "0");
                Dictionary__.Add("p_total_externos", "0");
                Dictionary__.Add("p_nivel", "0");
                Dictionary__.Add("p_id_area_padre", Guid.Empty.ToString());
                Cargas_Masivas_AD AD = new Cargas_Masivas_AD(Dictionary__, "area");
                ResponseCMAarea Areacm = new ResponseCMAarea(Tmpid, Areas, AD.ResponseFunction.cod);
            return Areacm;
        }
        public ResponseCMSubArea CM_(SubAreaMasiva SubAreas, Guid p_tbl_dependencia_id)
        {
                String Tmpid = Guid.NewGuid().ToString();
                Dictionary<String, String> Dictionary__ = new Dictionary<String, String>();
                Dictionary__.Add("p_opt", "2");
                Dictionary__.Add("p_id", Tmpid);
                Dictionary__.Add("p_tbl_area_id", SubAreas.p_tbl_area_id.ToString());
                Dictionary__.Add("p_subarea", SubAreas.p_subarea);
                Cargas_Masivas_AD AD = new Cargas_Masivas_AD(Dictionary__, "subarea");
                ResponseCMSubArea subarea_ = new ResponseCMSubArea(Tmpid, SubAreas.p_subarea, AD.ResponseFunction.cod);
            return subarea_;
        }
        public Crudresponse CM_(SubordinadaMasiva SubAreas, Guid p_tbl_dependencia_id)
        {
                String Tmpid = Guid.NewGuid().ToString();
                Dictionary<String, String> Dictionary__ = new Dictionary<String, String>();
                Dictionary__.Add("p_opt", "2");
                Dictionary__.Add("p_id", Tmpid);
                Dictionary__.Add("p_tbl_subarea_id", SubAreas.p_tbl_subarea_id.ToString());
                Dictionary__.Add("p_area_subordinada", SubAreas.p_area_subordinada);
                Cargas_Masivas_AD AD = new Cargas_Masivas_AD(Dictionary__, "subordinada");
                AD.ResponseFunction.msg = Tmpid;
                return AD.ResponseFunction;
        }
        public Crudresponse CM_(tbl_proveedor_add Proveedores, Guid p_tbl_dependencia_id)
        {

                Guid identifier = Guid.NewGuid();
                Dictionary<String, String> Dictionary__ = new Dictionary<String, String>();
                Dictionary__.Add("p_opt", "2");
                Dictionary__.Add("p_id", identifier.ToString());
                Dictionary__.Add("p_tbl_dependencia_id", p_tbl_dependencia_id.ToString());
                Dictionary__.Add("p_numero", Proveedores.p_numero.ToString());
                Dictionary__.Add("p_razon_social", Proveedores.p_razon_social.ToString());
                Dictionary__.Add("p_rfc", Proveedores.p_rfc.ToString());
                Dictionary__.Add("p_domicilio_fiscal", Proveedores.p_domicilio_fiscal.ToString());
                Dictionary__.Add("p_rep_legal_nombre", Proveedores.p_rep_legal_nombre.ToString());
                Dictionary__.Add("p_rep_legal_ap_paterno", Proveedores.p_rep_legal_ap_paterno);
                Dictionary__.Add("p_rep_legal_ap_materno", Proveedores.p_rep_legal_ap_materno);
                Dictionary__.Add("p_eje_cuenta_nombre", Proveedores.p_eje_cuenta_nombre.ToString());
                Dictionary__.Add("p_eje_cuenta_ap_paterno", Proveedores.p_eje_cuenta_ap_paterno.ToString());
                Dictionary__.Add("p_eje_cuenta_ap_materno", Proveedores.p_eje_cuenta_ap_materno.ToString());
                Dictionary__.Add("p_telefono", Proveedores.p_telefono.ToString());
                Dictionary__.Add("p_extension", Proveedores.p_extension.ToString());
                Dictionary__.Add("p_correo_electronico", Proveedores.p_correo_electronico.ToString());
                Dictionary__.Add("p_es_global","1");
                Dictionary__.Add("p_tbl_tipo_interlocutor_id", Proveedores.p_tipo_interlocutor.ToString());
            Cargas_Masivas_AD AD = new Cargas_Masivas_AD(Dictionary__, "proveedor");
                AD.ResponseFunction.msg = identifier.ToString();
            return AD.ResponseFunction;
        }
        public String ReadExcel(String startupPath, Guid p_tbl_dependencia_id)
        {
            string path = startupPath;
            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            int rows = worksheet.Dimension.Rows; 
            int columns = worksheet.Dimension.Columns;
            List<CargaMasiva> CargaMasivaAreas_ = new List<CargaMasiva>();
            for (int i = 2; i <= rows; i++)
            {
                CargaMasiva _CM_ = new CargaMasiva();
                for (int j = 1; j <= columns; j++)
                {
                    if (j == 1){
                        _CM_.Areas = worksheet.Cells[i, j].Value.ToString();
                    }
                    else if (j == 2){
                        _CM_.SubAreas = worksheet.Cells[i, j].Value.ToString();
                    }
                    else if (j == 3){
                        _CM_.Oficina = worksheet.Cells[i, j].Value.ToString();
                    }
                }
                CargaMasivaAreas_.Add(_CM_);
            }
            List<String> AreasLst = new List<String>();
            List<String> AreaSubLst = new List<String>();
            List<String> OficinaLst = new List<String>();
            List<ErrorLst> ErrorLst_ = new List<ErrorLst>();
            int ct = 0;
            foreach (CargaMasiva item in CargaMasivaAreas_)
            {
       
                if (!AreasLst.Contains(item.Areas))
                {
                    //Comienza Carga Areas
                    ResponseCMAarea RCMA = CM_(item.Areas, p_tbl_dependencia_id);
                    AreasLst.Add(item.Areas);
                    CargaMasivaAreas_[ct].idAreas = Guid.Parse(RCMA.id);

                    (from AreaArr in CargaMasivaAreas_
                     where AreaArr.Areas == item.Areas
                     select AreaArr).ToList().ForEach(x => x.idAreas = Guid.Parse(RCMA.id));
                    ErrorLst Error = new ErrorLst("Area", RCMA.Status, RCMA.id, item.Areas);
                    ErrorLst_.Add(Error);


                    if (RCMA.Status == "success")
                    {

                        //Comienza Carga SubAreas
                        if (!AreaSubLst.Contains(item.SubAreas) && item.SubAreas != "-")
                        {
                            String inicial = JsonConvert.SerializeObject(CargaMasivaAreas_);

                            SubAreaMasiva SAM = new SubAreaMasiva();
                            SAM.p_subarea = item.SubAreas;
                            SAM.p_tbl_area_id = Guid.Parse(RCMA.id);
                            ResponseCMSubArea RCMSA = CM_(SAM, p_tbl_dependencia_id);
                            AreaSubLst.Add(item.SubAreas);
                            CargaMasivaAreas_[ct].idSubAreas = Guid.Parse(RCMSA.id);

                            (from SubArr in CargaMasivaAreas_
                             where SubArr.SubAreas == item.SubAreas
                             select SubArr).ToList().ForEach(x => x.idSubAreas = Guid.Parse(RCMSA.id));

                            ErrorLst Error_ = new ErrorLst("SubArea", RCMSA.Status, RCMSA.id, item.SubAreas);
                            ErrorLst_.Add(Error_);

                            //Comienza Carga Oficinas
                            String inicial_ = JsonConvert.SerializeObject(CargaMasivaAreas_);

                            if (!OficinaLst.Contains(item.Oficina) && item.Oficina != "-")
                            {
                                SubordinadaMasiva SM = new SubordinadaMasiva();
                                SM.p_tbl_subarea_id = Guid.Parse(RCMSA.id);
                                SM.p_area_subordinada = item.Oficina;
                                Crudresponse CrudR = CM_(SM, p_tbl_dependencia_id);
                                CargaMasivaAreas_[ct].idOficina = Guid.Parse(CrudR.msg);
                                OficinaLst.Add(item.Oficina);
                                ErrorLst Error__ = new ErrorLst("Oficina", CrudR.cod, CrudR.msg, item.Oficina);
                                ErrorLst_.Add(Error__);

                            }
                        }
                    }
                    ct++;
                }
                else
                {
                    //Comienza Carga SubAreas
                    if (!AreaSubLst.Contains(item.SubAreas) && item.SubAreas != "-")
                    {
                        String inicial = JsonConvert.SerializeObject(CargaMasivaAreas_);

                        SubAreaMasiva SAM = new SubAreaMasiva();
                        SAM.p_subarea = item.SubAreas;
                        SAM.p_tbl_area_id = CargaMasivaAreas_[ct].idAreas;
                        ResponseCMSubArea RCMSA = CM_(SAM, p_tbl_dependencia_id);
                        AreaSubLst.Add(item.SubAreas);
                        CargaMasivaAreas_[ct].idSubAreas = Guid.Parse(RCMSA.id);
                        (from SubArr in CargaMasivaAreas_
                         where SubArr.SubAreas == item.SubAreas
                         select SubArr).ToList().ForEach(x => x.idSubAreas = Guid.Parse(RCMSA.id));
                        ErrorLst Error = new ErrorLst("SubArea", RCMSA.Status, RCMSA.id, item.SubAreas);
                        ErrorLst_.Add(Error);
                        //Comienza Carga Oficinas
                        if (!OficinaLst.Contains(item.Oficina) && item.Oficina != "-")
                        {
                            String inicial_ = JsonConvert.SerializeObject(CargaMasivaAreas_);

                            SubordinadaMasiva SM = new SubordinadaMasiva();
                            SM.p_tbl_subarea_id = Guid.Parse(RCMSA.id);
                            SM.p_area_subordinada = item.Oficina;
                            Crudresponse CrudR = CM_(SM, p_tbl_dependencia_id);
                            CargaMasivaAreas_[ct].idOficina = Guid.Parse(CrudR.msg);
                            OficinaLst.Add(item.Oficina);

                            (from SubordiArr in CargaMasivaAreas_
                             where SubordiArr.Oficina == item.Oficina
                             select SubordiArr).ToList().ForEach(x => x.idOficina = Guid.Parse(CrudR.msg));
                            ErrorLst Error_ = new ErrorLst("Oficina", CrudR.cod, CrudR.msg, item.Oficina);
                            ErrorLst_.Add(Error_);
                        }
                        ct++;
                    }
                    else
                    {
                        //Comienza Carga Oficinas
                        if (!OficinaLst.Contains(item.Oficina) && item.Oficina != "-")
                        {
                            String inicial = JsonConvert.SerializeObject(CargaMasivaAreas_);
                            SubordinadaMasiva SM = new SubordinadaMasiva();
                            SM.p_tbl_subarea_id = item.idSubAreas;
                            SM.p_area_subordinada = item.Oficina;
                            Crudresponse CrudR = CM_(SM, p_tbl_dependencia_id);
                            //CargaMasivaAreas_[ct].idOficina = Guid.Parse(CrudR.msg);
                            OficinaLst.Add(item.Oficina);
                            (from SubordiArr in CargaMasivaAreas_
                             where SubordiArr.Oficina == item.Oficina
                             select SubordiArr).ToList().ForEach(x => x.idOficina = Guid.Parse(CrudR.msg));
                            ErrorLst Error_ = new ErrorLst("Oficina", CrudR.cod, CrudR.msg, item.Oficina);
                            ErrorLst_.Add(Error_);
                        }
                        ct++;
                    }
                }
            }



            return JsonConvert.SerializeObject(ErrorLst_);
        }
        public String ReadExcelProveedor(String startupPath, Guid p_tbl_dependencia_id, Guid p_id_instancia)
        {
            string path = startupPath;
            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            int rows = worksheet.Dimension.Rows;
            int columns = worksheet.Dimension.Columns;
            List<tbl_proveedor_add> CargaMasivaProveedor = new List<tbl_proveedor_add>();
            tbl_proveedor_negocio _Proveedor_Negocio = new tbl_proveedor_negocio();
            var listaProveedores = _Proveedor_Negocio.Get_lista_tipo_interlocutor("1").Response;
            List<ErrorLst> DatosInvalidos_ = new List<ErrorLst>();

            for (int i = 2; i <= rows; i++)
            {
                tbl_proveedor_add _CM_ = new tbl_proveedor_add();
                _CM_.p_opt = 2;
                _CM_.p_id = Guid.NewGuid();
                _CM_.p_tbl_dependencia_id = p_tbl_dependencia_id;
                _CM_.p_numero = "";
                _CM_.p_telefono = worksheet.Cells[i, 1].Value == null ? "" : worksheet.Cells[i, 1].Value.ToString();
                _CM_.p_razon_social = worksheet.Cells[i, 2].Value == null ? "" : worksheet.Cells[i, 2].Value.ToString();
                _CM_.p_rfc = worksheet.Cells[i, 3].Value == null ? "" : worksheet.Cells[i, 3].Value.ToString();
                _CM_.p_domicilio_fiscal = worksheet.Cells[i, 4].Value == null ? "" : worksheet.Cells[i, 4].Value.ToString();
                _CM_.p_rep_legal_nombre = worksheet.Cells[i, 5].Value == null ? "" : worksheet.Cells[i, 5].Value.ToString();
                _CM_.p_rep_legal_ap_paterno = worksheet.Cells[i, 6].Value == null ? "" : worksheet.Cells[i, 6].Value.ToString();
                _CM_.p_rep_legal_ap_materno = worksheet.Cells[i, 7].Value == null ? "" : worksheet.Cells[i, 7].Value.ToString();
                _CM_.p_eje_cuenta_nombre = worksheet.Cells[i, 8].Value == null ? "" : worksheet.Cells[i, 8].Value.ToString();
                _CM_.p_eje_cuenta_ap_paterno = worksheet.Cells[i, 9].Value == null ? "" : worksheet.Cells[i, 9].Value.ToString();
                _CM_.p_eje_cuenta_ap_materno = worksheet.Cells[i, 10].Value == null ? "" : worksheet.Cells[i, 10].Value.ToString();
                _CM_.p_extension = worksheet.Cells[i, 11].Value == null ? "" : worksheet.Cells[i, 11].Value.ToString();
                _CM_.p_correo_electronico = worksheet.Cells[i, 12].Value == null ? "" : worksheet.Cells[i, 12].Value.ToString();
                _CM_.p_es_global = worksheet.Cells[i, 13].Value == null ? 0 : Convert.ToInt32(worksheet.Cells[i, 13].Value.ToString());
                _CM_.p_tipo_interlocutor = worksheet.Cells[i, 14].Value == null ? Guid.Empty : Guid.Parse(listaProveedores.Where(x => x.nombre.ToLower().Contains(worksheet.Cells[i, 14].Value.ToString().ToLower())).FirstOrDefault().id);
                var validacion = ValidarCamposVacios(_CM_);
                if (validacion.Item1)
                {
                    CargaMasivaProveedor.Add(_CM_);
                }
                else {
                    DatosInvalidos_.Add(new ErrorLst("Error","fail" , validacion.Item2, "Fila " + i));
                }
            }
            List<ErrorLst> ErrorLst_ = new List<ErrorLst>();
            if (DatosInvalidos_.Count > 0) {
                return JsonConvert.SerializeObject(DatosInvalidos_);

            }
            foreach (tbl_proveedor_add item in CargaMasivaProveedor)
            {
                Crudresponse CR = CM_(item, p_tbl_dependencia_id);
                var tipoInterlocutor = listaProveedores.Where(x => x.id == item.p_tipo_interlocutor.ToString()).FirstOrDefault().nombre;
                ErrorLst EL = new ErrorLst(tipoInterlocutor, CR.cod, CR.msg, item.p_razon_social);
                ErrorLst_.Add(EL);
            }
            return JsonConvert.SerializeObject(ErrorLst_);
        }
        public Tuple<bool,string> ValidarCamposVacios(tbl_proveedor_add modelo) 
        {
            if (modelo.p_razon_social == null || modelo.p_razon_social == "")
            {
                return new Tuple<bool,string>(false,"Debe ingresar la razón social");
            }
            if (modelo.p_rfc == null || modelo.p_rfc == "")
            {
                return new Tuple<bool, string>(false, "Debe ingresar el rfc");
            }
            if (modelo.p_rfc.Length < 12) {
                return new Tuple<bool, string>(false, "El rfc no debe ser menor a 12 caracteres");
            }
            if (modelo.p_rep_legal_nombre == null || modelo.p_rep_legal_nombre == "")
            {
                return new Tuple<bool, string>(false, "Ingrese el nombre del representante legal");
            }
            if (modelo.p_rep_legal_ap_paterno == null || modelo.p_rep_legal_ap_paterno == "")
            {
                return new Tuple<bool, string>(false, "Ingrese el apellido paterno del representante legal");
            }
            if (modelo.p_eje_cuenta_nombre == null || modelo.p_eje_cuenta_nombre == "")
            {
                return new Tuple<bool, string>(false, "Ingrese el nombre del ejecutivo de cuenta");
            }
            if (modelo.p_eje_cuenta_ap_paterno == null || modelo.p_eje_cuenta_ap_paterno == "")
            {
                return new Tuple<bool, string>(false, "Ingrese el apellido paterno del ejecutivo de cuenta");
            }
            if (modelo.p_domicilio_fiscal == null || modelo.p_domicilio_fiscal == "")
            {
                return new Tuple<bool, string>(false, "Ingrese el domicio fiscal");
            }
            if (modelo.p_telefono == null || modelo.p_telefono == "")
            {
                return new Tuple<bool, string>(false, "Ingrese el número de teléfono");
            }
            if (modelo.p_correo_electronico == null || modelo.p_correo_electronico == "")
            {
                return new Tuple<bool, string>(false, "Ingrese el correo electrónico");
            }
            if (modelo.p_tipo_interlocutor == null || modelo.p_tipo_interlocutor == Guid.Empty)
            {
                return new Tuple<bool, string>(false, "Ingrese el tipo de interlocutor correcto");
            }

            //if (modelo.p_rep_legal_ap_materno == null || modelo.p_rep_legal_ap_materno == "")
            //{
            //    return false;
            //}
            //if (modelo.p_eje_cuenta_ap_materno == null || modelo.p_eje_cuenta_ap_materno == "")
            //{
            //    return false;
            //}
            //if (modelo.p_telefono == null || modelo.p_telefono == "")
            //{
            //    return false;
            //}
            //if (modelo.p_extension == null || modelo.p_extension == "")
            //{
            //    return false;
            //}


            return new Tuple<bool, string>(true, "");
        }

    }
}
