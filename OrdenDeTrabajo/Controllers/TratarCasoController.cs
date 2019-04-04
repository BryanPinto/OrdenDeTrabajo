using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace WebSolicitudes.Controllers
{
    public class TratarCasoController : Controller
    {
        //
        // GET: /TratarCaso/

        public ActionResult TratarCaso(int id)
        {
            string xml = ObtenerCaso(id);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            //Obtener valores del xml respuesta de Bizagi del numero de caso enviado

            //DATOS SOLICITANTE
            string txtFechaSolicitud = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.FechaSolicitud']").InnerText;
            string txtEjecutivo = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.EjecutivoOficina']").InnerText;
            string txtNombreSolicitante = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SolicitanteOTMedidor']").InnerText;
            string txtCorreoElectronico = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.EjecutivoOficina.CorreoElectronico']").InnerText;

            //INFORMACION CLIENTE
            string txtNumTicketCRM = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.NumeroTicketCRM']").InnerText;
            string txtSinCuentaContrato = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SinCuentaContrato']").InnerText;
            string txtCuentaContrato = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.CuentaContrato']").InnerText;
            string txtNombre = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Nombre']").InnerText;
            string txtNumSerieMedidor = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.NumeroSerieMedidor']").InnerText;
            string txtCiudad = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Ciudad']").InnerText;
            string txtDireccion = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Direccion']").InnerText;
            string txtSeleccionarCliente = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SeleccionarCliente']").InnerText;

            //DATOS DE CONTACTO
            string txtNombreContacto = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Contacto.NombreContacto']").InnerText;
            string txtCorreoContacto = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Contacto.CorreoElectronico']").InnerText;
            string txtTelefonoFijo = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Contacto.TelefonoFijo']").InnerText;
            string txtCelularContacto = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Contacto.Celular']").InnerText;
            string txtRegion = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Region']").InnerText;
            string txtComunas = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Comunas']").InnerText;

            //REQUERIMIENTO
            string txtContratistasOT = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ContratistasOTMedidor']").InnerText;
            string txtCorreoContratista = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ContratistasOTMedidor.CorreoElectronico']").InnerText;
            string txtMotivoOT = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.MotivoOT']").InnerText;
            string txtSubMotivoOT = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SubMotivoOT']").InnerText;
            string txtComentarioSolici = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ComentarioSolicitud']").InnerText;
            string txtArchivoBase64 = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item").InnerText;
            string txtArchivoNombre = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item").Attributes["FileName"].InnerText;

            //FORMATEAR FECHA SOLICITUD
            if (txtFechaSolicitud != string.Empty)
            {
                DateTime fechaInicio = DateTime.Parse(txtFechaSolicitud);
                ViewData["txtFechaTermino"] = fechaInicio.ToString("yyyy-MM-dd");
            }

            //ASIGNAR VALORES RESCATADOS DE XML A CAMPOS DEL FORMULARIO
            //ViewData["txtFechaSolicitud"] = txtFechaSolicitud;
            ViewData["txtEjecutivo"] = txtEjecutivo;
            ViewData["txtNombreSolicitante"] = txtNombreSolicitante;
            ViewData["txtCorreo"] = txtCorreoElectronico;

            ViewData["txtNumTicketCRM"] = txtNumTicketCRM;
            ViewData["txtSinCuenta"] = txtSinCuentaContrato;
            ViewData["txtCuentaContrato"] = txtCuentaContrato;
            ViewData["txtNombre"] = txtNombre;
            ViewData["txtNumSerieMedidor"] = txtNumSerieMedidor;
            ViewData["txtCiudad"] = txtCiudad;
            ViewData["txtDireccion"] = txtDireccion;
            ViewData["txtSeleccionarCliente"] = txtSeleccionarCliente;

            ViewData["txtNombreContacto"] = txtNombreContacto;
            ViewData["txtCorreoElectronico"] = txtCorreoContacto;
            ViewData["txtFonoFijo"] = txtTelefonoFijo;
            ViewData["txtCelular"] = txtCelularContacto;
            ViewData["txtRegion"] = txtRegion;
            ViewData["txtComunas"] = txtComunas;

            ViewData["txtContratistas"] = txtContratistasOT;
            ViewData["txtCorreoContratista"] = txtCorreoContratista;
            ViewData["txtMotivosOT"] = txtMotivoOT;
            ViewData["txtCelular"] = txtSubMotivoOT;
            ViewData["txtComentarioSolicitud"] = txtComentarioSolici;
            ViewData["txtArchivo"] = @"
                    <a download='" + txtArchivoNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtArchivoBase64 + @"' class='btn btn-primary btn-md'>
                        <span class='glyphicon glyphicon-save'></span> Descargar " + txtArchivoNombre + @"
                    </a>
                ";

            return View();
        }

        public string ObtenerCaso(int id)
        {
            int numCasoXML = id;
            LipigasEntityManager.EntityManagerSOASoapClient servicioQuery = new LipigasEntityManager.EntityManagerSOASoapClient();

            string respuestaBizagi = "";

            //Escribir log con el xml creado como consulta de casos
            string rutaLog = HttpRuntime.AppDomainAppPath;
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine +
                      DateTime.Now.ToShortDateString() + " " +
                      DateTime.Now.ToShortTimeString() + ": " +
                      "Número caso solicitado: " + numCasoXML);
            System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
            sb.Clear();

            //Crear XML para obtener la información del caso seleccionado para trabajar

            string queryObtenerCaso = @"
                <BizAgiWSParam>
                    <CaseInfo>
                        <CaseNumber>" + id + @"</CaseNumber>
                    </CaseInfo>
                    <XPaths>
                        <XPath XPath=""OrdendeTrabajoMedidor.FechaSolicitud""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.SolicitanteOTMedidor""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.EjecutivoOficina.CorreoElectronico""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.EjecutivoOficina""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.NumeroTicketCRM""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.SinCuentaContrato""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.CuentaContrato""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Nombre""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.SolicitanteOTMedidor""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.NumeroSerieMedidor""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Ciudad""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Direccion""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.SeleccionarCliente""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Contacto.NombreContacto""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Contacto.CorreoElectronico""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Contacto.TelefonoFijo""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Contacto.Celular""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Region""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Comunas""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.ContratistasOTMedidor""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.ContratistasOTMedidor.CorreoElectronico""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.MotivoOT""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.SubMotivoOT""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.ComentarioSolicitud""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Archivo""/>
                    </XPaths>
                    </BizAgiWSParam>";

            respuestaBizagi = servicioQuery.getCaseDataUsingXPathsAsString(queryObtenerCaso);
            respuestaBizagi = respuestaBizagi.Replace("\n", "");
            respuestaBizagi = respuestaBizagi.Replace("\t", "");
            respuestaBizagi = respuestaBizagi.Replace("\r", "");

            //Escribir log con el xml creado como consulta de casos
            rutaLog = HttpRuntime.AppDomainAppPath;
            sb = new StringBuilder();
            sb.Append(Environment.NewLine +
                      DateTime.Now.ToShortDateString() + " " +
                      DateTime.Now.ToShortTimeString() + ": " +
                      "queryObtenerCaso: " + queryObtenerCaso + "| " + "Respuesta Bizagi: " + respuestaBizagi);
            System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
            sb.Clear();

            return (respuestaBizagi);//CAMBIAR
        }

    }
}
