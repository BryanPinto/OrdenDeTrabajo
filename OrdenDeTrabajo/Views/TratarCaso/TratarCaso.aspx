﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script src="<%: Url.Content("~/Styles/js/jquery-3.2.1.js") %>"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <title>Tratar caso</title>
   <script>
       $(document).ready(function () {           
           $("#btnGuardar").click(function (e) {
               e.preventDefault();
               console.log("1");
               var formulario = $("#formTratarCaso").serialize(); //ESTO ESTABA ANTES
               var archivos = new FormData(formulario);    //ESTO ESTABA ANTES
               //var formulario = $("#formTratarCaso"); // ESTO HAY QUE PROBARLO
               //var archivos = new FormData();   //ESTO HAY QUE PROBARLO
                             
               //var datos = $(formulario, archivos).serialize(); //se envia como data


               // Buscar cabeceras
               $.ajax({
                   url: '<%: Url.Content("~/TratarCaso/ActualizarCaso/") %>',
                   //data: formulario.extends(archivos), /*SERIALIZANDO FORMULARIO FUNCIONA, CUANDO DATA ES "ARCHIVOS" NO CONSIDERA LOS CAMPOS EN CONTROLADOR*/
                   //data: datos, //ESTO HAY QUE PROBARLO
                   data: $("#formTratarCaso").serialize(),
                   cache: false,
                   async: false, //ESTO DEBO PROBARLO 
                   type: "POST",
                   processData: false,
                   enctype: "multipart/form-data",
                   success: function (data) {
                       console.log("data");
                       console.log(data);
                       <%--swal({
                           title: 'Modificación exitosa',
                           text: 'La información del caso ha sido actualizada',
                           icon: 'success'
                       }).then(function () {
                           window.location.href = '<%: Url.Content("~/Home/Index") %>';                          
                       });--%>
                       if (data != "error") {
                       }
                       else
                        swal("Modificación sin éxito", "Contacte a un administrador", "error");
                   },
                   error: function () {
                       swal("Problemas al cargar los casos", "Contacte a un administrador", "warning");
                   }
               });


               console.log("-----------ARCHIVOS------------");
               console.log(archivos);
               console.log(archivos);
               jQuery.each(jQuery('#txtArchivoContratista')[0].files, function (i, file) { 
                   archivos.append('txtArchivoContratista' + i, file);
                   //formulario.append('txtArchivoContratista', file);
                   console.log(file);
               }); 
                $.ajax({
                   url: '<%: Url.Content("~/TratarCaso/ActualizarCasoArchivos/") %>',
                   data: archivos,
                   cache: false,
                   dataType: "json",
                   type: "POST",
                   contentType: false,
                   processData: false,
                   enctype: "multipart/form-data",
                   success: function (data) {
                       console.log("data formulario");
                       console.log(data);
                       swal({
                           title: 'Modificación exitosa',
                           text: 'La información del caso ha sido actualizada',
                           icon: 'success'
                       }).then(function () {
                           window.location.href = '<%: Url.Content("~/Home/Index") %>';                          
                       });
                       if (data != "error") {
                       }
                       else
                        swal("Modificación sin éxito", "Contacte a un administrador", "error");
                   },
                   error: function () {
                       swal({
                           title: 'Modificación exitosa',
                           text: 'La información del caso ha sido actualizada',
                           icon: 'success'
                       }).then(function () {
                           window.location.href = '<%: Url.Content("~/Home/Index") %>';                          
                       });
                   }
               });
           });

           $("#btnFinalizar").click(function (e) {
               e.preventDefault();
               $("#btnGuardar").submit();
               // Buscar cabeceras
               $.ajax({                   
                   url: '<%: Url.Content("~/TratarCaso/FinalizarCaso/") %>',
                   data: $("#formTratarCaso").serialize(),
                   cache: false,
                   type: "POST",
                   success: function (data) {
                       console.log("data");
                       console.log(data);
                       //swal("Caso finalizado exitosamente", "Puedes ver el resumen de este caso en la pestaña de casos históricos", "success");
                       swal({
                           title: 'Caso finalizado exitosamente',
                           text: 'Puedes ver el resumen de este caso en la pestaña de casos históricos',
                           icon: 'success'
                       }).then(function () {
                           window.location.href = '<%: Url.Content("~/Home/Index") %>';                          
                       });
                       if (data != "error") {
                       }
                       else
                           swal("Error al finalizar el caso", "Contacte a un administrador", "error");
                   },
                   error: function () {
                       swal("Se produjo un error", "Contacte a un administrador", "warning");
                   }
               });
           });

           if ("<%=ViewData["txtSinCuenta"]%>" != "") {
               if ("<%=ViewData["txtSinCuenta"].ToString()%>" == "True") {
                   $("#txtCuentaContrato").hide();
                   $("#txtCuentaContratoL").hide();
               }
               else {
                   $("#txtCuentaContrato").show();
                   $("#txtCuentaContratoL").show();
               }
           }

           // Mostrar mensaje de creación o error
           if ("<%= ViewData["estado"] %>" == "correcto")
               swal("Inicio de sesión exitoso", "Redirigiendo a vista de casos", "success");
           else if ("<%= ViewData["estado"] %>" == "error")
               swal("Error al iniciar sesión", "Pudo ser debido a credenciales inválidas o hubo un error al consultar los datos. Intente nuevamente", "error");

           var maxHeight = 400;

$(function(){

    $(".dropdown > li").hover(function() {
    
         var $container = $(this),
             $list = $container.find("ul"),
             $anchor = $container.find("a"),
             height = $list.height() * 1.1,       // make sure there is enough room at the bottom
             multiplier = height / maxHeight;     // needs to move faster if list is taller
        
        // need to save height here so it can revert on mouseout            
        $container.data("origHeight", $container.height());
        
        // so it can retain it's rollover color all the while the dropdown is open
        $anchor.addClass("hover");
        
        // make sure dropdown appears directly below parent list item    
        $list
            .show()
            .css({
                paddingTop: $container.data("origHeight")
            });
        
        // don't do any animation if list shorter than max
        if (multiplier > 1) {
            $container
                .css({
                    height: maxHeight,
                    overflow: "hidden"
                })
                .mousemove(function(e) {
                    var offset = $container.offset();
                    var relativeY = ((e.pageY - offset.top) * multiplier) - ($container.data("origHeight") * multiplier);
                    if (relativeY > $container.data("origHeight")) {
                        $list.css("top", -relativeY + $container.data("origHeight"));
                    };
                });
        }
        
    }, function() {
    
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
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float:right">
<nav>
<ul class="dropdown">
        	<li class="drop"><a href="#">Solicitudes</a>
        		<ul class="sub_menu">
        			<li><a href="<%: Url.Content("~/Home/Index") %>">Casos pendientes</a></li>
					<li><a href="<%: Url.Content("~/ListarCasos/ListarCasos") %>">Histórico de casos</a></li>
        		</ul>
        	</li>
        	<li><a href="<%: Url.Content("~/Home/CerrarSesion") %>">Cerrar sesión</a>
        	</li>
        </ul>
</nav></div><br /><br />
    <form id="formTratarCaso" enctype="multipart/form-data">
    <div class="datosSolicitante">
        <h3>Datos solicitante</h3>
        
        <div class="row">            
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtFecha" id="txtFecha">Fecha solicitud</label>
                <input type="date" class="form-control caso" id="txtFechaSolicitud" name="txtFechaSolicitud" readonly value="<%= ViewData["txtFechaSolicitud"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtEjecutivoL" id="txtEjecutivoL"<%-- style="color:#f9f9fb"--%>>Ejecutivo</label>
                <input type="text" class="form-control caso" id="txtEjecutivo" name="txtEjecutivo" placeholder="Ejecutivo" readonly value="<%= ViewData["txtEjecutivo"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtNomSolicitanteL" id="txtNomSolicitanteL"<%-- style="color:#f9f9fb"--%>>Nombre solicitante</label>
                <input type="text" class="form-control caso" id="txtNombreSolicitante" name="txtNombreSolicitante" placeholder="Nombre solicitante" readonly value="<%= ViewData["txtNombreSolicitante"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtCorreoSolicitanteL" id="txtCorreoSolicitanteL"<%-- style="color:#f9f9fb"--%>>Correo electrónico</label>
                <input type="text" class="form-control caso" id="txtCorreo" name="txtCorreo" placeholder="Correo electronico" readonly value="<%= ViewData["txtCorreo"] %>"/>
            </fieldset>
        </div>
    </div>

    <div class="datosSolicitante">
        <h3>Información cliente</h3>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtNumTicketL" id="txtNumTicketL"<%-- style="color:#f9f9fb"--%>>Número ticket CRM</label>
                <input type="text" class="form-control caso" id="txtNumTicketCRM" name="txtNumTicketCRM" placeholder="Número ticket CRM" readonly value="<%= ViewData["txtNumTicketCRM"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso radio col-md-4">
                <label class="radio-inline" style="font-size:17px">Sin cuenta contrato
                </label>
                <% if(ViewData["txtSinCuenta"] != null)
                    {%>
               <% if (ViewData["txtSinCuenta"].ToString() == "True")
                        { %>
                <label class="radio-inline">
                    <input type="radio" name="txtSinCuenta" id="txtSinCuenta" disabled value="true" checked="checked"/>Si
                </label>
                <label class="radio-inline">
                    <input type="radio" name="txtSinCuenta" id="txtSinCuenta" disabled value="false"/>No
                </label>
               <%}
                else
               { %>
                <label class="radio-inline">
                    <input type="radio" name="txtSinCuenta" id="txtSinCuenta" disabled value="true" />Si
                </label>
                <label class="radio-inline">
                    <input type="radio" name="txtSinCuenta" id="txtSinCuenta" disabled value="false" checked="checked"/>No
                </label>
                <%}%><%} %>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-6" id="txtCuentaContrato">
                <label for="txtCuentaContratoL" id="txtCuentaContratoL"<%-- style="color:#f9f9fb"--%>>Cuenta contrato</label>
                <input type="text" class="form-control caso" name="txtCuentaContrato" placeholder="Cuenta contrato" readonly value="<%= ViewData["txtCuentaContrato"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-6">
                <label for="txtNumSerieL" id="txtNumSerieL"<%-- style="color:#f9f9fb"--%>>Número serie medidor</label>
                <input type="text" class="form-control caso" id="txtNumSerieMedidor" name="txtNumSerieMedidor" placeholder="Número serie medidor" readonly value="<%= ViewData["txtNumSerieMedidor"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-6">
                <label for="txtNombreL" id="txtNombreL"<%-- style="color:#f9f9fb"--%>>Nombre</label>
                <input type="text" class="form-control caso" id="txtNombre" name="txtNombre" placeholder="Nombre" readonly value="<%= ViewData["txtNombre"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-6">
                <label for="txtCiudadL" id="txtCiudadL"<%-- style="color:#f9f9fb"--%>>Ciudad</label>
                <input type="text" class="form-control caso" id="txtCiudad" name="txtCiudad" placeholder="Ciudad" readonly value="<%= ViewData["txtCiudad"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtDireccionL" id="txtDireccionL"<%-- style="color:#f9f9fb"--%>>Dirección</label>
                <input type="text" class="form-control caso" id="txtDireccion" name="txtDireccion" placeholder="Dirección" readonly value="<%= ViewData["txtDireccion"] %>"/>
            </fieldset>
            <%--<fieldset class="form-group tratarcaso radio col-md-4">
                <label class="radio-inline" style="font-size:17px">Seleccionar cliente
                </label>
                <% if(ViewData["txtSeleccionarCliente"] != null)
                    {%>
               <% if (ViewData["txtSeleccionarCliente"].ToString() == "True")
                        { %>
                <label class="radio-inline">
                    <input type="radio" name="txtSeleccionarCliente" id="txtSeleccionarCliente" disabled value="true" checked="checked"/>Si
                </label>
                <label class="radio-inline">
                    <input type="radio" name="txtSeleccionarCliente" id="txtSeleccionarCliente" disabled value="false"/>No
                </label>
                 <%}
                else
               { %>
                <label class="radio-inline">
                    <input type="radio" name="txtSeleccionarCliente" id="txtSeleccionarCliente" disabled value="true"/>Si
                </label>
                <label class="radio-inline">
                    <input type="radio" name="txtSeleccionarCliente" id="txtSeleccionarCliente" disabled value="false" checked="checked"/>No
                </label>
                <%}%><%} %>
            </fieldset>--%>
        </div>
    </div>

    <div class="datosSolicitante">
        <h3>Datos de contacto</h3>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtNomContactoL" id="txtNomContactoL"<%-- style="color:#f9f9fb"--%>>Nombre contacto</label>
                <input type="text" class="form-control caso" id="txtNombreContacto" name="txtNombreContacto" placeholder="Nombre contacto" readonly value="<%= ViewData["txtNombreContacto"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtCorreoContactoL" id="txtCorreoContactoL"<%-- style="color:#f9f9fb"--%>>Correo electrónico</label>
                <input type="text" class="form-control caso" id="txtCorreoElectronico" name="txtCorreoElectronico" placeholder="Correo electrónico" readonly value="<%= ViewData["txtCorreoElectronico"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtTelFijoL" id="txtTelFijoL"<%-- style="color:#f9f9fb"--%>>Teléfono fijo</label>
                <input type="text" class="form-control caso" id="txtFonoFijo" name="txtFonoFijo" placeholder="Teléfono fijo" readonly value="<%= ViewData["txtFonoFijo"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtCelularL" id="txtCelularL"<%-- style="color:#f9f9fb"--%>>Celular</label>
                <input type="text" class="form-control caso" id="txtCelular" name="txtCelular" placeholder="Celular" readonly value="<%= ViewData["txtCelular"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtRegionL" id="txtRegionL"<%-- style="color:#f9f9fb"--%>>Región</label>
                <input type="text" class="form-control caso" id="txtRegion" name="txtRegion" placeholder="Región" readonly value="<%= ViewData["txtRegion"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtComunaL" id="txtComunaL"<%-- style="color:#f9f9fb"--%>>Comuna</label>
                <input type="text" class="form-control caso" id="txtComunas" name="txtComunas" placeholder="Comunas" readonly value="<%= ViewData["txtComunas"] %>"/>
            </fieldset>
        </div>
    </div>
    
    <div class="datosSolicitante">
        <h3>Requirimiento</h3>
        
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtContratistaL" id="txtContratistaL"<%-- style="color:#f9f9fb"--%>>Contratista OT Medidor</label>
                <input type="text" class="form-control caso" id="txtContratistas" name="txtContratistas" placeholder="Contratistas OT Medidor" readonly value="<%= ViewData["txtContratistas"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtCorreoContratistaL" id="txtCorreoContratistaL"<%-- style="color:#f9f9fb"--%>>Correo electrónico</label>
                <input type="text" class="form-control caso" id="txtCorreoContratista" name="txtCorreoContratista" placeholder="Correo electrónico" readonly value="<%= ViewData["txtCorreoContratista"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtMotivoL" id="txtMotivoL"<%-- style="color:#f9f9fb"--%>>Motivo OT medidor</label>
                <input type="text" class="form-control caso" id="txtMotivosOT" name="txtMotivosOT" placeholder="Motivos OT Medidor" readonly value="<%= ViewData["txtMotivosOT"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtSubMotivoL" id="txtSubMotivoL"<%-- style="color:#f9f9fb"--%>>Sub motivo OT medidor</label>
                <input type="text" class="form-control caso" id="txtSubMotivosOT" name="txtSubMotivosOT" placeholder="Sub Motivos OT Medidor" readonly value="<%= ViewData["txtSubMotivosOT"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtComentarioSoliL" id="txtComentarioSoliL"<%-- style="color:#f9f9fb"--%>>Comentario solicitud</label>
                <textarea class="form-control caso" id="txtComentarioSolicitud" name="txtComentarioSolicitud" placeholder="Comentario solicitud" disabled ><%= ViewData["txtComentarioSolicitud"] %></textarea>
            </fieldset>
            <fieldset class="form-group tratarcaso archivo col-md-4">
                <% if(ViewData["txtArchivo"] != null)
                    {%>
                <label for="txtArchivo">Archivo</label>
                <% if(ViewData["txtArchivo"] != null)%>
                            <%= ViewData["txtArchivo"] %>
                <%}%>
                <%--<input type="file" class="form-control" id="txtArchivo" name="txtArchivo" disabled value="<%= ViewData["txtArchivo"] %>"/>--%>
            </fieldset>
        </div>
        <br />
        <div class="row">
            <h3>Campos obligatorios</h3>
            <fieldset class="form-group tratarcaso modificable col-md-4">
                <label for="txtFechaDeVisitaL">Fecha visita</label>
                <input type="date" class="form-control casoModificable" id="txtFechaDeVisita" name="txtFechaDeVisita" value="<%= ViewData["txtFechaDeVisita"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso archivoModificable col-md-4">
                <label for="txtArchivoSoliL" id="txtArchivoSoliL">Archivo</label>
                <input type="file" class="form-control" id="txtArchivoContratista" name="txtArchivoContratista" multiple value="<%= ViewData["txtArchivoContratista"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso modificable col-md-4">
                <% if(ViewData["txtArchivosCargados"] != null)
                    {%>
                <label for="txtArchivoCargados" id="txtArchivoCargados">Archivo cargados</label>
                <% if(ViewData["txtArchivosCargados"] != null)%>
                    <%= ViewData["txtArchivosCargados"] %>
                <%--<input type="file" class="form-control" id="txtArchivoCargados1" name="txtArchivoCargados1" multiple value="<%= ViewData["txtArchivosCargados"] %>"/>--%>
                 <%}%>
            </fieldset>               
            <fieldset class="form-group tratarcaso modificable col-md-4">
                <label for="txtComentarioCierreL" id="txtComentarioCierreL"<%-- style="color:#f9f9fb"--%>>Comentario cierre</label>
                <textarea class="form-control casoModificable" id="txtComentarioCierre" name="txtComentarioCierre" placeholder="Comentario cierre de solicitud"><%= ViewData["txtComentarioCierre"] %></textarea>
            </fieldset>
        </div>
    </div>

    <div class="botones">
        <div class="row">
            <fieldset class="form-group botones col-md-4">
                <input type="hidden" name="txtNumCaso" id="txtNumCaso" value="<%= Request.Url.Segments.LastOrDefault() %>"/>
                <input type="submit" id="btnGuardar" value="Guardar" class="guardar">
                <input type="button" id="btnFinalizar" value="Finalizar" class="finalizar">
            </fieldset>
        </div>
    </div>
        </form>

</asp:Content>
