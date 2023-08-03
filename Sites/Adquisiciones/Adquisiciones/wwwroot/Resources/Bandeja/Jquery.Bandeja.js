$(document).ready(function () {
	$('#ComplementarSol, #PropoTec').DataTable();

	$(".eachTbl").each(function (indice, elemento) {
		$(elemento).DataTable();
	});

});

function openCity(evt, cityName) {
	// Declare all variables
	var i, tabcontent, tablinks;

	// Get all elements with class="tabcontent" and hide them
	tabcontent = document.getElementsByClassName("tabcontent");
	for (i = 0; i < tabcontent.length; i++) {
		tabcontent[i].style.display = "none";
	}

	// Get all elements with class="tablinks" and remove the class "active"
	tablinks = document.getElementsByClassName("tablinks");
	for (i = 0; i < tablinks.length; i++) {
		tablinks[i].className = tablinks[i].className.replace(" active", "");
	}

	// Show the current tab, and add an "active" class to the link that opened the tab
	document.getElementById(cityName).style.display = "block";
	evt.currentTarget.className += " active";
	Redimension();
}

///////////////////////   INICIO   scripts unicamente para la bandeja "Justificación"
function LaunchJust(input, num, link) {

	$('#txt_solicitud_hd').val(input);
	$('#idSolicitud_').val(input);
	
	$('#modal_justificacion').modal('show');
	$('#NumSol_hd').val(num);
	$('#NumSol_lbl').html(num);
	$('#downloadfilejust').attr('href', link);
	$('#downloadfilejust').attr('target', 'blank');
}
///////////////////////   FIN      scripts unicamente para la bandeja "Justificación"



