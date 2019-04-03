﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <title>Tratar caso</title>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="datosSolicitante">
        <h3>Datos solicitante</h3>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtFecha" id="txtFecha">Fecha solicitud</label>
                <input type="date" class="form-control caso" id="txtFechaSolicitud" name="txtFechaSolicitud" readonly value="<%= ViewData["txtFechaSolicitud"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtEjecutivoLabel" id="txtEjecutivoLabel" style="color:#f9f9fb">Ejecutivo</label>
                <input type="text" class="form-control caso" id="txtEjecutivo" name="txtEjecutivo" placeholder="Ejecutivo" readonly value="<%= ViewData["txtEjecutivo"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNombreSolicitante" name="txtNombreSolicitante" placeholder="Nombre solicitante" readonly value="<%= ViewData["txtNombreSolicitante"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCorreo" name="txtCorreo" placeholder="Correo electronico" readonly value="<%= ViewData["txtCorreo"] %>"/>
            </fieldset>
        </div>
    </div>

    <div class="datosSolicitante">
        <h3>Información cliente</h3>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNumTicketCRM" name="txtNumTicketCRM" placeholder="Número ticket CRM" readonly value="<%= ViewData["txtNumTicketCRM"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso radio col-md-4">
                <label class="radio-inline" style="font-size:17px">Sin cuenta contrato
                </label>
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
                <%}%>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCuentaContrato" name="txtCuentaContrato" placeholder="Cuenta contrato" readonly value="<%= ViewData["txtCuentaContrato"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNumSerieMedidor" name="txtNumSerieMedidor" placeholder="Número serie medidor" readonly value="<%= ViewData["txtNumSerieMedidor"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNombre" name="txtNombre" placeholder="Nombre" readonly value="<%= ViewData["txtNombre"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCiudad" name="txtCiudad" placeholder="Ciudad" readonly value="<%= ViewData["txtCiudad"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtDireccion" name="txtDireccion" placeholder="Dirección" readonly value="<%= ViewData["txtDireccion"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso radio col-md-4">
                <label class="radio-inline" style="font-size:17px">Seleccionar cliente
                </label>
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
                <%}%>
            </fieldset>
        </div>
    </div>

    <div class="datosSolicitante">
        <h3>Datos de contacto</h3>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNombreContacto" name="txtNombreContacto" placeholder="Nombre contacto" readonly value="<%= ViewData["txtNombreContacto"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCorreoElectronico" name="txtCorreoElectronico" placeholder="Correo electrónico" readonly value="<%= ViewData["txtCorreoElectronico"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtFonoFijo" name="txtFonoFijo" placeholder="Teléfono fijo" readonly value="<%= ViewData["txtFonoFijo"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCelular" name="txtCelular" placeholder="Celular" readonly value="<%= ViewData["txtCelular"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtRegion" name="txtRegion" placeholder="Región" readonly value="<%= ViewData["txtRegion"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtComunas" name="txtComunas" placeholder="Comunas" readonly value="<%= ViewData["txtComunas"] %>"/>
            </fieldset>
        </div>
    </div>

    <div class="datosSolicitante">
        <h3>Requirimiento</h3>
        
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtContratistas" name="txtContratistas" placeholder="Contratistas OT Medidor" readonly value="<%= ViewData["txtContratistas"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCorreoContratista" name="txtCorreoContratista" placeholder="Correo electrónico" readonly value="<%= ViewData["txtCorreoContratista"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtMotivosOT" name="txtMotivosOT" placeholder="Motivos OT Medidor" readonly value="<%= ViewData["txtMotivosOT"] %>"/>
            </fieldset>
            <fieldset class="form-group tratarcaso archivo col-md-4">
                <%--<label for="txtArchivo">Archivo</label>--%>
                <input type="file" class="form-control" id="txtArchivo" name="txtArchivo" disabled value="<%= ViewData["txtArchivo"] %>"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <textarea class="form-control caso" id="txtComentarioSolicitud" name="txtComentarioSolicitud" placeholder="Comentario solicitud" disabled ><%= ViewData["txtComentarioSolicitud"] %></textarea>
            </fieldset>
        </div>
        <br />
        <div class="row">
            <h3>Campos obligatorios</h3>
            <fieldset class="form-group tratarcaso modificable col-md-4">
                <label for="txtFechaDeVisita">Fecha visita</label>
                <input type="date" class="form-control casoModificable" id="txtFechaDeVisita" name="txtFechaDeVisita" required/>
            </fieldset>
            <fieldset class="form-group tratarcaso archivoModificable col-md-4">
                <input type="file" class="form-control" id="txtArchivoContratista" name="txtArchivoContratista" required/>
            </fieldset>
            <fieldset class="form-group tratarcaso modificable col-md-4">
                <textarea class="form-control casoModificable" id="txtComentarioCierre" name="txtComentarioCierre" placeholder="Comentario cierre de solicitud" required></textarea>
            </fieldset>
        </div>
    </div>

    <div class="botones">
        <div class="row">
            <fieldset class="form-group botones col-md-4">
                <input type="button" id="btnGuardar" value="Guardar" class="guardar">
                <input type="button" id="btnFinalizar" value="Finalizar" class="finalizar">
            </fieldset>
        </div>
    </div>

</asp:Content>
