using System;
using System.Collections.Generic;
using System.IO;
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
            string txtFechaSolicitud    = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.FechaSolicitud']").InnerText;
            string txtEjecutivo         = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.EjecutivoOficina.Nombre']").InnerText;
            string txtNombreSolicitante = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SolicitanteOTMedidor.NombreSolicitante']").InnerText;
            string txtCorreoElectronico = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.EjecutivoOficina.CorreoElectronico']").InnerText;

            //INFORMACION CLIENTE
            string txtNumTicketCRM       = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.NumeroTicketCRM']").InnerText;
            string txtSinCuentaContrato  = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SinCuentaContrato']").InnerText;
            string txtCuentaContrato     = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.CuentaContrato']").InnerText;
            string txtNombre             = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Nombre']").InnerText;
            string txtNumSerieMedidor    = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.NumeroSerieMedidor']").InnerText;
            string txtCiudad             = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Ciudad']").InnerText;
            string txtDireccion          = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Direccion']").InnerText;
            string txtSeleccionarCliente = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SeleccionarCliente']").InnerText;

            //DATOS DE CONTACTO
            string txtNombreContacto  = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Contacto.NombreContacto']").InnerText;
            string txtCorreoContacto  = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Contacto.CorreoElectronico']").InnerText;
            string txtTelefonoFijo    = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Contacto.TelefonoFijo']").InnerText;
            string txtCelularContacto = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Contacto.Celular']").InnerText;
            string txtRegion          = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Region.Nombre']").InnerText;
            string txtComunas         = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Comunas.Comuna']").InnerText;

            //REQUERIMIENTO
            string txtContratistasOT    = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ContratistasOTMedidor.Nombre']").InnerText;
            string txtCorreoContratista = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ContratistasOTMedidor.CorreoElectronico']").InnerText;
            string txtMotivoOT          = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.MotivoOT.Motivo']").InnerText;
            string txtSubMotivoOT       = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SubMotivoOT.Submotivo']").InnerText;
            string txtComentarioSolici  = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ComentarioSolicitud']").InnerText;
            //string txtArchivoBase64 = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item").InnerText;
            //string txtArchivoNombre = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item").Attributes["FileName"].InnerText;

            //CAMPOS OBLIGATORIOS
            string txtFechaDeVisita     = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.FechadeVisita']").InnerText;
            string txtComentarioCierre = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ComentarioCierreSolicitud']").InnerText;

            
            bool tieneArchivo = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item") != null;
            XmlNodeList tieneArchivos = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item");

            if (tieneArchivo)
            {
                foreach (XmlNode item in tieneArchivos)
                {
                    string txtArchivoBase64 = item.InnerText;
                    string txtArchivoNombre = item.Attributes["FileName"].InnerText;

                    ViewData["txtArchivo"] += @"
                    <a download='" + txtArchivoNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtArchivoBase64 + @"' class='btn btn-primary btn-md' style='width:300px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtArchivoNombre + @"
                    </a>
                ";

                    txtArchivoBase64 = null;
                    txtArchivoNombre = null;
                } 
            }

            //Archivo cargado por contratista
            bool tieneArchivoContratista = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.RespaldoAtencion']/Items/Item") != null;
            //bool tieneArchivoContratista = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.RespaldoAtencion']").Count >= 0;
            XmlNodeList archivosContratistas = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.RespaldoAtencion']/Items/Item");

            if (tieneArchivoContratista)
            {
                foreach (XmlNode item in archivosContratistas)
                {
                    string txtContratistaBase64 = item.InnerText;
                    string txtArchivoContratista = item.Attributes["FileName"].InnerText;
                   
                    ViewData["txtArchivosCargados"] += @"
                    <a download='" + txtArchivoContratista + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtContratistaBase64 + @"' class='btn btn-primary btn-md' style='width:310px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtArchivoContratista + @"
                    </a>
                    ";

                    txtContratistaBase64 = null;
                    txtArchivoContratista = null;
                }                
            }

            //FORMATEAR FECHA SOLICITUD
            if (txtFechaSolicitud != string.Empty)
            {
                DateTime fechaInicio = DateTime.Parse(txtFechaSolicitud);
                ViewData["txtFechaSolicitud"] = fechaInicio.ToString("yyyy-MM-dd");
            }

            //FORMATEAR FECHA DE VISITA
            if (txtFechaDeVisita != string.Empty)
            {
                DateTime fechaVisita = DateTime.Parse(txtFechaDeVisita);
                ViewData["txtFechaDeVisita"] = fechaVisita.ToString("yyyy-MM-dd");
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
            ViewData["txtSubMotivosOT"] = txtSubMotivoOT;
            ViewData["txtComentarioSolicitud"] = txtComentarioSolici;
            
            ViewData["txtComentarioCierre"] = txtComentarioCierre;

            
            


            return View();
        }

        public ActionResult ResumenTratarCaso(int id)
        {
            string xml = ObtenerCaso(id);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            //Obtener valores del xml respuesta de Bizagi del numero de caso enviado

            //DATOS SOLICITANTE
            string txtFechaSolicitud = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.FechaSolicitud']").InnerText;
            string txtEjecutivo = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.EjecutivoOficina.Nombre']").InnerText;
            string txtNombreSolicitante = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SolicitanteOTMedidor.NombreSolicitante']").InnerText;
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
            string txtRegion = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Region.Nombre']").InnerText;
            string txtComunas = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Comunas.Comuna']").InnerText;

            //REQUERIMIENTO
            string txtContratistasOT = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ContratistasOTMedidor.Nombre']").InnerText;
            string txtCorreoContratista = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ContratistasOTMedidor.CorreoElectronico']").InnerText;
            string txtMotivoOT = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.MotivoOT.Motivo']").InnerText;
            string txtSubMotivoOT = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.SubMotivoOT.Submotivo']").InnerText;
            string txtComentarioSolici = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ComentarioSolicitud']").InnerText;
            //string txtArchivoBase64 = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item").InnerText;
            //string txtArchivoNombre = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item").Attributes["FileName"].InnerText;

            //CAMPOS OBLIGATORIOS
            string txtFechaDeVisita = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.FechadeVisita']").InnerText;
            string txtComentarioCierre = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.ComentarioCierreSolicitud']").InnerText;


            bool tieneArchivo = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item") != null;
            XmlNodeList tieneArchivos = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Archivo']/Items/Item");

            if (tieneArchivo)
            {
                foreach (XmlNode item in tieneArchivos)
                {
                    string txtArchivoBase64 = item.InnerText;
                    string txtArchivoNombre = item.Attributes["FileName"].InnerText;

                    ViewData["txtArchivo"] += @"
                    <a download='" + txtArchivoNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtArchivoBase64 + @"' class='btn btn-primary btn-md' style='width:300px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtArchivoNombre + @"
                    </a>
                ";

                    txtArchivoBase64 = null;
                    txtArchivoNombre = null;
                }
            }

            //Archivo cargado por contratista
            bool tieneArchivoContratista = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.RespaldoAtencion']/Items/Item") != null;
            //bool tieneArchivoContratista = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.RespaldoAtencion']").Count >= 0;
            XmlNodeList archivosContratistas = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.RespaldoAtencion']/Items/Item");

            if (tieneArchivoContratista)
            {
                foreach (XmlNode item in archivosContratistas)
                {
                    string txtContratistaBase64 = item.InnerText;
                    string txtArchivoContratista = item.Attributes["FileName"].InnerText;

                    ViewData["txtArchivosCargados"] += @"
                    <a download='" + txtArchivoContratista + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtContratistaBase64 + @"' class='btn btn-primary btn-md' style='width:310px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtArchivoContratista + @"
                    </a>
                    ";

                    txtContratistaBase64 = null;
                    txtArchivoContratista = null;
                }
            }

            //FORMATEAR FECHA SOLICITUD
            if (txtFechaSolicitud != string.Empty)
            {
                DateTime fechaInicio = DateTime.Parse(txtFechaSolicitud);
                ViewData["txtFechaSolicitud"] = fechaInicio.ToString("yyyy-MM-dd");
            }

            //FORMATEAR FECHA DE VISITA
            if (txtFechaDeVisita != string.Empty)
            {
                DateTime fechaVisita = DateTime.Parse(txtFechaDeVisita);
                ViewData["txtFechaDeVisita"] = fechaVisita.ToString("yyyy-MM-dd");
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
            ViewData["txtSubMotivosOT"] = txtSubMotivoOT;
            ViewData["txtComentarioSolicitud"] = txtComentarioSolici;

            ViewData["txtComentarioCierre"] = txtComentarioCierre;

            return View();
        }

        public string ObtenerCaso(int id)
        {
            string respuestaBizagi = string.Empty;

            try
            {
                int numCasoXML = id;
                BPMSEntity.EntityManagerSOASoapClient servicioQuery = new BPMSEntity.EntityManagerSOASoapClient();

                //Escribir log CSV
                UtilController.EscribirLog("Caso a trabajar", "ObtenerCaso", Convert.ToString(numCasoXML));
                //Fin CSV

                //Crear XML para obtener la información del caso seleccionado para trabajar

                string queryObtenerCaso = @"
                <BizAgiWSParam>
                    <CaseInfo>
                        <IdCase>" + id + @"</IdCase>
                    </CaseInfo>
                    <XPaths>
                        <XPath XPath=""OrdendeTrabajoMedidor.FechaSolicitud""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.SolicitanteOTMedidor.NombreSolicitante""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.EjecutivoOficina.CorreoElectronico""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.EjecutivoOficina.Nombre""/>
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
                        <XPath XPath=""OrdendeTrabajoMedidor.Region.Nombre""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Comunas.Comuna""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.ContratistasOTMedidor.Nombre""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.ContratistasOTMedidor.CorreoElectronico""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.MotivoOT.Motivo""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.SubMotivoOT.Submotivo""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.ComentarioSolicitud""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.Archivo""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.FechadeVisita""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.ComentarioCierreSolicitud""/>
                        <XPath XPath=""OrdendeTrabajoMedidor.RespaldoAtencion""/>
                    </XPaths>
                    </BizAgiWSParam>";

                //Escribir log CSV
                UtilController.EscribirLog("Campos solicitados", "ObtenerCaso", queryObtenerCaso);
                //Fin CSV

                respuestaBizagi = servicioQuery.getCaseDataUsingXPathsAsString(queryObtenerCaso);
                respuestaBizagi = respuestaBizagi.Replace("\n", "");
                respuestaBizagi = respuestaBizagi.Replace("\t", "");
                respuestaBizagi = respuestaBizagi.Replace("\r", "");

                //Escribir log CSV
                UtilController.EscribirLog("Respuesta", "ObtenerCaso", respuestaBizagi);
                //Fin CSV
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "ObtenerCaso", ex.Message);
                //Fin CSV
            }
            return (respuestaBizagi);
        }

        [HttpPost]
        public string ActualizarCaso(FormCollection collection, IEnumerable<HttpPostedFileBase> files)
        {
            string respuestaBizagi = string.Empty;
            DateTime fechaDeVisita = DateTime.MinValue;
            string IDUsuario = "";
            string idRegistro = "";
            try
            {
                if (System.Web.HttpContext.Current.Session["IDUsuario"] != null)
                {
                    IDUsuario = System.Web.HttpContext.Current.Session["IDUsuario"].ToString();
                    Convert.ToInt32(IDUsuario);
                    ViewData["IDUsuario"] = IDUsuario;
                }
                int numCaso          = Convert.ToInt32(collection["txtNumCaso"]);
                var fechaVisita      = collection["txtFechaDeVisita"];
                var archivoSoli      = collection["txtArchivoContratista"];
                var comentarioCierre = collection["txtComentarioCierre"];

                fechaDeVisita = Convert.ToDateTime(fechaVisita);
                fechaDeVisita.ToString("yyyy-MM-dd HH':'mm':'ss");

                // Conversión de archivos
                string archivosBase64 = "";
                string archivosNombres = "";
                string archivosRespaldos = "";
                foreach (string upload in Request.Files)
                {
                    if (Request.Files[upload].FileName != "")
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "./";
                        string path = Path.GetTempPath();
                        string filename = Request.Files[upload].FileName;
                        Request.Files[upload].SaveAs(Path.Combine(path, filename));
                        string archivoConvertido = ConversorBase64.convertirABase64(path + filename);
                        if (upload != null)
                        {
                            archivosBase64 = archivoConvertido;
                            archivosNombres = archivoConvertido;
                            archivosRespaldos += @"<File fileName='" + filename + @"'>" + archivoConvertido + @"</File>";
                        }
                    }
                }

                //LipigasEntityManagerSoa.EntityManagerSOASoapClient servicioQuery = new LipigasEntityManagerSoa.EntityManagerSOASoapClient();
                //DemoLipiEntity.EntityManagerSOASoapClient servicioQuery = new DemoLipiEntity.EntityManagerSOASoapClient();
                BPMSEntity.EntityManagerSOASoapClient servicioQuery = new BPMSEntity.EntityManagerSOASoapClient();

                //Escribir log CSV
                UtilController.EscribirLog("Caso a actualizar", "ActualizarCaso", Convert.ToString(numCaso));
                //Fin CSV

                //XML PARA ACTUALIZAR VALORES DEL CASO
                string queryActualizarCaso = @"<BizAgiWSParam>
                                                    <Entities>
                                                        <OrdendeTrabajoMedidor businessKey=""NroCaso='" + numCaso + @"'"">
                                                            <ComentarioCierreSolicitud>"+comentarioCierre+@"</ComentarioCierreSolicitud>
                                                            <FechadeVisita>"+fechaVisita+@"</FechadeVisita>
                                                        </OrdendeTrabajoMedidor>
                                                    </Entities>
                                               </BizAgiWSParam>";

                //Escribir log CSV
                UtilController.EscribirLog("Datos a actualizar", "ActualizarCaso", queryActualizarCaso);
                //Fin CSV

                respuestaBizagi = servicioQuery.saveEntityAsString(queryActualizarCaso);
                respuestaBizagi = respuestaBizagi.Replace("\n", "");
                respuestaBizagi = respuestaBizagi.Replace("\t", "");
                respuestaBizagi = respuestaBizagi.Replace("\r", "");

                //Obtener id del registro actualizado para ocuparlo como filtro en el metodo para actualizar archivo
                //Transformar respuesta STRING de Bizagi a XML para poder recorrer los nodos
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuestaBizagi);
                XmlNodeList rows = doc.GetElementsByTagName("OrdendeTrabajoMedidor");

                if (rows != null)
                {
                    foreach (XmlNode row in rows)
                    {
                        if(doc.GetElementsByTagName("OrdendeTrabajoMedidor")[0] != null)
                        {
                            idRegistro = doc.GetElementsByTagName("OrdendeTrabajoMedidor")[0].InnerText;
                        }
                    }
                }

                //Escribir log CSV
                UtilController.EscribirLog("Respuesta", "ActualizarCaso", respuestaBizagi);
                //Fin CSV
                System.Web.HttpContext.Current.Session["NroCasoTemp"] = numCaso;
                System.Web.HttpContext.Current.Session["IdRegistro"] = idRegistro;
            }
            catch(Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "ActualizarCaso", ex.Message);
                //Fin CSV
            }

            return (respuestaBizagi);
        }

        [HttpPost]
        public string ActualizarCasoArchivos(FormCollection collection, IEnumerable<HttpPostedFileBase> files)
        {
            string IDUsuario = "";
            string idRegistro = "";
            string respuestaBizagi = "";
            string archivoConvertido = "";
            try
            {
                if (System.Web.HttpContext.Current.Session["NroCasoTemp"] != null)
                {
                    if (System.Web.HttpContext.Current.Session["IdRegistro"] != null && System.Web.HttpContext.Current.Session["IDUsuario"] != null)
                    {
                        idRegistro = System.Web.HttpContext.Current.Session["IdRegistro"].ToString();
                        IDUsuario = System.Web.HttpContext.Current.Session["IDUsuario"].ToString();
                        Convert.ToInt32(IDUsuario);
                        ViewData["IDUsuario"] = IDUsuario;
                    }
                    int numCaso = Convert.ToInt32(System.Web.HttpContext.Current.Session["NroCasoTemp"]);
                    var archivoSoli = collection["txtArchivoContratista"];

                    // Conversión de archivos
                    string archivosBase64 = "";
                    string archivosNombres = "";
                    string archivosRespaldos = "";
                    foreach (string upload in Request.Files)
                    {
                        if (Request.Files[upload].FileName != "")
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "./";
                            string path = Path.GetTempPath();
                            string filename = Request.Files[upload].FileName;
                            Request.Files[upload].SaveAs(Path.Combine(path, filename));
                            archivoConvertido = ConversorBase64.convertirABase64(path + filename);
                            if (upload != null)
                            {
                                archivosBase64 = archivoConvertido;
                                archivosNombres = archivoConvertido;
                                archivosRespaldos += @"<File fileName=""" + filename + @""">" + archivoConvertido + @"</File>";
                                //archivosRespaldos += @"<File fileName='" + filename + @"'>" + archivoConvertido + @"</File>";
                            }
                        }
                    }
                    
                    BPMSEntity.EntityManagerSOASoapClient servicioQuery = new BPMSEntity.EntityManagerSOASoapClient();

                    //Escribir log CSV
                    UtilController.EscribirLog("Caso a actualizar", "ActualizarCasoArchivos", Convert.ToString(numCaso));
                    //Fin CSV

                    //XML PARA ACTUALIZAR VALORES DEL CASO
                    string queryActualizarCaso = @"<BizAgiWSParam>
                                                    <Entities idCase='"+ numCaso + @"'>
                                                        <OrdendeTrabajoMedidor key='"+ idRegistro + @"'>
                                                            <RespaldoAtencion>" + archivosRespaldos + @"</RespaldoAtencion>
                                                        </OrdendeTrabajoMedidor>
                                                    </Entities>
                                               </BizAgiWSParam>";

                    System.Web.HttpContext.Current.Session["NroCasoTemp"] = null;

                    //Escribir log CSV
                    queryActualizarCaso = queryActualizarCaso.Replace("\n", "");
                    queryActualizarCaso = queryActualizarCaso.Replace("\t", "");
                    queryActualizarCaso = queryActualizarCaso.Replace("\r", "");
                    UtilController.EscribirLog("Datos a actualizar", "ActualizarCasoArchivos", queryActualizarCaso);
                    //Fin CSV

                    respuestaBizagi = servicioQuery.saveEntityAsString(queryActualizarCaso);
                    respuestaBizagi = respuestaBizagi.Replace("\n", "");
                    respuestaBizagi = respuestaBizagi.Replace("\t", "");
                    respuestaBizagi = respuestaBizagi.Replace("\r", "");

                    //Escribir log CSV
                    UtilController.EscribirLog("Respuesta", "ActualizarCasoArchivos", respuestaBizagi);
                    //Fin CSV
                }
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "ActualizarCasoArchivos", ex.Message);
                //Fin CSV
            }

            return (respuestaBizagi);
        }

        [HttpPost]
        public string FinalizarCaso(FormCollection collection)
        {
            string respuestaBizagi = string.Empty;
            try
            {
                string IDUsuario = System.Web.HttpContext.Current.Session["IDUsuario"].ToString();
                int numCaso = Convert.ToInt32(collection["txtNumCaso"]);

                //Escribir log CSV
                UtilController.EscribirLog("Caso a finalizar", "FinalizarCaso", Convert.ToString(numCaso));
                //Fin CSV

                //LipigasWorkflowEngineSoa.WorkflowEngineSOASoapClient servicioQuery = new LipigasWorkflowEngineSoa.WorkflowEngineSOASoapClient();
                //DemoLipiWorkflow.WorkflowEngineSOASoapClient servicioQuery = new DemoLipiWorkflow.WorkflowEngineSOASoapClient();
                BPMSWorkflow.WorkflowEngineSOASoapClient servicioQuery = new BPMSWorkflow.WorkflowEngineSOASoapClient();



                //XML PARA ACTUALIZAR VALORES DEL CASO
                string queryFinalizarCaso = @"<BizAgiWSParam>
	                                                <domain>domain</domain>
	                                                <userName>admon</userName>
	                                                <ActivityData>
		                                                <radNumber>" + numCaso + @"</radNumber>
		                                                <taskName>Task4</taskName>
                                                        <taskId>2447</taskId>
	                                                </ActivityData>
                                                </BizAgiWSParam>";

                //Escribir log CSV
                UtilController.EscribirLog("Avanzar para finalizar", "FinalizarCaso", queryFinalizarCaso);
                //Fin CSV

                respuestaBizagi = servicioQuery.performActivityAsString(queryFinalizarCaso);
                respuestaBizagi = respuestaBizagi.Replace("\n", "");
                respuestaBizagi = respuestaBizagi.Replace("\t", "");
                respuestaBizagi = respuestaBizagi.Replace("\r", "");

                //Escribir log CSV
                UtilController.EscribirLog("Respuesta", "FinalizarCaso", respuestaBizagi);
                //Fin CSV
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "FinalizarCaso", ex.Message);
                //Fin CSV
            }

            return (respuestaBizagi);
        }
    }
}
