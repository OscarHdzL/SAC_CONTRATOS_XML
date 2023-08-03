$(document).ready(function () {
    $('.div_titulo').hide();
    $('#_div_informacion_x_dep').hide();
    LaunchLoader(true);

    Get_arbol();
    Get_Ejercicio();
    _arbo_dep_areas();
    monitorear_tamaño();
})
function Get_Ejercicio() {
    $.get($("#EndPointAdmon").val() + "Dependencia/Get/Ejercicio", function (data, status) {
        var body = "";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].value + "'>Ejercicio " + data[i].text + "</option>";
        }
        $('#opt_Ejercicio').html(body);
    });
    return;
}
function Get_arbol() {
    $.get("/AsignacionOrganizacional/lista_estructura", function (data, status) {
        $("#_arbol").html(data);
        LaunchLoader(false);
    });
}
function _arbo_dep_areas() {
    $('.list-group-item').on('click', function () {
        $('.glyphicon', this)
            .toggleClass('glyphicon-chevron-right')
            .toggleClass('glyphicon-chevron-down');
    });
}

function obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem) {
    $("#IdItem").val(IdItem);
    $("#IdItem_d").val(IdItem_d);
    $("#ItemTexto").val(ItemTexto);
    $("#TipoItem").val(TipoItem);

    LaunchLoader(true);
    $('.div_titulo').show();
    $('.titulo').html('Asignación a:' + ' ' + ItemTexto);
    var id_ejercicio = $("#opt_Ejercicio").val();
    var Monto_total = 0;
    var Monto_total_x_repatir = 0;
    var Monto_repartido = 0;

    if (TipoItem == 1) {
        $.get($("#EndPointAdmon").val() + "Estructura/Get/Lista/Capitulos/" + IdItem + "/" + id_ejercicio, function (data, status) {
            if (data.length > 0) {
                //$('#_div_informacion_x_dep').addClass("collapsed-box");
                var myTable = "<table class='table table-striped' id='tbl_lista_capitulos_gastos'><tr><td style='width: 40%; text-align: left;'>Capítulos de Gasto</td>";
                myTable += "<td style='width: 25%; text-align: left;'>Monto por repartir</td>";
                myTable += "<td style='width: 35%; text-align: left;'>Monto autorizado</td>";
                myTable += "<td style='width: 35%; text-align: left;'></td></tr>";
                for (var y = 0; y <= data.length - 1; y++) {
                    var monto = data[y].monto_asignado == 0 ? "" : data[y].monto_asignado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    var monto_x_repartir = data[y].monto_por_repartir == 0 ? 0 : data[y].monto_por_repartir.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    myTable += "<tr><td style='width: 40%; text-align: left; font-size:90%;'>" + data[y].capitulo_gasto + "</td>";
                    myTable += "<td style='width: 25%; text-align: left;'>" + '<input type="text" disabled class="form-control monto_respartido_x_area" id="monto_rep' + data[y].capitulo_gasto_dependencia_id + '" value="' + monto_x_repartir + '">' + "</td>";
                    myTable += "<td style='width: 30%; text-align: left;'>" + '<div class="input-group" id="div_' + data[y].capitulo_gasto_dependencia_id + '"><span class="input-group-addon" id="span_' + data[y].capitulo_gasto_dependencia_id + '">$</span><input type="text" class="form-control monto_cap" placeholder="$ 0.00" id="monto_' + data[y].capitulo_gasto_dependencia_id + '" value="' + monto + '" autocomplete="off" ></div>' + "</td>";
                    if (monto != 0 && (data[y].monto_por_repartir != data[y].monto_asignado)) {
                        myTable += "<td style='width: 5%; text-align: left;'><button class='btn btn-light' title='Ver asignación en áreas' onclick=\"info_asignacion_area('" + data[y].capitulo_gasto_dependencia_id + "','" + IdItem + "', '" + data[y].capitulo_gasto + "');\"><i class='fa fa-pie-chart'></i></button></td>";
                    }
                    else {
                        myTable += "<td style='width: 5%; text-align: left;'></td>";
                    }
                    myTable += "</tr>";

                    Monto_total += data[y].monto_asignado;
                    Monto_total_x_repatir += data[y].monto_por_repartir;
                }
                myTable += "</table>";

                Monto_repartido = parseFloat(Monto_total - Monto_total_x_repatir);
                var tabla_info = "<table class='table table-striped' id='tbl_informacion'><tr><td style='width: 40%; text-align: left;'></td>";
                tabla_info += "<td style='width: 30%; text-align: left;'></td>";
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total de la dependencia.</td>";
                tabla_info += "<td style='width: 30%; text-align: left;'>" + '$ ' + Monto_total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total repartido por áreas.</td>";
                if (Monto_repartido > 0) {
                    tabla_info += "<td style='width: 30%; text-align: left; color:red;'>" + '$ ' + Monto_repartido.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                else {
                    tabla_info += "<td style='width: 30%; text-align: left;'>" + '$ ' + Monto_repartido.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total por repartir.</td>";
                if (Monto_total_x_repatir > 0) {
                    tabla_info += "<td style='width: 30%; text-align: left; color:green;'>" + '$ ' + Monto_total_x_repatir.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                else {
                    tabla_info += "<td style='width: 30%; text-align: left; color:red;'>" + '$ ' + Monto_total_x_repatir.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                tabla_info += "</table>";

                var button = "<button type='button' class='btn btn-primary' onclick='Guardar_Montos_dep()'>Guardar</button>";
                $("#Desglose_partidas").html(myTable);
                $("#_info").html(tabla_info);
                formato_moneda();
                $("#foter").html(button)
                $('#_div_informacion_x_dep').show();
                LaunchLoader(false);
            }
            else {
                $('#_div_informacion_x_dep').hide();
                var error = "<div align='center'>";
                error += "<h3><i class='fa fa-warning text-yellow'></i> Ningún capítulo encontrado.</h3>";
                error += "<p>Esta dependencia no cuenta con ningún capítulo de gasto configurado. </p> <a href = '../Dependencias'>Ir a la configuración.</a></p >";
                error += "</div>";

                $("#Desglose_partidas").html(error);
                $("#foter").html("")
                LaunchLoader(false);
            }

        });
    }
    if (TipoItem == 2) {
        $('#id_area').val(IdItem);
        $.get($("#EndPointAdmon").val() + "Estructura/Get/Lista/Capitulos/Area/" + IdItem_d + "/" + IdItem + "/" + id_ejercicio, function (data, status) {
            if (data.length > 0) {
                //$('#_div_informacion_x_dep').addClass("collapsed-box");
                var myTable = "<table class='table table-striped' id='tbl_lista_capitulos_gastos'><tr><td style='width: 40%; text-align: left;'>Capítulos de Gasto</td>";
                myTable += "<td style='width: 30%; text-align: left;'>Monto disponible de la dependencia</td>";
                myTable += "<td style='width: 30%; text-align: left;'>Monto autorizado</td>";
                myTable += "<td style='width: 35%; text-align: left;'></td></tr>";
                for (var y = 0; y <= data.length - 1; y++) {
                    var monto = data[y].lista1.monto_asignado_area == 0 ? "" : data[y].lista1.monto_asignado_area.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    var monto_x_repartir = data[y].lista1.monto_por_repartir_d == 0 ? 0 : data[y].lista1.monto_por_repartir_d.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    myTable += "<tr><td style='width: 40%; text-align: left; font-size:90%;'>" + data[y].lista1.capitulo_gasto + "</td>";
                    myTable += "<td style='width: 30%; text-align: left;'>" + '<input type="text" disabled class="form-control monto_respartido_x_area" id="monto_rep' + data[y].lista1.capitulo_gasto_dependencia_id + '" value="' + monto_x_repartir + '">' + "</td>";
                    myTable += "<td style='width: 30%; text-align: left;'>" + '<div class="input-group" id="div_' + data[y].lista1.capitulo_gasto_dependencia_id + '"><span class="input-group-addon" id="span_' + data[y].lista1.capitulo_gasto_dependencia_id + '">$</span><input type="text" class="form-control monto_cap" placeholder="$ 0.00" id="monto_' + data[y].lista1.capitulo_gasto_dependencia_id + '" value="' + monto + '" autocomplete="off" ></div>' + "</td>";
                    if (monto != 0 && data[y].monto_repartido != 0) {
                        myTable += "<td style='width: 5%; text-align: left;'><button class='btn btn-light' title='Ver asignación en subáreas' onclick=\"info_asignacion_subarea('" + data[y].lista1.capitulo_gasto_dependencia_id + "','" + IdItem + "', '" + data[y].lista1.capitulo_gasto + "');\"><i class='fa fa-pie-chart'></i></button></td>";
                    }
                    else {
                        myTable += "<td style='width: 5%; text-align: left;'></td>";
                    }
                    myTable += "</tr>";

                    Monto_total += data[y].lista1.monto_asignado_area;
                    Monto_repartido += data[y].monto_repartido;
                }
                myTable += "</table>";

                Monto_total_x_repatir = parseFloat(Monto_total - Monto_repartido);
                var tabla_info = "<table class='table table-striped' id='tbl_informacion'><tr><td style='width: 40%; text-align: left;'></td>";
                tabla_info += "<td style='width: 30%; text-align: left;'></td>";
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total del área.</td>";
                tabla_info += "<td style='width: 30%; text-align: left;'>" + '$ ' + Monto_total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total repartido por subáreas.</td>";
                if (Monto_repartido > 0) {
                    tabla_info += "<td style='width: 30%; text-align: left; color:red;'>" + '$ ' + Monto_repartido.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                else {
                    tabla_info += "<td style='width: 30%; text-align: left;'>" + '$ ' + Monto_repartido.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total por repartir.</td>";
                if (Monto_total_x_repatir > 0) {
                    tabla_info += "<td style='width: 30%; text-align: left; color:green;'>" + '$ ' + Monto_total_x_repatir.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                else {
                    tabla_info += "<td style='width: 30%; text-align: left; color:red;'>" + '$ ' + Monto_total_x_repatir.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                tabla_info += "</table>";


                var button = "<button type='button' class='btn btn-primary' onclick='Guardar_Montos_area()'>Guardar</button>";
                $("#Desglose_partidas").html(myTable);
                $("#_info").html(tabla_info);
                formato_moneda();
                $("#foter").html(button);
                $('#_div_informacion_x_dep').show();
                LaunchLoader(false);
            }
            else {
                $('#_div_informacion_x_dep').hide();
                var error = "<div align='center'>";
                error += "<h3><i class='fa fa-warning text-yellow'></i> Ningún capítulo encontrado.</h3>";
                error += "<p>Esta área no cuenta con ningún capítulo de gasto configurado, primero debe configurar la dependencia a la cual pertenece </p> <a href = '../Dependencias'>Ir a la configuración.</a></p >";
                error += "</div>";

                $("#Desglose_partidas").html(error);
                $("#foter").html("");
                LaunchLoader(false);
            }

        });
    }
    if (TipoItem == 3) {
        $('#id_subarea').val(IdItem);
        $.get($("#EndPointAdmon").val() + "Estructura/Get/Lista/Capitulos/Subarea/" + IdItem_d + "/" + IdItem + "/" + id_ejercicio, function (data, status) {
            if (data.length > 0) {
                //$('#_div_informacion_x_dep').addClass("collapsed-box");
                var myTable = "<table class='table table-striped' id='tbl_lista_capitulos_gastos'><tr><td style='width: 40%; text-align: left;'>Capítulos de Gasto</td>";
                myTable += "<td style='width: 30%; text-align: left;'>Monto disponible del área</td>";
                myTable += "<td style='width: 30%; text-align: left;'>Monto autorizado</td>";
                myTable += "<td style='width: 35%; text-align: left;'></td></tr>";
                for (var y = 0; y <= data.length - 1; y++) {
                    var monto = data[y].lista1.monto_asignado_sub == 0 ? "" : data[y].lista1.monto_asignado_sub.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    var monto_x_repartir = data[y].lista1.monto_por_repartir_a == 0 ? 0 : data[y].lista1.monto_por_repartir_a.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    myTable += "<tr><td style='width: 40%; text-align: left; font-size:90%;'>" + data[y].lista1.capitulo_gasto + "</td>";
                    myTable += "<td style='width: 30%; text-align: left;'>" + '<input type="text" disabled class="form-control monto_respartido_x_area" id="monto_rep' + data[y].lista1.capitulo_gasto_area_id + '" value="' + monto_x_repartir + '">' + "</td>";
                    myTable += "<td style='width: 30%; text-align: left;'>" + '<div class="input-group" id="div_' + data[y].lista1.capitulo_gasto_area_id + '"><span class="input-group-addon" id="span_' + data[y].lista1.capitulo_gasto_area_id + '">$</span><input type="text" class="form-control monto_cap" placeholder="$ 0.00" id="monto_' + data[y].lista1.capitulo_gasto_area_id + '" value="' + monto + '" autocomplete="off" ></div>' + "</td>";
                    if (monto != 0 && data[y].monto_repartido != 0) {
                        myTable += "<td style='width: 5%; text-align: left;'><button class='btn btn-light' title='Ver asignación en áreas subordinadas' onclick=\"info_asignacion_subordinado('" + data[y].lista1.capitulo_gasto_area_id + "','" + IdItem + "', '" + data[y].lista1.capitulo_gasto + "');\"><i class='fa fa-pie-chart'></i></button></td>";
                    }
                    else {
                        myTable += "<td style='width: 5%; text-align: left;'></td>";
                    }
                    myTable += "</tr>";

                    Monto_total += data[y].lista1.monto_asignado_sub;
                    Monto_repartido += data[y].monto_repartido;
                }
                myTable += "</table>";

                Monto_total_x_repatir = parseFloat(Monto_total - Monto_repartido);
                var tabla_info = "<table class='table table-striped' id='tbl_informacion'><tr><td style='width: 40%; text-align: left;'></td>";
                tabla_info += "<td style='width: 30%; text-align: left;'></td>";
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total del subárea.</td>";
                tabla_info += "<td style='width: 30%; text-align: left;'>" + '$ ' + Monto_total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total repartido por áreas subordinadas.</td>";
                if (Monto_repartido > 0) {
                    tabla_info += "<td style='width: 30%; text-align: left; color:red;'>" + '$ ' + Monto_repartido.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                else {
                    tabla_info += "<td style='width: 30%; text-align: left;'>" + '$ ' + Monto_repartido.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                tabla_info += "<tr><td style='width: 40%; text-align: left; font-size:100%;'>Monto total por repartir.</td>";
                if (Monto_total_x_repatir > 0) {
                    tabla_info += "<td style='width: 30%; text-align: left; color:green;'>" + '$ ' + Monto_total_x_repatir.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                else {
                    tabla_info += "<td style='width: 30%; text-align: left; color:red;'>" + '$ ' + Monto_total_x_repatir.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') + "</td></tr>";
                }
                tabla_info += "</table>";

                var button = "<button type='button' class='btn btn-primary' onclick='Guardar_Montos_subarea()'>Guardar</button>";
                $("#Desglose_partidas").html(myTable);
                $("#_info").html(tabla_info);
                formato_moneda();
                $("#foter").html(button);
                $('#_div_informacion_x_dep').show();
                LaunchLoader(false);
            }
            else {
                $('#_div_informacion_x_dep').hide();
                var error = "<div align='center'>";
                error += "<h3><i class='fa fa-warning text-yellow'></i> Ningún capítulo encontrado.</h3>";
                error += "<p>Esta subárea no cuenta con ningún capítulo de gasto configurado, primero debe asignar presupuesto al área a la cual pertenece.</p></a></p >";
                error += "</div>";

                $("#Desglose_partidas").html(error);
                $("#foter").html("")
                LaunchLoader(false);
            }

        });
    }
    if (TipoItem == 4) {
        $('#id_area_sub').val(IdItem);
        $.get($("#EndPointAdmon").val() + "Estructura/Get/Lista/Capitulos/Subordinada/" + IdItem_d + "/" + IdItem + "/" + id_ejercicio, function (data, status) {
            if (data.length > 0) {
                $('#_div_informacion_x_dep').hide();
                var myTable = "<table class='table table-striped' id='tbl_lista_capitulos_gastos'><tr><td style='width: 40%; text-align: left;'>Capítulos de Gasto</td>";
                myTable += "<td style='width: 30%; text-align: left;'>Monto disponible de la subárea</td>";
                myTable += "<td style='width: 30%; text-align: left;'>Monto autorizado</td>";
                for (var y = 0; y <= data.length - 1; y++) {
                    var monto = data[y].monto_asignado_area_sub == 0 ? "" : data[y].monto_asignado_area_sub.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    var monto_x_repartir = data[y].monto_por_repartir_s == 0 ? 0 : data[y].monto_por_repartir_s.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    myTable += "<tr><td style='width: 40%; text-align: left; font-size:90%;'>" + data[y].capitulo_gasto + "</td>";
                    myTable += "<td style='width: 30%; text-align: left;'>" + '<input type="text" disabled class="form-control monto_respartido_x_area" id="monto_rep' + data[y].capitulo_gasto_subarea_id + '" value="' + monto_x_repartir + '">' + "</td>";
                    myTable += "<td style='width: 30%; text-align: left;'>" + '<div class="input-group" id="div_' + data[y].capitulo_gasto_subarea_id + '"><span class="input-group-addon" id="span_' + data[y].capitulo_gasto_subarea_id + '">$</span><input type="text" class="form-control monto_cap" placeholder="$ 0.00" id="monto_' + data[y].capitulo_gasto_subarea_id + '" value="' + monto + '" autocomplete="off" ></div>' + "</td>";
                    myTable += "</tr>";
                }
                myTable += "</table>";
                var button = "<button type='button' class='btn btn-primary' onclick='Guardar_Montos_area_sub()'>Guardar</button>";
                $("#Desglose_partidas").html(myTable);
                formato_moneda();
                $("#foter").html(button)
                LaunchLoader(false);
            }
            else {
                $('#_div_informacion_x_dep').hide();
                var error = "<div align='center'>";
                error += "<h3><i class='fa fa-warning text-yellow'></i> Ningún capítulo encontrado.</h3>";
                error += "<p>Esta área subordinada no cuenta con ningún capítulo de gasto configurado, primero debe asignar presupuesto a la subárea a la cual pertenece.</p></a></p >";
                error += "</div>";

                $("#Desglose_partidas").html(error);
                $("#foter").html("")
                LaunchLoader(false);
            }

        });
    }
}

function tbl_capitulo_gasto_dependencia(p_opt, p_id, p_tbl_dependencia_id, p_tbl_capitulo_gasto_id, p_tbl_ejercicio_id, p_monto_asignado) {
    this.p_opt = p_opt;
    this.p_id = p_id;
    this.p_tbl_dependencia_id = p_tbl_dependencia_id;
    this.p_tbl_capitulo_gasto_id = p_tbl_capitulo_gasto_id;
    this.p_tbl_ejercicio_id = p_tbl_ejercicio_id;
    this.p_monto_asignado = p_monto_asignado;
}
function tbl_capitulo_gasto_area(p_opt, p_id, p_tbl_capitulo_gasto_dependencia_id, p_tbl_area_id, p_monto_asignado) {
    this.p_opt = p_opt;
    this.p_id = p_id;
    this.p_tbl_capitulo_gasto_dependencia_id = p_tbl_capitulo_gasto_dependencia_id;
    this.p_tbl_area_id = p_tbl_area_id;
    this.p_monto_asignado = p_monto_asignado;
}
function tbl_capitulo_gasto_subarea(p_opt, p_id, p_tbl_capitulo_gasto_area_id, p_tbl_subarea_id, p_monto_asignado) {
    this.p_opt = p_opt;
    this.p_id = p_id;
    this.p_tbl_capitulo_gasto_area_id = p_tbl_capitulo_gasto_area_id;
    this.p_tbl_subarea_id = p_tbl_subarea_id;
    this.p_monto_asignado = p_monto_asignado;
}
function tbl_capitulo_gasto_area_subordinada(p_opt, p_id, p_tbl_capitulo_gasto_subarea_id, p_tbl_area_subordinada_id, p_monto_asignado) {
    this.p_opt = p_opt;
    this.p_id = p_id;
    this.p_tbl_capitulo_gasto_subarea_id = p_tbl_capitulo_gasto_subarea_id;
    this.p_tbl_area_subordinada_id = p_tbl_area_subordinada_id;
    this.p_monto_asignado = p_monto_asignado;
}

/************************Guardar Montos*************************/

function Guardar_monto_cg_dep() {

    var lista_cg_d = [];
    var lista_tbl_capitulo_gasto_dependencia = [];
    var listaSuccess = [];
    var ListaError = [];


    $(".monto_cap").each(function () {
        capitulo_gasto_dependencia_id = "#" + this.id;
        var id = this.id.split('_');
        var monto_asignado = $(capitulo_gasto_dependencia_id).val() == "" ? 0 : $(capitulo_gasto_dependencia_id).val();
        if (monto_asignado != 0) {
            lista_cg_d.push(id[1] + "," + monto_asignado.replace(/,/g, ""));
        }
        else {
            lista_cg_d.push(id[1] + "," + monto_asignado);
        }

    });
    for (x = 0; x <= lista_cg_d.length - 1; x++) {
        texto = lista_cg_d[x].split(",");
        var obj = new tbl_capitulo_gasto_dependencia(3, texto[0], '', '', '', texto[1]);
        lista_tbl_capitulo_gasto_dependencia.push(obj);
    }

    //console.log(JSON.stringify(lista_tbl_capitulo_gasto_dependencia));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(lista_tbl_capitulo_gasto_dependencia),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(JSON.parse(data));

            var IdItem = $("#IdItem").val();
            var IdItem_d = $("#IdItem_d").val();
            var ItemTexto = $("#ItemTexto").val();
            var TipoItem = $("#TipoItem").val();

            for (x = 0; x <= objresponse.length - 1; x++) {
                if (objresponse[x].cod == "success") {
                    var objSuccess = new listaSuccessObj(objresponse[x].msg, objresponse[x].id)
                    listaSuccess.push(objSuccess);
                }
                else {
                    var objError = new listaErrorObj(objresponse[x].msg, objresponse[x].id)
                    ListaError.push(objError);
                }
            }
            if (ListaError.length < 0 || ListaError.length == 0) {
                obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem);
                function style() {
                    for (z = 0; z <= listaSuccess.length - 1; z++) {
                        var iddiv = "div_" + listaSuccess[z].id;
                        var idspan = "#span_" + listaSuccess[z].id;

                        var element = document.getElementById(iddiv);
                        element.classList.add("has-success");
                        $(idspan).html('<i class="fa fa fa-check" title="El monto se almaceno correctamente." style="color:#00a65a;"></i>');
                    }
                }
                var si = eval(style);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
            }
            else {
                obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem);
                function style_alt() {

                    for (z = 0; z <= ListaError.length - 1; z++) {
                        var iddiv = "div_" + ListaError[z].id;
                        var idspan = "#span_" + ListaError[z].id;

                        var element = document.getElementById(iddiv);
                        element.classList.add("has-error");
                        $(idspan).html('<i class="fa fa-times-circle-o" title="' + ListaError[z].msg + '" style="color:#dd4b39;"></i>');
                    }

                    for (y = 0; y <= listaSuccess.length - 1; y++) {
                        var iddiv_scs = "div_" + listaSuccess[y].id;
                        var idspan_scs = "#span_" + listaSuccess[y].id;

                        var element = document.getElementById(iddiv_scs);
                        element.classList.add("has-success");
                        $(idspan_scs).html('<i class="fa fa fa-check" title="El monto se almaceno correctamente." style="color:#00a65a;"></i>');
                    }
                }
                var si_alt = eval(style_alt);
                Aviso_ErrorSAAction("Atención", "La operación presento algunos errores", si_alt);
            }
        },
        error: function () {
            ErrorSA("", "Ocurrio un error.");
        },
        processData: false,
        type: 'PUT',
        url: $("#EndPointAdmon").val() + "Estructura/Update/CapituloG/Dependencia"

    })
}
function Guardar_Montos_dep() {
    Guardar_monto_cg_dep();
}

function Guardar_monto_cg_area() {
    var lista_cg_a = [];
    var lista_tbl_capitulo_gasto_area = [];
    var listaSuccess = [];
    var ListaError = [];

    var tbl_area_id = $('#id_area').val();
    $(".monto_cap").each(function () {
        capitulo_gasto_dependencia_id = "#" + this.id;
        var id = this.id.split('_');
        var monto_asignado = $(capitulo_gasto_dependencia_id).val() == "" ? 0 : $(capitulo_gasto_dependencia_id).val();
        if (monto_asignado != 0) {
            lista_cg_a.push(id[1] + "," + monto_asignado.replace(/,/g, ""));
        }
        else {
            lista_cg_a.push(id[1] + "," + monto_asignado);
        }


    });
    for (x = 0; x <= lista_cg_a.length - 1; x++) {
        texto = lista_cg_a[x].split(",");
        var obj = new tbl_capitulo_gasto_area(1, '00000000-0000-0000-0000-000000000000', texto[0], tbl_area_id, texto[1]);
        lista_tbl_capitulo_gasto_area.push(obj);
    }

    console.log(JSON.stringify(lista_tbl_capitulo_gasto_area));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(lista_tbl_capitulo_gasto_area),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(JSON.parse(data));

            var IdItem = $("#IdItem").val();
            var IdItem_d = $("#IdItem_d").val();
            var ItemTexto = $("#ItemTexto").val();
            var TipoItem = $("#TipoItem").val();

            for (x = 0; x <= objresponse.length - 1; x++) {
                if (objresponse[x].cod == "success") {
                    var objSuccess = new listaSuccessObj(objresponse[x].msg, objresponse[x].id)
                    listaSuccess.push(objSuccess);
                }
                else {
                    var objError = new listaErrorObj(objresponse[x].msg, objresponse[x].id)
                    ListaError.push(objError);
                }
            }
            if (ListaError.length < 0 || ListaError.length == 0) {
                obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem);
                function style() {
                    for (z = 0; z <= listaSuccess.length - 1; z++) {
                        var iddiv = "div_" + listaSuccess[z].id;
                        var idspan = "#span_" + listaSuccess[z].id;

                        var element = document.getElementById(iddiv);
                        element.classList.add("has-success");
                        $(idspan).html('<i class="fa fa fa-check" title="El monto se almaceno correctamente." style="color:#00a65a;"></i>');
                    }
                }
                var si = eval(style);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
            }
            else {
                obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem);
                function style_alt() {

                    for (z = 0; z <= ListaError.length - 1; z++) {
                        var iddiv = "div_" + ListaError[z].id;
                        var idspan = "#span_" + ListaError[z].id;

                        var element = document.getElementById(iddiv);
                        element.classList.add("has-error");
                        $(idspan).html('<i class="fa fa-times-circle-o" title="' + ListaError[z].msg + '" style="color:#dd4b39;"></i>');
                    }

                    for (y = 0; y <= listaSuccess.length - 1; y++) {
                        var iddiv_scs = "div_" + listaSuccess[y].id;
                        var idspan_scs = "#span_" + listaSuccess[y].id;

                        var element = document.getElementById(iddiv_scs);
                        element.classList.add("has-success");
                        $(idspan_scs).html('<i class="fa fa fa-check" title="El monto se almaceno correctamente." style="color:#00a65a;"></i>');
                    }
                }
                var si_alt = eval(style_alt);
                Aviso_ErrorSAAction("Atención", "La operación presento algunos errores", si_alt);
            }
        },
        error: function () {
            ErrorSA("", "Ocurrio un error.");
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAdmon").val() + "Estructura/Add/CapituloG/Area"
    })
}
function Guardar_Montos_area() {
    Guardar_monto_cg_area();
}

function Guardar_monto_cg_subarea() {
    var lista_cg_s = [];
    var lista_tbl_capitulo_gasto_subarea = [];
    var listaSuccess = [];
    var ListaError = [];

    var tbl_subarea_id = $('#id_subarea').val();
    $(".monto_cap").each(function () {
        capitulo_gasto_area_id = "#" + this.id;
        var id = this.id.split('_');
        var monto_asignado = $(capitulo_gasto_area_id).val() == "" ? 0 : $(capitulo_gasto_area_id).val();
        if (monto_asignado != 0) {
            lista_cg_s.push(id[1] + "," + monto_asignado.replace(/,/g, ""));
        }
        else {
            lista_cg_s.push(id[1] + "," + monto_asignado);
        }


    });
    for (x = 0; x <= lista_cg_s.length - 1; x++) {
        texto = lista_cg_s[x].split(",");
        var obj = new tbl_capitulo_gasto_subarea(1, '00000000-0000-0000-0000-000000000000', texto[0], tbl_subarea_id, texto[1]);
        lista_tbl_capitulo_gasto_subarea.push(obj);
    }

    console.log(JSON.stringify(lista_tbl_capitulo_gasto_subarea));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(lista_tbl_capitulo_gasto_subarea),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(JSON.parse(data));

            var IdItem = $("#IdItem").val();
            var IdItem_d = $("#IdItem_d").val();
            var ItemTexto = $("#ItemTexto").val();
            var TipoItem = $("#TipoItem").val();

            for (x = 0; x <= objresponse.length - 1; x++) {
                if (objresponse[x].cod == "success") {
                    var objSuccess = new listaSuccessObj(objresponse[x].msg, objresponse[x].id)
                    listaSuccess.push(objSuccess);
                }
                else {
                    var objError = new listaErrorObj(objresponse[x].msg, objresponse[x].id)
                    ListaError.push(objError);
                }
            }
            if (ListaError.length < 0 || ListaError.length == 0) {
                obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem);
                function style() {
                    for (z = 0; z <= listaSuccess.length - 1; z++) {
                        var iddiv = "div_" + listaSuccess[z].id;
                        var idspan = "#span_" + listaSuccess[z].id;

                        var element = document.getElementById(iddiv);
                        element.classList.add("has-success");
                        $(idspan).html('<i class="fa fa fa-check" title="El monto se almaceno correctamente." style="color:#00a65a;"></i>');
                    }
                }
                var si = eval(style);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
            }
            else {
                obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem);
                function style_alt() {

                    for (z = 0; z <= ListaError.length - 1; z++) {
                        var iddiv = "div_" + ListaError[z].id;
                        var idspan = "#span_" + ListaError[z].id;

                        var element = document.getElementById(iddiv);
                        element.classList.add("has-error");
                        $(idspan).html('<i class="fa fa-times-circle-o" title="' + ListaError[z].msg + '" style="color:#dd4b39;"></i>');
                    }

                    for (y = 0; y <= listaSuccess.length - 1; y++) {
                        var iddiv_scs = "div_" + listaSuccess[y].id;
                        var idspan_scs = "#span_" + listaSuccess[y].id;

                        var element = document.getElementById(iddiv_scs);
                        element.classList.add("has-success");
                        $(idspan_scs).html('<i class="fa fa fa-check" title="El monto se almaceno correctamente." style="color:#00a65a;"></i>');
                    }
                }
                var si_alt = eval(style_alt);
                Aviso_ErrorSAAction("Atención", "La operación presento algunos errores", si_alt);
            }
        },
        error: function () {
            ErrorSA("", "Ocurrio un error.");
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAdmon").val() + "Estructura/Add/CapituloG/Subarea"
    })
}
function Guardar_Montos_subarea() {
    Guardar_monto_cg_subarea();
}

function Guardar_monto_cg_area_sub() {
    var lista_cg_as = [];
    var lista_tbl_capitulo_gasto_area_subordinada = [];
    var listaSuccess = [];
    var ListaError = [];

    var tbl_area_sub_id = $('#id_area_sub').val();
    $(".monto_cap").each(function () {
        capitulo_gasto_subarea_id = "#" + this.id;
        var id = this.id.split('_');
        var monto_asignado = $(capitulo_gasto_subarea_id).val() == "" ? 0 : $(capitulo_gasto_subarea_id).val();
        if (monto_asignado != 0) {
            lista_cg_as.push(id[1] + "," + monto_asignado.replace(/,/g, ""));
        }
        else {
            lista_cg_as.push(id[1] + "," + monto_asignado);
        }


    });
    for (x = 0; x <= lista_cg_as.length - 1; x++) {
        var texto = lista_cg_as[x].split(",");
        var obj = new tbl_capitulo_gasto_area_subordinada(1, '00000000-0000-0000-0000-000000000000', texto[0], tbl_area_sub_id, texto[1]);
        lista_tbl_capitulo_gasto_area_subordinada.push(obj);
    }

    console.log(JSON.stringify(lista_tbl_capitulo_gasto_area_subordinada));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(lista_tbl_capitulo_gasto_area_subordinada),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(JSON.parse(data));

            var IdItem = $("#IdItem").val();
            var IdItem_d = $("#IdItem_d").val();
            var ItemTexto = $("#ItemTexto").val();
            var TipoItem = $("#TipoItem").val();

            for (x = 0; x <= objresponse.length - 1; x++) {
                if (objresponse[x].cod == "success") {
                    var objSuccess = new listaSuccessObj(objresponse[x].msg, objresponse[x].id)
                    listaSuccess.push(objSuccess);
                }
                else {
                    var objError = new listaErrorObj(objresponse[x].msg, objresponse[x].id)
                    ListaError.push(objError);
                }
            }
            if (ListaError.length < 0 || ListaError.length == 0) {
                obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem);
                function style() {
                    for (z = 0; z <= listaSuccess.length - 1; z++) {
                        var iddiv = "div_" + listaSuccess[z].id;
                        var idspan = "#span_" + listaSuccess[z].id;

                        var element = document.getElementById(iddiv);
                        element.classList.add("has-success");
                        $(idspan).html('<i class="fa fa fa-check" title="El monto se almaceno correctamente." style="color:#00a65a;"></i>');
                    }
                }
                var si = eval(style);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
            }
            else {
                obtener_cap_gastos(IdItem, IdItem_d, ItemTexto, TipoItem);
                function style_alt() {

                    for (z = 0; z <= ListaError.length - 1; z++) {
                        var iddiv = "div_" + ListaError[z].id;
                        var idspan = "#span_" + ListaError[z].id;

                        var element = document.getElementById(iddiv);
                        element.classList.add("has-error");
                        $(idspan).html('<i class="fa fa-times-circle-o" title="' + ListaError[z].msg + '" style="color:#dd4b39;"></i>');
                    }

                    for (y = 0; y <= listaSuccess.length - 1; y++) {
                        var iddiv_scs = "div_" + listaSuccess[y].id;
                        var idspan_scs = "#span_" + listaSuccess[y].id;

                        var element = document.getElementById(iddiv_scs);
                        element.classList.add("has-success");
                        $(idspan_scs).html('<i class="fa fa fa-check" title="El monto se almaceno correctamente." style="color:#00a65a;"></i>');
                    }
                }
                var si_alt = eval(style_alt);
                Aviso_ErrorSAAction("Atención", "La operación presento algunos errores", si_alt);
            }
        },
        error: function () {
            ErrorSA("", "Ocurrio un error.");
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAdmon").val() + "Estructura/Add/CapituloG/Subordinada"
    })
}
function Guardar_Montos_area_sub() {
    Guardar_monto_cg_area_sub();
}

/********************Fin de guardar Montos**********************/

function listaSuccessObj(msg, id) {
    this.msg = msg;
    this.id = id;
}
function listaErrorObj(msg, id) {
    this.msg = msg;
    this.id = id;
}

function Ajustar_tamano() {
    var SCRH = ((screen.height / 4) * 3) - 40;
    $('#contenedor_box').height(SCRH);
    $('#body_contenedor').height(SCRH - 68);
}
function monitorear_tamaño() {
    setInterval(function () {
        Ajustar_tamano();
    }, 1000)
}
function formato_moneda() {
    $(".monto_cap").on({
        "focus": function (event) {
            $(event.target).select();
        },
        "keyup": function (event) {
            $(event.target).val(function (index, value) {
                return value.replace(/\D/g, "")
                    .replace(/([0-9])([0-9]{2})$/, '$1.$2')
                    .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
            });
        }
    });
}


function info_asignacion_area(id_cap_gasto, id_item, cap_gasto) {
    $("#lista_info").html("");
    LaunchLoader(true);
    $('#Modal_Info').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Info').modal('show');
    var Monto_total = 0;
    $("#div_descripcion").html("<h5>Cap. gasto: " + cap_gasto.toLowerCase() + " </h5>")

    $.get($("#EndPointAdmon").val() + "Estructura/Get/Lista/Info/1/" + id_cap_gasto + "/" + id_item, function (data, status) {

        var myTable = "<table class='table table-striped table-sm' id='tbl_lista_capitulos_gastos'><tr><td style='width: 60%; text-align: left;'>Área</td>";
        myTable += "<td style='width: 40%; text-align: left;'>Monto asignado</td></tr>";
        for (var y = 0; y <= data.length - 1; y++) {
            var monto = data[y].monto_asignado == 0 ? 0.00 : data[y].monto_asignado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');

            myTable += "<tr><td style='width: 60%; text-align: left;'>" + data[y].item + "</td>";
            myTable += "<td style='width: 40%; text-align: left;'>" + "$ " + monto + "</td>";
            myTable += "</tr>";

            Monto_total += data[y].monto_asignado;
        }
        var monto_t = Monto_total == 0 ? 0.00 : Monto_total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        myTable += "<tr><td colspan='1' style='width: 60%; text-align: left;'></td> <td style='width: 40%; text-align: left;'></td></tr>"
        myTable += "<tr><td colspan='1' style='width: 60%; text-align: left;'>Monto total repartido</td> <td style='width: 40%; text-align: left;'>" + "$ " + monto_t + "</td></tr>"
        myTable += "</table>";
        $("#lista_info").html(myTable);
        LaunchLoader(false);
    });

}
function info_asignacion_subarea(id_cap_gasto, id_item, cap_gasto) {
    $("#lista_info").html("");
    LaunchLoader(true);
    $('#Modal_Info').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Info').modal('show');
    var Monto_total = 0;
    $("#div_descripcion").html("<h5>Cap. gasto: " + cap_gasto.toLowerCase() + " </h5>")

    $.get($("#EndPointAdmon").val() + "Estructura/Get/Lista/Info/2/" + id_cap_gasto + "/" + id_item, function (data, status) {

        var myTable = "<table class='table table-striped table-sm' id='tbl_lista_capitulos_gastos'><tr><td style='width: 60%; text-align: left;'>Subárea</td>";
        myTable += "<td style='width: 40%; text-align: left;'>Monto asignado</td></tr>";
        for (var y = 0; y <= data.length - 1; y++) {
            var monto = data[y].monto_asignado == 0 ? 0.00 : data[y].monto_asignado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');

            myTable += "<tr><td style='width: 60%; text-align: left;'>" + data[y].item + "</td>";
            myTable += "<td style='width: 40%; text-align: left;'>" + "$ " + monto + "</td>";
            myTable += "</tr>";

            Monto_total += data[y].monto_asignado;
        }
        var monto_t = Monto_total == 0 ? 0.00 : Monto_total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        myTable += "<tr><td colspan='1' style='width: 60%; text-align: left;'></td> <td style='width: 40%; text-align: left;'></td></tr>"
        myTable += "<tr><td colspan='1' style='width: 60%; text-align: left;'>Monto total repartido</td> <td style='width: 40%; text-align: left;'>" + "$ " + monto_t + "</td></tr>"
        myTable += "</table>";
        $("#lista_info").html(myTable);
        LaunchLoader(false);
    });
}
function info_asignacion_subordinado(id_cap_gasto, id_item, cap_gasto) {
    $("#lista_info").html("");
    LaunchLoader(true);
    $('#Modal_Info').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Info').modal('show');
    var Monto_total = 0;
    $("#div_descripcion").html("<h5>Cap. gasto: " + cap_gasto.toLowerCase() + " </h5>")

    $.get($("#EndPointAdmon").val() + "Estructura/Get/Lista/Info/3/" + id_cap_gasto + "/" + id_item, function (data, status) {

        var myTable = "<table class='table table-striped table-sm' id='tbl_lista_capitulos_gastos'><tr><td style='width: 60%; text-align: left;'>Subárea</td>";
        myTable += "<td style='width: 40%; text-align: left;'>Monto asignado</td></tr>";
        for (var y = 0; y <= data.length - 1; y++) {
            var monto = data[y].monto_asignado == 0 ? 0.00 : data[y].monto_asignado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');

            myTable += "<tr><td style='width: 60%; text-align: left;'>" + data[y].item + "</td>";
            myTable += "<td style='width: 40%; text-align: left;'>" + "$ " + monto + "</td>";
            myTable += "</tr>";

            Monto_total += data[y].monto_asignado;
        }
        var monto_t = Monto_total == 0 ? 0.00 : Monto_total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        myTable += "<tr><td colspan='1' style='width: 60%; text-align: left;'></td> <td style='width: 40%; text-align: left;'></td></tr>"
        myTable += "<tr><td colspan='1' style='width: 60%; text-align: left;'>Monto total repartido</td> <td style='width: 40%; text-align: left;'>" + "$ " + monto_t + "</td></tr>"
        myTable += "</table>";
        $("#lista_info").html(myTable);
        LaunchLoader(false);
    });
}