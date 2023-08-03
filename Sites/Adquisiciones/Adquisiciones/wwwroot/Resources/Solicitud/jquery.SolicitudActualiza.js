$(".btnGoHome").click(function () {
	window.location.href = "/Bandeja";
});
$(".btnDoOkAprobar").click(function () {
    Update('ACCOS');
}); 
$(".btnDoOk").click(function () {
	UpdateComplete();
}); 

$(".btnDoRechazo").click(function () {
    Update('RECHA');
});  

function GoBandeja() {
	window.location.href = "/Bandeja";
}

var json_pres_act = []

$(function () {
	LaunchLoader(true);
	$('#txt_FechaSolicitud').datetimepicker({
		format: 'YYYY-MM-DD',
		locale: 'es'
	});
	GetDocumentos();
	//getSolicitud();
	getdrop_tipo_docto();
	
		
	$('#tbl_adjuntos').DataTable({
		"language": {
			"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
		},
		"pageLength": 5,
		"lengthMenu": [5]
	});
	
	setTimeout(function () {
		LaunchLoader(false);
	}, 1000)
});

var con = $('#EndPointAQ').val() + "SerSolicitud/";

function getdrop_tipo_docto() {
	var instancia = $('#HDidInstancia').val();
	//$.get($('#EndPointAQ').val() + "SerSolicitud/Get_lista_tipo_documento/" + instancia,	
	$.get(con + "Get_lista_tipo_documento/" + instancia,function (data, status) {
			$('#tipo_dcto').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].id + "'>" + data[i].tipo_documento + "</option>";
				$('#tipo_dcto').append(item);
			}
	});	
}

function GetDocumentos() {	
	$.get(con + "Get_Documentos_Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
		var Arreglo_arreglosdot = [];
		for (var i = 0; i <= data.length - 1; i++) {
			var InternoArr = [];
			InternoArr.push(data[i].nombre_documento);
			InternoArr.push(data[i].tipo_documento);			
			InternoArr.push("<button class='btn btn-primary' onclick=\"ViewDocto('" + data[i].token + "');\"><span class='glyphicon glyphicon-eye-open'></span></button> <button class='btn btn-danger del_archivo' id='del_archivo' onclick=\"del_archivo('" + data[i].id + "');\"><span class='glyphicon glyphicon-trash'></span></button>");
			Arreglo_arreglosdot.push(InternoArr);
		}

		var table = $('#tbl_adjuntos').DataTable();

		table.destroy();

		$('#tbl_adjuntos').DataTable({
			"language": {
				"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
			},
			"pageLength": 5,
			"lengthMenu": [5],
			data: Arreglo_arreglosdot,
			columns: [
				{ title: "Nom. documento" },
				{ title: "Tipo de documento" },
				{ title: "Acción" }
			],
			columnDefs: [
				{
					targets: -1,
					className: 'dt-body-center'
				}]
		});
		getSolicitud();
	});
}

function ViewDocto(token_) {
	var RES_ = '';
	var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/10";
	//alert(Uri);
	var URIENC = '';
	$.get(Uri, function (data, status) {
		//alert(data);
		URIENC = data;
		RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
		var SCRH = ((screen.height / 4) * 3) - 40;
		$('#viewer_window_iframe').css('height', SCRH);
		$('#viewer_window_iframe').attr('src', RES_);
		$('#viewer_window').modal('show');
		return RES_;
	});

}

$("#add").click(function () {
	LaunchLoader(true);
	if ($('#carga_docts').val() == '') {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'Cargue un documento'
		})
		LaunchLoader(false);
		return;
	}
	else if ($('#tipo_dcto').val() == null) {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'Seleccione tipo de documento'
		})
		LaunchLoader(false);
		return;
	}

	upload_file_tobd();
});

function upload_file_tobd() {
	var form_data_file = new FormData();
	var file_data = $('#carga_docts').prop('files')[0];
	var tipo_docto = $('#tipo_dcto').val();
	var nom_docto = file_data.name;
	form_data_file.append('file', file_data);
	$.ajax({
		url: $("#EndPointFileAQ").val() + 'Upload/',
		dataType: 'text',
		cache: false,
		contentType: false,
		processData: false,
		data: form_data_file,
		type: 'POST',
		async: false,
		success: function (data) {
			var token = data.replace(/[ '"]+/g, '');
			archivos_to_bd(token, tipo_docto, nom_docto);

		},
		error: function (data) {
			var objresponse = JSON.parse(data);
			LaunchLoader(false);
			ErrorSA('', objresponse);
		}
	});
}

function archivos_to_bd(token, tipo_docto, nom_docto) {

	var OBJ_Form = {};
	OBJ_Form.id = null;
	OBJ_Form.token = token;
	OBJ_Form.tbl_tipo_documento_id = tipo_docto;
	OBJ_Form.tbl_solicitud_id = $('#_SOLICITUD').val();
	OBJ_Form.nom_documento = nom_docto;

	$.ajax({
		dataType: 'text',
		cache: false,
		contentType: 'application/json',
		processData: false,
		data: JSON.stringify(OBJ_Form),
		type: 'post',
		//url: ($('#EndPointAQ').val() + "SerSolicitud/Add_dcto_adj")
		url: (con + "Add_dcto_adj"),
		success: function (data) {
			$('#carga_docts').val('');
			$('#tipo_dcto').val('');
			LaunchLoader(false);
			GetDocumentos();
		},
		error: function (data) {
			var objresponse = JSON.parse(data);
			ErrorSA('', objresponse);
			LaunchLoader(false);
		}

	})
}

function del_archivo(id) {
	function Confirmacion() {
		return del_file_tobd(id);
	}
	var AccionSi = eval(Confirmacion);
	function Negacion() {
		return;
	}
	var AccionNo = eval(Negacion)
	QuestionSA('¡Usted está a punto de eliminar un registro permanentemente...!', '¿En verdad desea continuar? ', 'Si, Continuar', 'No, Cancelar', AccionSi, AccionNo)
}
function del_file_tobd(id) {
	$.ajax({
		dataType: 'text',
		cache: false,
		contentType: 'application/json',
		processData: false,
		type: 'post',
		//url: ($('#EndPointAQ').val() + "SerSolicitud/Add_dcto_adj")
		url: (con + "Delete_dcto_adj/" + id ),
		success: function (data) {
			var data_b = $.parseJSON(data);
			var objresponse = JSON.parse(data);
			if (!objresponse.Bit) {
				SuccessSA("Operación exitosa", data_b[0].msg);
				GetDocumentos();
			}
			else {
				ErrorSA("", objresponse.Descripcion);
			}			
		},
		error: function (data) {
			var objresponse = JSON.parse(data);
			ErrorSA('', objresponse);
			LaunchLoader(false);
		}
	})
}

$(".ModalPress").click(function () {
	if ($('#drop_dependencia').val() == ('' || null)) {
		Swal.fire({
			type: 'error',
			title: 'Debé seleccionar una dependencia'
		});
		return;
	}
	else {
		$('#HD_Dep_Id').val($('#drop_dependencia').val());
		$('#ModalPresupuestos').modal({ backdrop: 'static', keyboard: false });
		$('#ModalPresupuestos').modal('show');

		INIT_Areas(json_pres_act);
	}
});

function UpdateComplete() {


	var SolicitudIntancia = SolicitudClass;

	SolicitudIntancia.p_opt = 3;
	SolicitudIntancia.p_id = $('#txt_id').val();
	SolicitudIntancia.p_num_solicitud = $('#txt_numsol').val();
	SolicitudIntancia.p_tbl_tipo_solicitud_id = $('#drop_TipoSolicitud').val();
	SolicitudIntancia.p_tbl_tipo_contrato_id = $('#drop_TipoContrato').val();
	SolicitudIntancia.p_fecha_solicitud = $('#txt_FechaSolicitud').val();
	SolicitudIntancia.p_elaboro = $('#txt_Elaboro_hd').val();
	SolicitudIntancia.p_tbl_dependencia_id = $('#drop_dependencia').val();
	SolicitudIntancia.p_tbl_area_id = $('#drop_area').val();
	SolicitudIntancia.p_tbl_proyecto_id = $('#drop_Proyecto').val();
	SolicitudIntancia.p_descripcion = $('#txt_descripcion').val();
	SolicitudIntancia.p_monto_solicitud = $('#txt_montosolicitud').val();
	SolicitudIntancia.p_monto_autorizado = $('#txt_MA').val();
	SolicitudIntancia.p_comentarios = $('#txt_Comen').val();
	SolicitudIntancia.p_tbl_estatus_solicitud_id = $('#sigla_estatus').val();
	SolicitudIntancia.p_inclusion = $('#txt_FechaSolicitud').val();
	SolicitudIntancia.p_token_autorizacion = "00000000-0000-0000-0000-000000000000";
	SolicitudIntancia.p_token_solicitante = "00000000-0000-0000-0000-000000000000";
	SolicitudIntancia.p_json_pres = JSON.stringify(json_pres_act);
	SolicitudIntancia.p_nombre_bien_servicio = $('#txt_nom_bien_servicio').val();
	var i = $('#requiere_visita_s').is(":checked") ? 1 : 0;
	var f = $('#requiere_mesa_val').is(":checked") ? 1 : 0;
	SolicitudIntancia.p_visitasitio = i;
	SolicitudIntancia.p_mesa_validacion = f;

	

	//original
	//var SolicitudIntancia = SolicitudClass;
	//SolicitudClass.NumSolicitud = $('#txt_numsol').val();
	//SolicitudClass.TipoSolicitud = $('#drop_TipoSolicitud').val();
	//SolicitudClass.TipoContrato = $('#drop_TipoContrato').val();
	//SolicitudClass.fechaSolicitud = $('#txt_FechaSolicitud').val();
	//SolicitudClass.Elaboro = $('#txt_Elaboro_hd').val();
	//SolicitudClass.TBLENT_DEPENDENCIA_id = $('#drop_dependencia').val();
	//SolicitudClass.TBLENT_AREA_id = $('#drop_area').val();
	//SolicitudClass.TBLENT_PROYECTO_id = $('#drop_Proyecto').val();
	//SolicitudClass.descripcion = $('#txt_descripcion').val();
	//SolicitudClass.MontoSolicitud = $('#txt_montosolicitud').val();
	//SolicitudClass.MontoAutorizado = $('#txt_MA').val();
	//SolicitudClass.Comentarios = $('#txt_Comen').val();
	//SolicitudClass.tbl_estatus_solicitud_id = 4;
	//SolicitudClass.inclusion = $('#txt_FechaSolicitud').val();
	//SolicitudClass.TokenAutorizacion = $('#txt_TokenA').val();
	//SolicitudClass.TokenSolicitante = $('#txt_TokenS').val();
	//SolicitudClass.id = $('#txt_id').val();

	var Val = ValidaObject(SolicitudIntancia);
	if (!Val.Bit) {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: Val.Texto
		});
		return;
	}

	console.log(JSON.stringify(SolicitudIntancia));
	var form_data = new FormData();
	form_data.append('SolicitudObject', JSON.stringify(SolicitudIntancia));


	$.ajax({
		dataType: 'text',  // what to expect back from the PHP script, if anything
		cache: false,
		contentType: false,
		processData: false,
		data: form_data,
        type: 'post',

        success: function (data) {
			var objresponse = JSON.parse(data);
			if (objresponse != null) {

                Swal.fire({
                    type: 'success',
                    title: 'Solicitud Guardada',
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "/Bandeja";
                    }
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Error al guardar la solicitud'
            })
        },
        processData: false,
        type: 'POST',
		url: $('#EndPointAQ').val() + "SerSolicitud/Update"
		//url: (con + "solicitud/Update")
	});

}

function Update(Estatus) {

	if ($('#txt_comentarios').val() == '') {
		Swal.fire({ type: 'error', title: 'Hay un error en los datos de entrada', text: 'El campo "Comentarios" está vacío.' });
		return;
	}
	if ($('#txt_montoautorizado').val() == '') {
		Swal.fire({ type: 'error', title: 'Hay un error en los datos de entrada', text: 'El campo "Monto Autorizado" está vacío.' });
		return
    }
    //if ($('#FileAutorizado').prop('files')[0] == null) {
    //    Swal.fire({ type: 'error', title: 'Hay un error en los datos de entrada', text: 'Seleccione un archivo' });
    //    return
    //}
	//var file_data = $('#FileAutorizado').prop('files')[0]; 
    //var form_data_file = new FormData();
    var form_data = SolicitudClass;

    //form_data_file.append('file', file_data);

    form_data.p_opt = 5;
    form_data.p_id = $('#txt_id').val();
    form_data.p_fecha_solicitud = "0001-01-01T00:00:00";
    form_data.p_tbl_dependencia_id = "00000000-0000-0000-0000-000000000000";
    form_data.p_tbl_area_id = "00000000-0000-0000-0000-000000000000";
    form_data.p_tbl_proyecto_id = "00000000-0000-0000-0000-000000000000";
	form_data.p_token_autorizacion = "00000000-0000-0000-0000-000000000000";
    form_data.p_monto_solicitud = 0.0;
    form_data.p_monto_autorizado = $('#txt_montoautorizado').val(); 

    if (ValidaCadena($('#txt_comentarios').val(), 'Comentario') == '') {
        form_data.p_comentarios = $('#txt_comentarios').val();
    }
    else {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Comentario" no puede contener caracteres especiales'
        })
        return;
    }

    //form_data.p_token_autorizacion = 'asijdaiojsiodjasojadosm';

    //$.ajax({
    //    url: $("#EndPointFileAQ").val() + 'Upload/',
    //    //url: 'http://10.2.15.40:6509/Files/Upload',
    //    dataType: 'text',
    //    cache: false,
    //    contentType: false,
    //    processData: false,
    //    data: form_data_file,
    //    type: 'POST',
    //    async: false,
    //    success: function (data) {
    //        var token = data.replace(/[ '"]+/g, '');
    //        form_data.p_token_autorizacion = token;

    //    },
    //    error: function (data) {
    //        var objresponse = JSON.parse(data);
    //        ErrorSA('', objresponse);
    //    }
    //});

    form_data.p_tbl_estatus_solicitud_id = Estatus;
	form_data.p_inclusion = "0001-01-01T00:00:00";
	form_data.p_json_pres = JSON.stringify(json_pres_act);
	$.ajax({

        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(form_data),
        type: 'put',

        success: function (data) {
            if (!data.Bit) {
                Swal.fire({
                    type: 'success',
                    title: 'Solicitud Guardada',
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "/Bandeja";
                    }
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Error al guardar la solicitud'
            })
        },
        url: $('#EndPointAQ').val() + "SerSolicitud/Update/AprRch" 
        //url: con + "solicitud/Update/AprRch" 
	});
 
}

$("#drop_dependencia").change(function () {
	var Instancia = $('#HDidInstancia').val();
	var Dependencia = $('#drop_dependencia').val();
	//Cargamos Areas
	$.get($('#EndPointAQ').val() + "SerAreas/Get/Dropdown/" + Dependencia,
		function (data, status) {
			$('#drop_area').html('<option value="">Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_area').append(item);
			}
		});
	//Cargamos proyectos
	$.get($('#EndPointAQ').val() + "SerProyectos/Get/Dropdown/" + Dependencia,
		function (data, status) {
			$('#drop_Proyecto').html('<option  value="">Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_Proyecto').append(item);
			}
		});
});

 
function ValidaObject(obj) {
	var Validaciones = true;
	var Response = '';
	/*  NumSolicitud */
	Validaciones = SolicitudClass.p_num_solicitud != '';
	if (!Validaciones) {
		Response = 'Debe ingresa un Número de Solicitud';
		return { Texto: Response, Bit: false }
	}
	else if (Validaciones) {
		if (ValidaCadena(SolicitudClass.p_num_solicitud, 'Solicitud') != '') {
			Swal.fire({
				type: 'error',
				title: 'Hay un error en los datos de entrada',
				text: 'El campo "Solicitud" no puede contener caracteres especiales'
			})
			return;
		}
	}
	/*  Tipo Solicitud */
	Validaciones = SolicitudClass.p_tbl_tipo_solicitud_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar un tipo de solicitud';
		return { Texto: Response, Bit: false }
	}
	/*  Tipo Contrato */
	Validaciones = SolicitudClass.p_tbl_tipo_contrato_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar un tipo de contrato';
		return { Texto: Response, Bit: false }
	}
	/*  Fecha Solicitud */
	Validaciones = SolicitudClass.p_fecha_solicitud != '';
	if (!Validaciones) {
		Response = 'Fecha de solictud invalida';
		return { Texto: Response, Bit: false }
	}
	/*  Elaboro */
	Validaciones = SolicitudClass.p_elaboro != '';
	if (!Validaciones) {
		Response = 'El campo "Elaboró" no puede ser vacío';
		return { Texto: Response, Bit: false }
	}
	else if (Validaciones) {
		if (ValidaCadena(SolicitudClass.p_elaboro, 'Elaboró') != '') {
			Swal.fire({
				type: 'error',
				title: 'Hay un error en los datos de entrada',
				text: 'El campo "Elaboró" no puede contener caracteres especiales'
			})
			return;
		}
	}
	/*  Dependencia */
	Validaciones = SolicitudClass.p_tbl_dependencia_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar una dependencia';
		return { Texto: Response, Bit: false }
	}
	/*  Area */
	Validaciones = SolicitudClass.p_tbl_area_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar una Area';
		return { Texto: Response, Bit: false }
	}
	/*  Proyecto */
	Validaciones = SolicitudClass.p_tbl_proyecto_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar un Proyecto';
		return { Texto: Response, Bit: false }
	}
	/*  descripción */
	Validaciones = SolicitudClass.p_descripcion != '';
	if (!Validaciones) {
		Response = 'El campo "Descripción" no puede ser vacío';
		return { Texto: Response, Bit: false }
	}
	//var Documento = $('#FileSolicitante').val();
	//Validaciones = Documento != '';
	//if (!Validaciones) {
	//	Swal.fire({
	//		type: 'error',
	//		title: 'Hay un error en los datos de entrada',
	//		text: 'Debe seleccionar un documento'
	//	})
	//	return;
	//}
	else if (Validaciones) {
		if (ValidaCadena(SolicitudClass.p_descripcion, 'Descripción') != '') {
			Swal.fire({
				type: 'error',
				title: 'Hay un error en los datos de entrada',
				text: 'El campo "Descripción" no puede contener caracteres especiales'
			})
			return;
		}
	}
	/*  monto solicitud */
	Validaciones = SolicitudClass.p_monto_solicitud != '';
	if (!Validaciones) {
		Response = 'El campo "Monto Solicitud" no puede ser vacío';
		return { Texto: Response, Bit: false }
	}

	if (Validaciones) {

		if (ValidaCadena(SolicitudClass.p_num_solicitud, 'Solicitud') != '') {
			Swal.fire({
				type: 'error',
				title: 'Hay un error en los datos de entrada',
				text: 'El campo "Solicitud" no puede contener caracteres especiales'
			})
			return;
		}
		if (ValidaCadena(SolicitudClass.p_elaboro, 'Elaboro') != '') {
			Swal.fire({
				type: 'error',
				title: 'Hay un error en los datos de entrada',
				text: 'El campo "Elaboro" no puede contener caracteres especiales'
			})
			return;
		}
		if (ValidaCadena(SolicitudClass.p_descripcion, 'Solicitud') != '') {
			Swal.fire({
				type: 'error',
				title: 'Hay un error en los datos de entrada',
				text: 'El campo "Descripcion" no puede contener caracteres especiales'
			})
			return;
		}

		return { Texto: '', Bit: true }
	}

}




//Microservicios nuevos

function getSolicitud() {
	var IdSolicitud = $('#_SOLICITUD').val();

	$.get($('#EndPointAQ').val() + "SerSolicitud/Get/" + IdSolicitud,
	//$.get("https://localhost:44359/solicitud/Get/" + IdSolicitud,
		function (data, status) {

			getDependencias(data.tbl_dependencia_id, data.tbl_area_id);
			getTipoSolicitud(data.tbl_tipo_solicitud_id);
			getTipoContratoSolicitud(data.tbl_tipo_contrato_id);
			getProyectos(data.tbl_dependencia_id, data.tbl_proyecto_id);

			$('#txt_numsol').val(data.num_solicitud);
			//$('#drop_TipoSolicitud').val(data.tbl_tipo_solicitud_id);
			//$('#drop_TipoContrato').val(data.tbl_tipo_contrato_id);
            f = data.fecha_solicitud.split('T');
			$('#txt_FechaSolicitud').val(f[0]);
			$('#txt_elaboro').val(data.elaboro);

			//$('#drop_area').val(data.tbl_area_id);

			$('#txt_descripcion').val(data.descripcion);
			$('#txt_montosolicitud').val(data.monto_solicitud);
			$('#txt_montoautorizado').val(data.monto_autorizado);
			$('#txt_comentarios').val(data.comentarios);


			$('#sigla_estatus').val(data.sigla_estatus_solicitud);
			$('#txt_id').val(data.id);
			$('#txt_Elaboro_hd').val(data.elaboro);
			$('#txt_Comen').val(data.comentarios);
			$('#txt_TokenA').val(data.token_autorizacion);
			$('#txt_TokenS').val(data.token_solicitante);
            $('#txt_solicitante').val(data.token_solicitante);
			$('#txt_MA').val(data.monto_autorizado);
			$('#txt_nom_bien_servicio').val(data.nombre_bien_servicio);
			$('#txt_comentarios_suf').val(data.comentario_suficiencia);
			if (data.visita_sitio == true) {
				$('#requiere_visita_s').prop("checked", true);
			} else if (data.visita_sitio == false) {
				$('#requiere_visita_s').prop("checked", false);
			}
			if (data.mesa_validacion == true) {
				$('#requiere_mesa_val').prop("checked", true);
			} else if (data.mesa_validacion == false) {
				$('#requiere_mesa_val').prop("checked", false);
			}
			if (data.docum_completa == true) {
				$('#chkbx_doc_compl').prop("checked", true);
				$('#solic_compl').hide();
			} else if (data.docum_completa == false) {
				$('#chkbx_doc_compl').prop("checked", false);
			}
			if (data.requiere_dictamen == true) {
				$('#chkbx_req_dict').prop("checked", true);
			} else if (data.requiere_dictamen == false) {
				$('#chkbx_req_dict').prop("checked", false);
			}

			json_pres_act = JSON.parse(data.json_pres);

			if ((data.token_autorizacion != null) && (data.token_autorizacion != '') && (data.token_autorizacion != '00000000-0000-0000-0000-000000000000')) {

				$('#file_autorizacion').click(function () {
					getURL(data.token_autorizacion);

				});
				$('#file_autorizacion').show();
			}



			//Control de botones 



			if (data.sigla_estatus_solicitud == 'COYJU' || data.sigla_estatus_solicitud == 'COMPL' || data.sigla_estatus_solicitud == 'SOLIN') {
				$("input, textarea, select").each(function (indice, elemento) {
					$(elemento).prop('disabled', false);
				});
				$('#txt_montoautorizado').prop('disabled', true);
				$('#txt_comentarios').prop('disabled', true);
				$('#txt_comentarios_suf').prop('disabled', true);
				$('#FileAutorizado').prop('disabled', true);
				$('#txt_elaboro').prop('disabled', true);
				$('#txt_solicitante').prop('disabled', true);
				$(".btnDoRechazo, .btnDoOkAprobar").hide();

			} else if (data.sigla_estatus_solicitud == 'APROB') {
				$(".btnDoOk").hide();

			} else if (data.sigla_estatus_solicitud == 'ACCOS' || data.sigla_estatus_solicitud == 'RECHA') {
                $(".btnDoRechazo, .btnDoOkAprobar, .btnDoOk").hide();                
				//$("#btn_primer_set").hide();                
                $("input, textarea, select").each(function (indice, elemento) {
                    $(elemento).prop('disabled', true);
				});
				$('#txt_solicitante').prop('disabled', true);
				$("#add").prop('disabled', true);
				$(".del_archivo").prop('disabled', true);
                $(".back").show();
            }

            if (location.href.toLowerCase().indexOf('solicitud/complementa') > 0) {
                $("input, textarea, select").each(function (indice, elemento) {
                    $(elemento).prop('disabled', false);
                });
                $('#txt_montoautorizado').prop('disabled', true);
                $('#txt_comentarios').prop('disabled', true);
				$('#txt_comentarios_suf').prop('disabled', true);
                $('#FileAutorizado').prop('disabled', true);
				$('#txt_elaboro').prop('disabled', true);
				$('#txt_solicitante').prop('disabled', true);
				$('#txt_montosolicitud').prop('disabled', true);
                $(".btnDoOk, .btnGoHome").show();
				$(".back").hide();
                if (data.sigla_estatus_solicitud == 'REGIS') {
                    $(".btnDoOkAprobar, .btnDoRechazo").hide();
                    $("#sol_suf").show();
                } else if (data.sigla_estatus_solicitud == 'SOLIN') {
                    $("#env_a_int_prec").show();
                } else if (data.sigla_estatus_solicitud == 'SFRCH') {
                    $("#sol_suf").show();
                    $(".btnDoOkAprobar, .btnDoRechazo").hide();
                }
            }
			if (location.href.toLowerCase().indexOf('solicitud/detalle') > 0) {
				$("input, textarea, select, checkbox").each(function (indice, elemento) {
					$(elemento).prop('disabled', true);
				});
				$(".btnDoRechazo, .btnDoOkAprobar, .btnDoOk").hide();
				$(".del_archivo").prop('disabled', true);
				$("#add").prop('disabled', true);
				$("#sol_est_merc").hide();

				if (data.sigla_estatus_solicitud == 'EMENV' && $("#HDidRol").val() == '970593e1-a5ed-11ea-8a36-00155d1b3502') {
					$("#btn_primer_set").hide();
					$("#en_validacion_chkbx, #btn_validacion").show();
					$("#chkbx_doc_compl, #chkbx_req_dict").prop('disabled', false);
                }
    //            else if (data.sigla_estatus_solicitud == 'SFRCH') {
				//	$("#sol_suf").show();
				//}
			}
		});
}



function getDependencias(IdDepSelected, IdAreaSelected) {
	var instancia = $('#HDidInstancia').val();

	$.get($('#EndPointAQ').val() + "SerDependencia/Get/Dropdown/" + instancia,
		function (data, status) {
			$('#drop_dependencia').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_dependencia').append(item);				
			}
			
			//var Dependencia = $('#drop_dependencia').val();
			//Cargamos Areas
			$.get($('#EndPointAQ').val() + "SerAreas/Get/Dropdown/" + IdDepSelected,
				function (data, status) {
					$('#drop_area').html('<option value="">Seleccione... </option>');
					for (var i = 0; i <= data.length - 1; i++) {
						var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
						$('#drop_area').append(item);
					}		
					$('#drop_area').val(IdAreaSelected);
					$('#drop_dependencia').val(IdDepSelected);
				});
			
		});

	
	
}


function getTipoSolicitud(IdSelected) {

	$.get($('#EndPointAQ').val() + "SerSolicitud/Get/TipoSolicitud/Dropdown/",
		function (data, status) {
			$('#drop_TipoSolicitud').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_TipoSolicitud').append(item);
				
			}
			$('#drop_TipoSolicitud').val(IdSelected);
		});

}


function getTipoContratoSolicitud(IdSelected) {
	var instancia = $('#HDidInstancia').val();

	$.get($('#EndPointAQ').val() + "SerSolicitud/Get/TipoContratoSolicitud/Dropdown/" + instancia,
		function (data, status) {
			$('#drop_TipoContrato').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_TipoContrato').append(item);
			}
			$('#drop_TipoContrato').val(IdSelected);
		});

}

function getProyectos(Dependencia,IdSelected) {

	
	//Cargamos proyectos
	$.get($('#EndPointAQ').val() + "SerProyectos/Get/Dropdown/" + Dependencia,
		function (data, status) {
			$('#drop_Proyecto').html('<option  value="">Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_Proyecto').append(item);
			}
			$('#drop_Proyecto').val(IdSelected);
		});

	

}

//ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
$("#sol_suf").click(function () {
	Swal.fire({
		type: 'question',
		title: '¿Solicitar suficiencia?',
		showCancelButton: true,
		cancelButtonColor: '#d33',
		cancelButtonText: 'Cancelar',
		confirmButtonText: 'Solicitar',
		confirmButtonClass: 'btn btn-success'
	}).then((result) => {
		if (result.value) {
			add_sufuciencia('SFCSL', '');
		}
	})
});

$("#sol_est_merc").click(function () {  // falta complementar
	Swal.fire({
		type: 'question',
		title: '¿Solicitar estudio de mercado?',
		confirmButtonText: 'Solicitar',
		confirmButtonClass: 'btn btn-success btn-lg'
	}).then((result) => {
		if (result.value) {
			solic_suficiencia('estmerc');
		}
	})
});


function getURL(token_) {
	var RES_ = '';
	var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/10";
	//alert(Uri);
	var URIENC = '';
	$.get(Uri, function (data, status) {
		//alert(data);
		URIENC = data;
		RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
		var SCRH = ((screen.height / 4) * 3) - 40;
		$('#viewer_window_iframe').css('height', SCRH);
		$('#viewer_window_iframe').attr('src', RES_);
		modalVisualizacion();
		return RES_;
	});

}

function modalVisualizacion() {
	$('#viewer_window').modal('show');
}

$('#chkbx_doc_compl').change(function () {
	LaunchLoader(true);
	var doc_comp = $('#chkbx_doc_compl').is(":checked") ? 1 : 0;
	if (doc_comp == 1) {
		//console.log('docum_compl_sol');
		updatesolic_valid('docum_compl_sol', $('#_SOLICITUD').val(), 'null');
	} else if (doc_comp == 0) {
		//console.log('docum_no_compl_sol');
		updatesolic_valid('no_docum_compl_sol', $('#_SOLICITUD').val(), 'null');
    }
});
$('#chkbx_req_dict').change(function () {
	LaunchLoader(true);
	var req_dict = $('#chkbx_req_dict').is(":checked") ? 1 : 0;
	if (req_dict == 1) {
		//console.log('req_dict_sol');
		updatesolic_valid('req_dict_sol', $('#_SOLICITUD').val(), 'null');
	} else if (req_dict == 0) {
		//console.log('no_req_dict_sol');
		updatesolic_valid('no_req_dict_sol', $('#_SOLICITUD').val(),'null');
	}
});


$("#env_a_int_prec").click(function () {
	Swal.fire({
		type: 'question',
		title: 'Enviar a Integración a Precios?',
		confirmButtonText: 'Enviar',
		confirmButtonClass: 'btn btn-success btn-lg'
	}).then((result) => {
		if (result.value) {
			updatesolic_valid('Env_int_prec_valid', $('#_SOLICITUD').val(),'null');
		}
	})
});
function updatesolic_valid(parametro, id_sol, variable) {

	// para actualizar solicitud
	$.ajax({
		url: (con + "update_sol_metodo/" + parametro + "/" + id_sol + "/" + variable),
		dataType: 'text',
		cache: false,
		contentType: false,
		processData: false,
		data: new FormData(),
		type: 'POST',
		async: false,
		success: function (data) {
			LaunchLoader(false);
			var objresponse = JSON.parse(data);
			if (objresponse != null) {
				Swal.fire({
					type: 'success',
					title: 'Solicitud actualizada',
				}).then(function (isConfirm) {
					if (isConfirm) {
						window.location.href = "/Bandeja";
					}
				});
			}
		},
		error: function (data) {
			var objresponse = JSON.parse(data);
			ErrorSA('', objresponse);
		}
	});
}

$("#solic_compl").click(function () {	
	$('#SolicitarComplememnto').modal('show');
	$('#descr_REM_SC').prop('disabled', false);
}); 
$("#ENV_SC").click(function () {
	LaunchLoader(true);
	if ($('#descr_REM_SC').val() == '') {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'Ingrese un comentario'
		})
		LaunchLoader(false);
		return;
	}
	updatesolic_valid('Solic_Complem_a_solic', $('#_SOLICITUD').val(), $('#descr_REM_SC').val());
}); 