<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script src="<%: Url.Content("~/Styles/js/moment.js") %>"></script>
    <script src="<%: Url.Content("~/Styles/js/datatable.min.js") %>"></script
    <link href="<%: Url.Content("~/Styles/css/datatable.min.css") %>" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <title>Login</title>
    <meta charset="utf-8">
    <script>
        $(document).ready(function () {
            $("#form-signin").submit(function (e) {
                e.preventDefault();
                // Buscar cabeceras
                $.ajax({
                    url: '<%: Url.Content("~/Home/Login/") %>',
                    data: $("#form-signin").serialize(),
                    cache: false,
                    type: "POST",
                    success: function (data) {
                        if (data != "error") {
                            $('#form-signin').find('tbody').hide();
                            table.clear();
                            table.rows.add(JSON.parse(data));
                            table.draw();
                            $('#form-signin').find('tbody').fadeIn("slow");
                        }
                        else
                            alert("Error al buscar");
                    },
                    error: function () {
                        alert("Error al buscar");
                    }
                });
            });           

            // Mostrar mensaje de creación o error
            if ("<%= ViewData["estado"] %>" == "1")
                swal("Inicio de sesión exitoso", "Redirigiendo a vista de casos", "success");
            else if ("<%= ViewData["estado"] %>" == "0")
                swal("Error al iniciar sesión", "Pudo ser debido a credenciales inválidas o hubo un error al consultar los datos. Intente nuevamente", "error");
        });
            </script>
    <link href="<%: Url.Content("~/Styles/css/custom.css")%>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="card card-container">
            <img id="profile-img" class="profile-img-card" src="<%: Url.Content("https://www.lipigas.cl/wp-content/uploads/2018/06/logo-main.svg") %>" />
            <%--<p id="profile-name" class="profile-name-card">Inicie sesión con su usuario de Active Directory</p>--%>
            <form class="form-signin" method="post" <%--action="<%: Url.Content("~/Home/Login") %>"--%>>
                <span id="reauth-email" class="reauth-email"></span>
                <input type="hidden" id="txtVacio" name="txtVacio" class="form-control">
                <input type="text" id="txtCorreo" name="txtCorreo" class="form-control" placeholder="Correo electrónico" required autofocus>
                <input type="password" id="txtPass" name="txtPass" class="form-control" placeholder="Contraseña" required>
                <br />
                <fieldset class="has-error">
                    <span class="help-block"><%= ViewData["mensaje"] %></span>
                </fieldset>
                <%--<button class="btn btn-md btn-primary btn-block btn-signin" type="submit">Iniciar sesión</button>--%>
                <input type="submit" id="btnIniciar" value="Iniciar sesión" class="btnIniciar">
                <br />
            </form>
        </div>
        <!-- /card-container -->
    </div>
    <!-- /container -->
</asp:Content>
