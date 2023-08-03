using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class lista_capitulos_gastos
    {
        public Guid capitulo_gasto_dependencia_id { get; set; }
        public String capitulo_gasto { get; set; }
        public Decimal monto_asignado { get; set; }
        public Decimal monto_repartido { get; set; }
        public Decimal monto_por_repartir{ get; set; }
    }
    public class lista_capitulos_gastos_area
    {
        public Guid capitulo_gasto_dependencia_id { get; set; }
        public String capitulo_gasto { get; set; }
        public Decimal monto_asignado_d { get; set; }
        public Decimal monto_repartido_d { get; set; }
        public Decimal monto_por_repartir_d { get; set; }
        public Decimal monto_asignado_area { get; set; }
    }
    public class lista_capitulos_gastos_subarea
    {
        public Guid capitulo_gasto_area_id { get; set; }
        public String capitulo_gasto { get; set; }
        public Decimal monto_asignado_a{ get; set; }
        public Decimal monto_repartido_a { get; set; }
        public Decimal monto_por_repartir_a { get; set; }
        public Decimal monto_asignado_sub { get; set; }
    }
    public class lista_capitulos_gastos_area_subordinada
    {
        public Guid capitulo_gasto_subarea_id { get; set; }
        public String capitulo_gasto { get; set; }
        public Decimal monto_asignado_s { get; set; }
        public Decimal monto_repartido_s { get; set; }
        public Decimal monto_por_repartir_s { get; set; }
        public Decimal monto_asignado_area_sub { get; set; }
    }
    public class monto_repartido_cg
    {
        public Decimal monto_repartido_a_s { get; set; }
    }
    public class lista_capitulos_gastos_area_estructura 
    {
        public lista_capitulos_gastos_area lista1 { get; set; }
        public Decimal monto_repartido { get; set; }
    }
    public class lista_capitulos_gastos_subarea_estructura
    {
        public lista_capitulos_gastos_subarea lista1 { get; set; }
        public Decimal monto_repartido { get; set; }
    }
    public class capitulo_gasto_existente
    {
        public Guid id_existente { get; set; }
    }

    public class lista_info_capitulo_gasto 
    {
        public String id { get; set; }
        public String item { get; set; }
        public Decimal monto_asignado { get; set; }
    }
}
