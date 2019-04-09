using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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
    }
}
