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

                // Abrir conexión a servicio web
                BPMSEntity.EntityManagerSOASoapClient servicioQuery = new BPMSEntity.EntityManagerSOASoapClient();

                // Buscar en Bizagi
                string respuesta = servicioQuery.getEntitiesAsString(xmlGetEntities);

                //Escribir log CSV
                EscribirLog("Respuesta", "ListarParametrica", respuesta);
                //Fin CSV

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

            }
            catch (Exception ex)
            {
                //Escribir log CSV
                EscribirLog("ERROR", "ListarParametrica", ex.Message);
                //Fin CSV
            }

            return (lista);
        }

        /// <summary>
        /// Método para crear listas desplegables desde una paramétrica de Bizagi
        /// </summary>
        /// <param name="tabla">Nombre de tabla paramétrica</param>
        /// <param name="campoVisual">Campo que se va a mostrar como opción</param>
        /// <returns></returns>
        public static string ListarParametricaConPadre(string tabla, string campoVisual, string padre)
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
                EscribirLog("Listar motivo/submotivo", "ListarParametricaConPadre", xmlGetEntities);
                //Fin CSV

                // Abrir conexión a servicio web
                BPMSEntity.EntityManagerSOASoapClient servicioQuery = new BPMSEntity.EntityManagerSOASoapClient();

                // Buscar en Bizagi
                string respuesta = servicioQuery.getEntitiesAsString(xmlGetEntities);

                //Escribir log CSV
                EscribirLog("Respuesta", "ListarParametricaConPadre", respuesta);
                //Fin CSV

                // Convertir a XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);

                // Recorrer los resultados
                foreach (XmlNode item in doc.SelectNodes("/BizAgiWSResponse/Entities/" + tabla))
                {
                    // Obtener campos
                    string id = item.Attributes["key"].Value;
                    string campo = item.SelectSingleNode(campoVisual).InnerText;
                    string campoPadre = item.SelectSingleNode(padre).InnerText;

                    string motivoRelacion = campoPadre.Substring(0, campoPadre.Length-1);

                    // Crear opción
                    lista += "<option data-father='" + motivoRelacion + @"' value='" + id + @"'>" + campo + @"</option>";
                }

                //Escribir log CSV
                EscribirLog("Lista opciones generada", "ListarParametricaConPadre", lista);
                //Fin CSV

            }
            catch (Exception ex)
            {
                //Escribir log CSV
                EscribirLog("ERROR", "ListarParametrica", ex.Message);
                //Fin CSV
            }

            return (lista);
        }

        /// <summary>
        /// Método para rescatar el valor de un atributo, dado un atributo y valor por el cual se filtrará el registro.
        /// ESTOS CAMPOS VIENEN DESDE EL CONTROLADOR LISTARCASOS, EN LOS METODOS CASOSPENDIENTES Y BUSQUEDADECASOS, ANTES DE AÑADIR LA FILA DE MOTIVO FILTRADO POR CODIGO
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

                // Abrir conexión a servicio web
                BPMSEntity.EntityManagerSOASoapClient servicioQuery = new BPMSEntity.EntityManagerSOASoapClient();

                // Buscar en Bizagi
                string respuesta = servicioQuery.getEntitiesAsString(xmlGetEntities);

                //Escribir log CSV
                EscribirLog("Respuesta", "ObtenerAtributoParametricaByCod", respuesta);
                //Fin CSV

                // Convertir a XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);
                
                
                campo = doc.SelectSingleNode("/BizAgiWSResponse/Entities/" + tabla + "/"+ valorObtenido).InnerText;

                //Escribir log CSV
                EscribirLog("Atributo rescatado", "ObtenerAtributoParametricaByCod", campo);
                //Fin CSV
                
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                EscribirLog("ERROR", "ObtenerAtributoParametricaByCod", ex.Message);
                //Fin CSV
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
