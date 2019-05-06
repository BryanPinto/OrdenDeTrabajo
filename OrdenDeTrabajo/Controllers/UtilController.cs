using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.IO;

namespace WebSolicitudes.Controllers
{
    public class UtilController : Controller
    {
        //
        // GET: /Util/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método para crear listas desplegables desde una paramétrica de Bizagi
        /// </summary>
        /// <param name="tabla">Nombre de tabla paramétrica</param>
        /// <param name="campoVisual">Campo que se va a mostrar como opción</param>
        /// <returns></returns>
        public static string ListarParametrica(string tabla, string campoVisual)
        {
            string lista = string.Empty;

            try
            {
                // XML Búsqueda
                string xmlGetEntities = @"
                    <BizAgiWSParam>
                        <EntityData>
                            <EntityName>" + tabla + @"</EntityName>
                            <Filters>
                                <![CDATA[dsbl" + tabla + @" = " + false + @"]]>
                            </Filters>
                        </EntityData>
                    </BizAgiWSParam>
                ";
                //Escribir log CSV
                EscribirLog("Listar motivo/submotivo", "ListarParametrica", xmlGetEntities);
                //Fin CSV

                //Escribir log con el xml creado como consulta de casos
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "tabla: " + tabla + "| " + "campoVisual: " + campoVisual + "| " + "XML a Bizagi: " + xmlGetEntities);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();

                // Abrir conexión a servicio web
                LipigasEntityManager.EntityManagerSOASoapClient servicioQuery = new LipigasEntityManager.EntityManagerSOASoapClient();
                // Buscar en Bizagi
                string respuesta = servicioQuery.getEntitiesAsString(xmlGetEntities);

                //Escribir log CSV
                EscribirLog("Respuesta", "ListarParametrica", respuesta);
                //Fin CSV

                //Escribir log con el xml creado como consulta de casos
                rutaLog = HttpRuntime.AppDomainAppPath;
                sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "Respuesta Bizagi: " + respuesta);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();

                // Convertir a XML
                XmlDocument doc = new XmlDocument();
                    doc.LoadXml(respuesta);

                    // Recorrer los resultados
                    foreach (XmlNode item in doc.SelectNodes("/BizAgiWSResponse/Entities/" + tabla))
                    {
                        // Obtener campos
                        string id = item.Attributes["key"].Value;
                        string campo = item.SelectSingleNode(campoVisual).InnerText;

                        // Crear opción
                        lista += "<option value='" + id + @"'>" + campo + @"</option>";
                    }

                //Escribir log CSV
                EscribirLog("Lista opciones generada", "ListarParametrica", lista);
                //Fin CSV

                //Escribir log con el xml creado como consulta de casos
                rutaLog = HttpRuntime.AppDomainAppPath;
                sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "Lista de opciones: " + lista);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();

            }
            catch (Exception ex)
            {
                //Escribir log CSV
                EscribirLog("ERROR", "ListarParametrica", ex.Message);
                //Fin CSV

                //Escribir log con el xml creado como consulta de casos
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "Error: " + ex.Message);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();
            }

            return (lista);
        }

        /// <summary>
        /// Método para rescatar el valor de un atributo, dado un atributo y valor por el cual se filtrará el registro
        /// </summary>
        /// <param name="tabla">Nombre de tabla a consultar</param>
        /// <param name="nomAtributoTabla">Atributo por el cual filtrar</param>
        /// <param name="valorEnviado">Valor enviado al atributo filtrado</param>
        /// <param name="valorObtenido">Valor obtenido de un atributo a elección</param>
        /// <returns></returns>
    public static string ObtenerAtributoParametricaByCod(string tabla, string nomAtributoTabla, string valorEnviado, string valorObtenido)
        {
            string lista = string.Empty;
            string campo = string.Empty;

            try
            {
                // XML Búsqueda
                string xmlGetEntities = @"
                    <BizAgiWSParam>
                        <EntityData>
                            <EntityName>" + tabla + @"</EntityName>
                            <Filters>
                                <![CDATA["+nomAtributoTabla +" = '" + valorEnviado+ @"']]>
                            </Filters>
                        </EntityData>
                    </BizAgiWSParam>
                ";

                //Escribir log CSV
                EscribirLog("Obtener atributos de parametricas", "ObtenerAtributoParametricaByCod", xmlGetEntities);
                //Fin CSV

                //Escribir log con el xml creado como consulta de casos
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                DateTime.Now.ToShortDateString() + " " +
                DateTime.Now.ToShortTimeString() + ": " +
                "tabla: " + tabla + "| " + "nomAtributoTabla: " + nomAtributoTabla + "| " + "valorEnviado: " + valorEnviado + "| " + "XML a Bizagi: " + xmlGetEntities);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();

                // Abrir conexión a servicio web
                LipigasEntityManager.EntityManagerSOASoapClient servicioQuery = new LipigasEntityManager.EntityManagerSOASoapClient();
                // Buscar en Bizagi
                string respuesta = servicioQuery.getEntitiesAsString(xmlGetEntities);

                //Escribir log CSV
                EscribirLog("Respuesta", "ObtenerAtributoParametricaByCod", respuesta);
                //Fin CSV

                //Escribir log con el xml creado como consulta de casos
                rutaLog = HttpRuntime.AppDomainAppPath;
                sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                DateTime.Now.ToShortDateString() + " " +
                DateTime.Now.ToShortTimeString() + ": " +
                "Respuesta Bizagi: " + respuesta);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();

                // Convertir a XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);
                
                
                campo = doc.SelectSingleNode("/BizAgiWSResponse/Entities/" + tabla + "/"+ valorObtenido).InnerText;

                //Escribir log CSV
                EscribirLog("Atributo rescatado", "ObtenerAtributoParametricaByCod", campo);
                //Fin CSV

                //Escribir log con el xml creado como consulta de casos
                rutaLog = HttpRuntime.AppDomainAppPath;
                sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                DateTime.Now.ToShortDateString() + " " +
                DateTime.Now.ToShortTimeString() + ": " +
                "Lista de opciones: " + lista);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();
                
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                EscribirLog("ERROR", "ObtenerAtributoParametricaByCod", ex.Message);
                //Fin CSV

                //Escribir log con el xml creado como consulta de casos
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                DateTime.Now.ToShortDateString() + " " +
                DateTime.Now.ToShortTimeString() + ": " +
                "Error: " + ex.Message);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();
            }
            return campo;
        }

        /// <summary>
        /// Método para escribir un log
        /// Se guardar dentro de un CSV con nombre LogPortal.CSV dentro de la carpeta del proyecto
        /// </summary>
        /// <param name="proceso">Nombre del proceso</param>
        /// <param name="metodo">Método actual</param>
        /// <param name="mensaje">Mensaje que se va a guardar</param>
        public static void EscribirLog(string proceso, string metodo, string mensaje)
        {
            try
            {
                // Crear CSV
                string rutaLog = HttpRuntime.AppDomainAppPath;
                //string nombreArchivo = "LogPortal.csv";
                string rutaCompleta = rutaLog + "LogPortal.csv";
                var csv = new StringBuilder();

                // Revisar si tiene cabecera
                string primeraLinea = string.Empty;
                bool existeArchivo = System.IO.File.Exists(rutaCompleta);
                if (existeArchivo)
                    primeraLinea = System.IO.File.ReadLines(rutaCompleta).FirstOrDefault();
                //Si cabecera no existe, crear con las siguientes columnas
                if (!existeArchivo || (existeArchivo && (primeraLinea == null || primeraLinea == string.Empty)))
                {
                    string cabecera =
                        string.Format("{0};{1};{2};{3};{4}"
                        , "Fecha"
                        , "Hora"
                        , "Proceso"
                        , "Metodo"
                        , "Mensaje"
                        );
                    csv.Append(cabecera);
                    System.IO.File.AppendAllText(rutaCompleta, csv.ToString());
                    csv.Clear();
                }

                // Si existe cabecera, escribir linea
                string nuevaLinea = Environment.NewLine +
                    string.Format("{0};{1};{2};{3};{4}"
                    , "\"" + DateTime.Now.ToShortDateString() + "\""
                    , "\"" + DateTime.Now.ToShortTimeString() + "\""
                    , "\"" + proceso + "\""
                    , "\"" + metodo + "\""
                    , "\"" + mensaje + "\""
                    );

                // Agregar a archivo
                csv.Append(nuevaLinea);
                System.IO.File.AppendAllText(rutaCompleta, csv.ToString());
                csv.Clear();
            }
            catch (Exception)
            {
            }
        }
    }
}
