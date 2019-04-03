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
            string txtFechaSolicitud = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.FechaSolicitud']").InnerText;
            string txtEjecutivo = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.EjecutivoOficina']").InnerText;
            string txtNombreSolicitante = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='OrdendeTrabajoMedidor.Nombre']").InnerText;




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
