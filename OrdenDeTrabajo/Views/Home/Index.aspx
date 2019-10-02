<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script src="<%: Url.Content("~/Styles/js/moment.js") %>"></script>
    <script src="<%: Url.Content("~/Styles/js/datatable.min.js") %>"></script>
    <link href="<%: Url.Content("~/Styles/css/datatable.min.css") %>" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link href="<%: Url.Content("~/Styles/css/newdatatables.min.css") %>" rel="stylesheet" />
    <script src="<%: Url.Content("~/Styles/js/newdatatables.min.js") %>"></script>
    <%--<script src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>--%>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.css"/>
    <script src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.min.js"></script>--%>
    <link href="<%: Url.Content("~/Styles/css/custom.css") %>" rel="stylesheet" />
    <title>Casos pendientes</title>
    <script>
        $(document).ready(function () {
            // Datatable y propiedades
            var table = $('#tablaindex').DataTable({
                //"sDom": '<"top">rt<"bottom"ip><"clear">',
                "dom": 'lBfrtip',
                "buttons": [
                    {
                        extend: 'collection',
                        text: 'Descargar',
                        buttons: [
                            'excel',
                            'csv',
                            'pdf'
                        ]
                    }
                ],
                "order": [[0, "desc"]],
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                }
            });

            //Filtrar la lista de submotivos una vez seleccionado un motivo
            //var motivoSel = $("#txtMotivoSelect").val();
            $("#txtMotivoSelect").change(function () {
                var motivo = $("#txtMotivoSelect").find('option:selected').text(); // guardar valor de motivo seleccionado
                if (motivo != "Seleccione opción") {
                    $("#option-container").children().appendTo("#txtSubMotivoSelect"); // mueve <option> contenida en #option-container de vuelta a su <select>
                    var toMove = $("#txtSubMotivoSelect").children("[data-father!='" + motivo + "']"); // mover submotivos filtrados por motivo distinto al seleccionado
                    toMove.appendTo("#option-container"); // mover valores de submotivo a #option-container
                    $("#txtSubMotivoSelect").removeAttr("disabled"); // 
                }
            });

            $("#formIndex").submit(function (e) {
                e.preventDefault();
                // Buscar cabeceras
                $.ajax({
                    url: '<%: Url.Content("~/ListarCasos/CasosPendientes/") %>',
                    data: $("#formIndex").serialize(),
                    cache: false,
                    type: "POST",
                    success: function (data) {
                        if (data != "error") {
                            $('#formIndex').find('tbody').hide();
                            table.clear();
                            table.rows.add(JSON.parse(data));
                            table.draw();
                            $('#formIndex').find('tbody').fadeIn("slow");
                        }
                        else
                            alert("Error al buscar");
                    },
                    error: function () {
                        alert("Error al buscar");
                    }
                });
            });

            $("#formIndex").submit();

            // Validar números
            $("#txtNroCaso").numeric("{ negative : false , decimalPlaces : 0 , decimal : ',' }");

            var maxHeight = 400;
        

            console.log("Antes a funcion");
            $(function () {
                console.log("0");
                $("#dropdown > li").hover(function () {
                    console.log("1");
                    var $container = $(this),
                        $list = $container.find("ul"),
                        $anchor = $container.find("a"),
                        height = $list.height() * 1.1,       // make sure there is enough room at the bottom
                        multiplier = height / maxHeight;     // needs to move faster if list is taller
                    console.log("2");

                    // need to save height here so it can revert on mouseout            
                    $container.data("origHeight", $container.height());
                    console.log("3");
                    // so it can retain it's rollover color all the while the dropdown is open
                    $anchor.addClass("hover");
                    console.log("4");
                    // make sure dropdown appears directly below parent list item    
                    $list
                        .show()
                        .css({
                            paddingTop: $container.data("origHeight")
                        });
                    console.log("5");
                    // don't do any animation if list shorter than max
                    if (multiplier > 1) {
                        console.log("6");
                        $container
                            .css({
                                height: maxHeight,
                                overflow: "hidden"
                            })
                            .mousemove(function (e) {
                                var offset = $container.offset();
                                var relativeY = ((e.pageY - offset.top) * multiplier) - ($container.data("origHeight") * multiplier);
                                if (relativeY > $container.data("origHeight")) {
                                    $list.css("top", -relativeY + $container.data("origHeight"));
                                };
                            });
                        console.log("7");
                    }

                }, function () {

                    var $el = $(this);

                    // put things back to normal
                    $el
                        .height($(this).data("origHeight"))
                        .find("ul")
                        .css({ top: 0 })
                        .hide()
                        .end()
                        .find("a")
                        .removeClass("hover");

                });
            });
        });
                        // Mostrar mensaje de creación o error
                        if ("<%= ViewData["estado"] %>" == "1")
                            swal("Inicio de sesión exitoso", "Redirigiendo a vista de casos", "success");
                        else if ("<%= ViewData["estado"] %>" == "0")
                            swal("Error al iniciar sesión", "Pudo ser debido a credenciales inválidas o hubo un error al consultar los datos. Intente nuevamente", "error");
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float:right">
<nav>
<ul id="dropdown">
        	<%--<li class="drop"><a href="#">Solicitudes</a>
        		<ul class="sub_menu">
        			<li><a href="<%: Url.Content("~/Home/Index") %>">Casos pendientes</a></li>
					<li><a href="<%: Url.Content("~/ListarCasos/ListarCasos") %>">Histórico de casos</a></li>
        		</ul>
        	</li>--%>
            <li><a href="<%: Url.Content("~/Home/Index") %>">Casos pendientes</a></li>
            <li><a href="<%: Url.Content("~/ListarCasos/ListarCasos") %>">Histórico de casos</a></li>
        	<li><a href="<%: Url.Content("~/Home/CerrarSesion") %>">Cerrar sesión</a>
        	</li>
        </ul>
</nav></div><br /><br />
    <h2 style="text-align:center;color:#AEAEAE">Casos pendientes</h2><br />

    <%--FILTROS--%>
    <form id="formIndex">
        <div class="panel panel-primary">
            <div class="panel-heading">Filtros</div>
            <div class="panel-body">
                <div class="row">
                    <%--<fieldset class="form-group col-md-1">
                    </fieldset>--%>
                    <fieldset class="form-group col-md-2">
                        <label for="txtFechaDesde">Fecha desde</label>
                        <input type="date" class="form-control" id="txtFechaDesde" name="txtFechaDesde" />
                    </fieldset>
                    <fieldset class="form-group col-md-2">
                        <label for="txtFechaHasta">Fecha hasta</label>
                        <input type="date" class="form-control" id="txtFechaHasta" name="txtFechaHasta" />
                    </fieldset>
                    <fieldset class="form-group col-md-2">
                        <label for="txtNroCaso">Número de caso</label>
                        <input type="text" class="form-control" id="txtNroCaso" name="txtNroCaso" placeholder="Número de caso" />
                    </fieldset>
                  <fieldset class="form-group col-md-2">
                        <label for="txtMotivoSelect">Motivo</label>
                        <select name="txtMotivoSelect" id="txtMotivoSelect" class="form-control">
                            <option value="0">Seleccione opción</option>
                            <%=ViewData["txtMotivoSelect1"]%>
                        </select>
                      <%--<select class="select" id="province">
                          <option value="1">RM</option>
                          <option value="2">FI</option>
                        </select>--%>
                    </fieldset>
                   <fieldset class="form-group col-md-2">
                        <label for="txtSubMotivoSelect">Sub motivo</label>
                        <select name="txtSubMotivoSelect" id="txtSubMotivoSelect" class="form-control">
                            <option data-father="null" value="0">Seleccione opción</option>
                            <%=ViewData["txtSubMotivoSelect1"]%>
                        </select>
                       <%--<select class="select" id="city">
                          <option data-province="RM" value="1">ROMA</option>
                          <option data-province="RM" value="2">ANGUILLARA SABAZIA</option>
                          <option data-province="FI" value="3">FIRENZE</option>
                          <option data-province="FI" value="4">PONTASSIEVE</option>
                        </select>--%>
                    </fieldset>
                    <fieldset class="form-group col-md-2">
                        <%--<div id="buscarListar">--%>
                        <span id="option-container" style="visibility: hidden; position:absolute;"></span>
                            <input type="submit" id="btnBuscarListar" value="Buscar" class="buscar">
                        <%--</div>--%>
                    </fieldset>
                </div>
            </div>
        </div>
    </form>
   <%-- <a href='<%: Url.Content("~/Home/Exportar/") %>' id="descargarTabla" class="btn btn-default" role="button">Descargar plantilla</a>--%>
    <%--TABLA--%>
    <div class="table-responsive">
        <table class="table table-bordered table-hover" id="tablaindex">
            <thead>
                <tr>
                    <%--<th class="col-xs-2">Proceso</th>--%>
                    <th class="col-xs-1">Número de caso</th>
                    <%--<th class="col-xs-1">Estado del caso</th>--%>
                    <th class="col-xs-1">Fecha de asignación</th>
                    <%--<th class="col-xs-1">Fecha de solución</th>--%>
                    <%--<th class="col-xs-4">Actividad actual</th>--%>
                    <th class="col-xs-1">Motivo</th>
                    <th class="col-xs-1">Sub motivo</th> 
                    <th class="col-xs-1"></th>
                </tr>
            </thead>
            <tbody><%= ViewData["valores"] %></tbody>
        </table>
    </div>

</asp:Content>
