using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_proveedor
	{
		public Guid id { get; set; }
		public Guid tbl_dependencia_id { get; set; }
		public String numero { get; set; }
		public String razon_social { get; set; }
		public String rfc { get; set; }
		public String domicilio_fiscal { get; set; }
		public String rep_legal_nombre { get; set; }
		public String rep_legal_ap_paterno { get; set; }
		public String rep_legal_ap_materno { get; set; }
		public String eje_cuenta_nombre { get; set; }
		public String eje_cuenta_ap_paterno { get; set; }
		public String eje_cuenta_ap_materno { get; set; }
		public String telefono { get; set; }
		public String extension { get; set; }
		public String correo_electronico { get; set; }
		public Boolean?  es_global { get; set; }
		public int activo { get; set; }
		public Guid? tbl_tipo_interlocutor_id { get; set; }

	}
	public class tbl_proveedor_add
	{
		public int p_opt { get; set; }
		public Guid p_id { get; set; }
		public Guid p_tbl_dependencia_id { get; set; }
		public String p_numero { get; set; }
		public String p_razon_social { get; set; }
		public String p_rfc { get; set; }
		public String p_domicilio_fiscal { get; set; }
		public String p_rep_legal_nombre { get; set; }
		public String p_rep_legal_ap_paterno { get; set; }
		public String p_rep_legal_ap_materno { get; set; }
		public String p_eje_cuenta_nombre { get; set; }
		public String p_eje_cuenta_ap_paterno { get; set; }
		public String p_eje_cuenta_ap_materno { get; set; }
		public String p_telefono { get; set; }
		public String p_extension { get; set; }
		public String p_correo_electronico { get; set; }
		public int p_es_global { get; set; }
		public Guid p_tipo_interlocutor { get; set; }
		public List<dependencias_adicionales> dependencias_adicionales { get; set; }
	}
	public class dependencias_adicionales 
	{ 
		public Guid tbl_dependencia_id { get; set; }
	}
}
