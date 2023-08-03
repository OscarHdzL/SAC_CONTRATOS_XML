using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
	public class tbl_contrato_producto
	{
        public Guid id { get; set; }
        public Guid tbl_contrato_id { get; set; }
        public Guid tbl_producto_servicio_id { get; set; }
        public int cantidad_minima { get; set; }
        public int cantidad_maxima { get; set; }
        public Double monto_minimo { get; set; }
        public Double monto_maximo { get; set; }
        public String descripcion { get; set; }
        public Boolean estatus { get; set; }
        public Double unitario { get; set; }
    }

    public class tbl_contrato_producto_list
    {
        public Guid id { get; set; }
        public Guid tbl_contrato_id { get; set; }
        public String Contrato { get; set; }
        public Guid tbl_producto_servicio_id { get; set; }
        public String ProductoServicio { get; set; }
        public int cantidad_minima { get; set; }
        public int cantidad_maxima { get; set; }
        public Double monto_minimo { get; set; }
        public Double monto_maximo { get; set; }
        public String descripcion { get; set; }
        public Boolean estatus { get; set; }
        public Double unitario { get; set; }
    }


    public class tbl_contrato_producto_add
    {
        public int p_opt { get; set; }
        public Guid p_id { get; set; }
        public Guid p_tbl_contrato_id { get; set; }
        public Guid p_tbl_producto_servicio_id { get; set; }
        public int p_cantidad_minima { get; set; }
        public int p_cantidad_maxima { get; set; }
        public Double p_monto_minimo { get; set; }
        public Double p_monto_maximo { get; set; }
        public String p_descripcion { get; set; }
        public Byte p_estatus { get; set; }
        public Double p_unitario { get; set; }
    }
}
