<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="datosSolicitante">
        <h3>Datos solicitante</h3>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="date" class="form-control caso" id="txtFechaSolicitud" name="txtFechaSolicitud" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="date" class="form-control caso" id="txtFechaSolicitud2" name="txtFechaSolicitud2" readonly/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCorreo" name="txtCorreo" placeholder="Correo electronico" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNombreSolicitante" name="txtNombreSolicitante" placeholder="Nombre solicitante" readonly/>
            </fieldset>
        </div>
    </div>

    <div class="datosSolicitante">
        <h3>Información cliente</h3>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNumTicketCRM" name="txtNumTicketCRM" placeholder="Número ticket CRM" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label class="radio-inline" style="font-size:17px">Sin cuenta contrato
                </label>
                <label class="radio-inline">
                    <input type="radio" name="SinCuentaSi" disabled/>Si
                </label>
                <label class="radio-inline">
                    <input type="radio" name="SinCuentaNo" disabled/>No
                </label>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCuentaContrato" name="txtCuentaContrato" placeholder="Cuenta contrato" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNumSerieMedidor" name="txtNumSerieMedidor" placeholder="Número serie medidor" readonly/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNombre" name="txtNombre" placeholder="Nombre" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCiudad" name="txtCiudad" placeholder="Ciudad" readonly/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtDireccion" name="txtDireccion" placeholder="Dirección" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label class="radio-inline" style="font-size:17px">Seleccionar cliente
                </label>
                <label class="radio-inline">
                    <input type="radio" name="SeleccionarClienteSi" disabled/>Si
                </label>
                <label class="radio-inline">
                    <input type="radio" name="SeleccionarClienteNo" disabled/>No
                </label>
            </fieldset>
        </div>
    </div>

    <div class="datosSolicitante">
        <h3>Datos de contacto</h3>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtNombreContacto" name="txtNombreContacto" placeholder="Nombre contacto" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCorreoElectronico" name="txtCorreoElectronico" placeholder="Correo electrónico" readonly/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtFonoFijo" name="txtFonoFijo" placeholder="Teléfono fijo" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCelular" name="txtCelular" placeholder="Celular" readonly/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtRegion" name="txtRegion" placeholder="Región" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtComunas" name="txtComunas" placeholder="Comunas" readonly/>
            </fieldset>
        </div>
    </div>

    <div class="datosSolicitante">
        <h3>Requirimiento</h3>
        <h4>(*)Campos obligatorios</h4>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtContratistas" name="txtContratistas" placeholder="Contratistas OT Medidor" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtCorreoContratista" name="txtCorreoContratista" placeholder="Correo electrónico" readonly/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <input type="text" class="form-control caso" id="txtMotivosOT" name="txtMotivosOT" placeholder="Motivos OT Medidor" readonly/>
            </fieldset>
            <fieldset class="form-group tratarcaso col-md-4">
                <label for="txtArchivo">Archivo</label>
                <input type="file" class="form-control" id="txtArchivo" name="txtArchivo"/>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="form-group tratarcaso col-md-4">
                <textarea class="form-control caso" id="txtComentario" name="txtComentario" placeholder="Comentario solicitud*" required></textarea>
            </fieldset>
        </div>
    </div>

    <div class="botones">
        <div class="row">
            <fieldset class="form-group botones col-md-4">
                <input type="button" id="btnCancelar" value="Cancelar" class="cancelar">
                <input type="button" id="btnGuardar" value="Guardar" class="guardar">
                <input type="button" id="btnFinalizar" value="Finalizar" class="finalizar">
            </fieldset>
        </div>
    </div>

</asp:Content>
