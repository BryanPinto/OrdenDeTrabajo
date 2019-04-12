<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script src="<%: Url.Content("~/Styles/js/jquert-3.2.1.js") %>"></script>
    <title>Tratar caso</title>
   <script>
       $(document).ready(function () {           
           $("#btnGuardar").click(function (e) {
               e.preventDefault();
               console.log("1");
               var archivos = new FormData();
           jQuery.each(jQuery('#txtArchivoContratista')[0].files, function (i, file) {
               console.log("archivos: "+archivos);
                archivos.append('file-'+i, file);
               });
               console.log(archivos);
               // Buscar cabeceras
               console.log($("#formTratarCaso").serialize());
               $.ajax({
                   url: '<%: Url.Content("~/TratarCaso/ActualizarCaso/") %>',
                   data: $("#formTratarCaso").serialize(),
                   cache: false,
                   type: "POST",
                   success: function (data) {
                       console.log("data");
                       console.log(data);
                       if (data != "error") {
                           $('#tablaordenes').find('tbody').hide();
                           table.clear();
                           table.rows.add(JSON.parse(data));
                           table.draw();
                           $('#tablaordenes').find('tbody').fadeIn("slow");
                       }
                       else
                           alert("Error al buscar");
                   },
                   error: function () {
                       alert("Error al buscar");
                   }
               });
           });
           $("#formCerrarCaso").submit(function (e) {
               e.preventDefault();
               // Buscar cabeceras
               $.ajax({
                   url: '<%: Url.Content("~/TratarCaso/ActualizarCaso/") %>',
                   data: $("#formCerrarCaso").serialize(),
                   cache: false,
                   type: "POST",
                   success: function (data) {
                       console.log("data");
                       console.log(data);
                       if (data != "error") {
                           $('#tablaordenes').find('tbody').hide();
                           table.clear();
                           table.rows.add(JSON.parse(data));
                           table.draw();
                           $('#tablaordenes').find('tbody').fadeIn("slow");
                       }
                       else
                           alert("Error al buscar");
                   },
                   error: function () {
                       alert("Error al buscar");
                   }
               });
           });
       });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtCuentaContratoL" id="txtCuentaContratoL"<%-- style="color:#f9f9fb"--%>>Cuenta contrato</label>
                <input type="text" class="form-control caso" id="txtCuentaContrato" name="txtCuentaContrato" placeholder="Cuenta contrato" readonly value="<%= ViewData["txtCuentaContrato"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtNumSerieL" id="txtNumSerieL"<%-- style="color:#f9f9fb"--%>>Número serie medidor</label>
                <input type="text" class="form-control caso" id="txtNumSerieMedidor" name="txtNumSerieMedidor" placeholder="Número serie medidor" readonly value="<%= ViewData["txtNumSerieMedidor"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtNombreL" id="txtNombreL"<%-- style="color:#f9f9fb"--%>>Nombre</label>
                <input type="text" class="form-control caso" id="txtNombre" name="txtNombre" placeholder="Nombre" readonly value="<%= ViewData["txtNombre"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtCiudadL" id="txtCiudadL"<%-- style="color:#f9f9fb"--%>>Ciudad</label>
                <input type="text" class="form-control caso" id="txtCiudad" name="txtCiudad" placeholder="Ciudad" readonly value="<%= ViewData["txtCiudad"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtDireccionL" id="txtDireccionL"<%-- style="color:#f9f9fb"--%>>Dirección</label>
                <input type="text" class="form-control caso" id="txtDireccion" name="txtDireccion" placeholder="Dirección" readonly value="<%= ViewData["txtDireccion"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso radio col-md-4">
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
            </fieldset>
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
                <label for="txtArchivo">Archivo</label>
                <% if(ViewData["txtArchivo"] != null)%>
                            <%= ViewData["txtArchivo"] %>
                <%--<input type="file" class="form-control" id="txtArchivo" name="txtArchivo" disabled value="<%= ViewData["txtArchivo"] %>"/>--%>
            </fieldset>
        </div>
        <br />
        <div class="row">
            <h3>Campos obligatorios</h3>
            <fieldset class="form-group tratarcaso modificable col-md-4">
                <label for="txtFechaDeVisita">Fecha visita</label>
                <input type="date" class="form-control casoModificable" id="txtFechaDeVisita" name="txtFechaDeVisita" value="<%= ViewData["txtFechaDeVisita"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso archivoModificable col-md-4">
                <label for="txtArchivoSoliL" id="txtArchivoSoliL"<%-- style="color:#f9f9fb"--%>>Archivo</label>
                <input type="file" class="form-control" id="txtArchivoContratista" name="txtArchivoContratista" multiple value="<%= ViewData["txtArchivoContratista"] %>"/>
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
                <input type="submit" id="btnFinalizar" value="Finalizar" class="finalizar">
            </fieldset>
        </div>
    </div>
        </form>

</asp:Content>
