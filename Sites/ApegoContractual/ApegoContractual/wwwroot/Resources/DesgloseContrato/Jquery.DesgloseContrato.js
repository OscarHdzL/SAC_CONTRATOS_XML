
$.extend($.fn.dataTable.defaults, {
    responsive: true
} );
function Redimension() {
	try {
		var tables = document.getElementsByTagName('table');
		for (var i = 0; i < tables.length; i++) {
			$('#' + tables[i].id + '').resize();
		}
	}
	catch (err) { }
}


