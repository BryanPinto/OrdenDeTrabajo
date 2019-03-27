using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace WebSolicitudes.Controllers
{
    public class ListarCasosController : Controller
    {
        //
        // GET: /ListarCasos/

        public ActionResult ListarCasos()
        {
            return View();
        }

        public void escribirLog(string solicitud, string mensaje)
        {
            try
            {
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "Solicitud " + solicitud + ": " +
                          mensaje);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();
            }
            catch (Exception)
            {
            }
        }

        [HttpPost]
        public string BusquedaCasos(FormCollection collection)
        {
            string datosJSON = string.Empty;
            try
            {
                #region Agregar filtros y buscar
                // Variables
                string txtFechaDesde = collection["txtFechaDesde"];
                string txtFechaHasta = collection["txtFechaHasta"];
                string txtNroCaso = collection["txtNroCaso"];
                string txtEstado = collection["txtEstado"];

                // Fecha inicio
                DateTime? fechaInicio = null;
                if (txtFechaDesde != string.Empty)
                    fechaInicio = DateTime.ParseExact(txtFechaDesde, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Fecha término
                DateTime? fechaTermino = null;
                if (txtFechaHasta != string.Empty)
                    fechaTermino = DateTime.ParseExact(txtFechaHasta, "yyyy-MM-dd", CultureInfo.InvariantCulture);


                // Estado
                int? estado = null;
                if (Convert.ToInt32(txtEstado) != 0)
                    estado = int.Parse(txtEstado);
                

                #endregion

                #region Queries

                LipigasQuery.QueryFormSOASoapClient servicioQuery = new LipigasQuery.QueryFormSOASoapClient();

                int cantidadCasos = 20;
                string respuestaCasos = "";

                //Crear XML de consulta de casos (id de proceso 26 = OrdendeTrabajoMedidor)
                string queryCasos = @"
                <BizAgiWSParam>
	              <userName>admon</userName>
	              <domain>domain</domain>
                  <QueryParams>
                      <Internals>
                          <Internal Name='ProcessState' Include='true'>Running</Internal>
                          <Internal Name='ProcessState' Include='true'>Completed</Internal>
                          <Internal Name='idWfClass' Include='true'>26</Internal>
                      </Internals>
                      <XPaths>
                          <XPath Path='OrdendeTrabajoMedidor.MotivosOTMedidor.Motivo' Include='true'>
                          </XPath>
                          <XPath Path='OrdendeTrabajoMedidor.FechaSolicitud' Include='true'>";
                if (txtFechaDesde != null)
                {
                    queryCasos += @"<From>" + fechaInicio + @"</From>";
                }
                else
                {
                    queryCasos += @"<From>01/01/1900</From>";
                }
                if(txtFechaHasta != null)
                {
                    queryCasos += @"<To>" + fechaTermino + @"</To>";
                }
                else{}
                      queryCasos += @"        
                          </XPath>
                      </XPaths>
                  </QueryParams>
                  <Parameters>
                      <Parameter Name ='pag'>1</Parameter>
                       <Parameter Name='PageSize'>"+cantidadCasos+@"</Parameter>
                    </Parameters>
                </BizAgiWSParam>";

                respuestaCasos = servicioQuery.QueryCasesAsString(queryCasos);
                respuestaCasos = respuestaCasos.Replace("\n", "");
                respuestaCasos = respuestaCasos.Replace("\t", "");
                respuestaCasos = respuestaCasos.Replace("\r", "");

                //Escribir log con el xml creado como consulta de casos
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "XML: " + queryCasos + "| " + "Respuesta Bizagi: " + respuestaCasos);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();

                //Transformar respuesta STRING de Bizagi a XML para poder recorrer los nodos
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuestaCasos);
                XmlNodeList rows = doc.GetElementsByTagName("Row");

                #endregion

                //#region Crear JSON
                List<List<string>> registros = new List<List<string>>();
                if (rows != null)
                {
                    foreach (XmlNode row in rows)
                    {
                        List<string> fila = new List<string>();
                        bool valido = false;
                        //OBTENER NUMERO DE CASO
                        var numCaso = "";
                        if (row.SelectNodes("Column[@Name='IDCASE']")[0] != null && row.SelectNodes("Column[@Name='ORDENDETRAB_FECHASOLICITUD']")[0].InnerText != string.Empty)
                        {
                            numCaso = row.SelectNodes("Column[@Name='IDCASE']")[0].InnerText;
                            if (txtNroCaso == string.Empty)
                            {
                                valido = true;
                                fila.Add(numCaso);
                            }
                            else if (txtNroCaso != string.Empty && txtNroCaso == numCaso)
                            {
                                valido = true;
                                fila.Add(numCaso);
                            }
                        }
                        
                        //OBTENER ESTADO DEL CASO
                        if (row.SelectNodes("Column[@Name='IDCASESTATE']")[0] != null)
                        {
                            var estadoCaso = row.SelectNodes("Column[@Name='IDCASESTATE']")[0].InnerText;
                            var estadoTexto = "";
                            if (Convert.ToUInt32(estadoCaso) == 2)
                            {
                                estadoTexto = "En proceso";
                            }
                            if (Convert.ToInt32(estadoCaso) == 5)
                            {
                                estadoTexto = "Completado";
                            }
                            fila.Add(estadoTexto);
                        }

                        //OBTENER FECHA SOLICITUD
                        if (row.SelectNodes("Column[@Name='ORDENDETRAB_FECHASOLICITUD']")[0] != null && row.SelectNodes("Column[@Name='ORDENDETRAB_FECHASOLICITUD']")[0].InnerText != string.Empty)
                        {
                            var fechaSolicitud = row.SelectNodes("Column[@Name='ORDENDETRAB_FECHASOLICITUD']")[0].InnerText;
                            DateTime fecha = Convert.ToDateTime(fechaSolicitud);
                            var fechaFinal = fecha.ToString("dd-MM-yyyy");
                            fila.Add(fechaFinal);
                        }

                        //OBTENER MOTIVO OT MEDIDOR
                        if (row.SelectNodes("Column[@Name='MOTIVOSOTMEDIDOR_MOTIVO']")[0] != null)
                        {
                            var motivoOT = row.SelectNodes("Column[@Name='MOTIVOSOTMEDIDOR_MOTIVO']")[0].InnerText;
                            fila.Add(motivoOT);
                        }
                        fila.Add("Sub motivo");
                        fila.Add(@"<a href='" + Url.Action("TratarCaso", "TratarCaso", new { id = numCaso }) + @"' class='btn btn-default btn-md center-block'>Tratar</a>");


                        // Agregar a lista FORMA CORRECTA
                        if(valido)
                            registros.Add(fila);

                        
                    }
                    datosJSON = JsonConvert.SerializeObject(registros);

                    //Escribir log con el JSON serializado para enviar como filtro de busqueda y agregar los casos a la grilla
                    rutaLog = HttpRuntime.AppDomainAppPath;
                    sb = new StringBuilder();
                    sb.Append(Environment.NewLine +
                              DateTime.Now.ToShortDateString() + " " +
                              DateTime.Now.ToShortTimeString() + ": " +
                              "JSON: " + datosJSON);
                    System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                    sb.Clear();
                }
            }
            catch (Exception ex)
            {
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "Error: " + ex.Message);
                System.IO.File.AppendAllText(rutaLog + "Log-Errores.txt", sb.ToString());
                sb.Clear();
            }
            return (datosJSON);
        }

    }
}
