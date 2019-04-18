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
            string listaMotivo = UtilController.ListarParametrica("MotivoOT", "Motivo");
            ViewData["txtMotivoSelect1"] = listaMotivo;

            string listaSubMotivo = UtilController.ListarParametrica("P_SubMotivoOT", "SubMotivo");
            ViewData["txtSubMotivoSelect1"] = listaSubMotivo;
            return View();
        }


        [HttpPost]
        public string BusquedaCasos(FormCollection collection)
        {
            string datosJSON = string.Empty;
            try
            {
                // Variables
                //string txtFechaDesde = collection["txtFechaDesde"];
                //string txtFechaHasta = collection["txtFechaHasta"];
                string txtNroCaso = collection["txtNroCaso"];
                string txtEstadoSelect = collection["txtEstadoSelect"];
                string txtMotivoSelect = collection["txtMotivoSelect"];
                string txtSubMotivoSelect = collection["txtSubMotivoSelect"];
                

                // NumCaso
                int caso;
                int? numeroCaso;
                bool conversionOK = Int32.TryParse(txtNroCaso, out caso);
                if(conversionOK)
                {
                    numeroCaso = caso;
                }
                else
                {
                    numeroCaso = null;
                }
                

                // Motivo
                int? motivo = null;
                if (Convert.ToInt32(txtMotivoSelect) != 0)
                    motivo = int.Parse(txtMotivoSelect);

                // Sub Motivo
                int? subMotivo = null;
                if (Convert.ToInt32(txtSubMotivoSelect) != 0)
                    subMotivo = int.Parse(txtSubMotivoSelect);

                // Estado
                int? estado = null;
                if (Convert.ToInt32(txtEstadoSelect) != 0)
                    estado = int.Parse(txtEstadoSelect);

                //// Fecha inicio
                //DateTime? fechaInicioResumen = null;
                //if (txtFechaDesde != string.Empty)
                //    fechaInicio = DateTime.ParseExact(txtFechaDesde, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                //// Fecha término
                //DateTime? fechaTerminoResumen = null;
                //if (txtFechaHasta != string.Empty)
                //    fechaTermino = DateTime.ParseExact(txtFechaHasta, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                
                

                LipigasQuery.QueryFormSOASoapClient servicioQuery = new LipigasQuery.QueryFormSOASoapClient();

                int cantidadCasos = 20;
                string respuestaCasos = "";

                //Crear XML de consulta de casos (id de proceso 26 = OrdendeTrabajoMedidor)
                string queryCasos = @"
                <BizAgiWSParam>
	              <userName>admon</userName>
	              <domain>domain</domain>
                  <QueryParams>
                      <Internals>";
                //if (Convert.ToInt32(txtEstadoSelect) == 1)
                //{
                //    queryCasos += @"<Internal Name='ProcessState' Include='true'>Initiated</Internal>";
                //}
                if (Convert.ToInt32(txtEstadoSelect) == 2)
                {
                    queryCasos += @"<Internal Name='ProcessState' Include='true'>Running</Internal>";
                }
                //if (Convert.ToInt32(txtEstadoSelect) == 3)
                //{
                //    queryCasos += @"<Internal Name='ProcessState' Include='true'>Suspended</Internal>";
                //}
                //if (Convert.ToInt32(txtEstadoSelect) == 4)
                //{
                //    queryCasos += @"<Internal Name='ProcessState' Include='true'>Aborted</Internal>";
                //}
                if (Convert.ToInt32(txtEstadoSelect) == 5)
                {
                    queryCasos += @"<Internal Name='ProcessState' Include='true'>Completed</Internal>";
                }
                else
                {
                    queryCasos += @"<Internal Name='ProcessState' Include='true'>Initiated</Internal>
                                    <Internal Name='ProcessState' Include='true'>Running</Internal>
                                    <Internal Name='ProcessState' Include='true'>Suspended</Internal>
                                    <Internal Name='ProcessState' Include='true'>Aborted</Internal>
                                    <Internal Name='ProcessState' Include='true'>Completed</Internal>";
                }
                queryCasos += @"
                          <Internal Name='idWfClass' Include='true'>26</Internal>
                      </Internals>
                      <XPaths>";
                if (Convert.ToInt32(txtMotivoSelect) != 0)
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.MotivoOT.Cod' Include='true'>" + motivo + "</XPath>";
                }
                else
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.MotivoOT.Motivo' Include='true'></XPath>";
                }
                if (Convert.ToInt32(txtSubMotivoSelect) != 0)
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.SubMotivoOT.Cod' Include='true'>" + subMotivo + "</XPath>";
                }
                else
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.SubMotivoOT.SubMotivo' Include='true'></XPath>";
                }
                if(Convert.ToInt32(numeroCaso) != 0)
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.NroCaso' Include='true'>" + numeroCaso + "</XPath>";
                }
                else
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.NroCaso' Include='true'></XPath>";
                }
                queryCasos += @"
                          <XPath Path='OrdendeTrabajoMedidor.Fechaasignacion' Include='true'>
                            <From>01/01/1900</From>        
                          </XPath>
                      </XPaths>
                  </QueryParams>
                  <Parameters>
                      <Parameter Name ='pag'>1</Parameter>
                       <Parameter Name='PageSize'>" + cantidadCasos + @"</Parameter>
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
                        if (row.SelectNodes("Column[@Name='ORDENDETRABAJOMEDI_NROCASO']")[0] != null && row.SelectNodes("Column[@Name='ORDENDETRABAJOMEDI_NROCASO']")[0].InnerText != string.Empty)
                        {
                            numCaso = row.SelectNodes("Column[@Name='ORDENDETRABAJOMEDI_NROCASO']")[0].InnerText;
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
                            //if (Convert.ToUInt32(estadoCaso) == 1)
                            //{
                            //    estadoTexto = "Iniciado";
                            //}
                            if (Convert.ToUInt32(estadoCaso) == 2)
                            {
                                estadoTexto = "En proceso";
                            }
                            //if (Convert.ToUInt32(estadoCaso) == 3)
                            //{
                            //    estadoTexto = "Suspendido";
                            //}
                            //if (Convert.ToUInt32(estadoCaso) == 4)
                            //{
                            //    estadoTexto = "Abortado";
                            //}
                            if (Convert.ToInt32(estadoCaso) == 5)
                            {
                                estadoTexto = "Completado";
                            }
                            fila.Add(estadoTexto);
                        }

                        //OBTENER FECHA ASIGNACION
                        if (row.SelectNodes("Column[@Name='ORDENDETRA_FECHAASIGNACION']")[0] != null && row.SelectNodes("Column[@Name='ORDENDETRA_FECHAASIGNACION']")[0].InnerText != string.Empty)
                        {
                            var fechaAsignacion = row.SelectNodes("Column[@Name='ORDENDETRA_FECHAASIGNACION']")[0].InnerText;
                            DateTime fecha = Convert.ToDateTime(fechaAsignacion);
                            var fechaFinal = fecha.ToString("dd-MM-yyyy");
                            fila.Add(fechaFinal);
                        }

                        //OBTENER MOTIVO OT MEDIDOR
                        if (row.SelectNodes("Column[@Name='MOTIVOOT_MOTIVO']")[0] != null && row.SelectNodes("Column[@Name='MOTIVOOT_MOTIVO']")[0].InnerText != string.Empty)
                        {
                            var motivoOT = row.SelectNodes("Column[@Name='MOTIVOOT_MOTIVO']")[0].InnerText;
                            fila.Add(motivoOT);
                        }
                        //OBTENER MOTIVO OT MEDIDOR FILTRADO POR CODIGO
                        if (row.SelectNodes("Column[@Name='MOTIVOOT_COD']")[0] != null && row.SelectNodes("Column[@Name='MOTIVOOT_COD']")[0].InnerText != string.Empty)
                        {
                            string motivoFiltrado = UtilController.ObtenerAtributoParametricaByCod("MotivoOT", "Cod", row.SelectNodes("Column[@Name='MOTIVOOT_COD']")[0].InnerText,"Motivo");
                            fila.Add(motivoFiltrado);
                        }
                        
                        //OBTENER SUB MOTIVO OT MEDIDOR
                        if (row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_SUBMOTIVO']")[0] != null && row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_SUBMOTIVO']")[0].InnerText != string.Empty)
                        {
                            var subMotivoOT = row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_SUBMOTIVO']")[0].InnerText;
                            fila.Add(subMotivoOT);
                        }

                        //OBTENER SUB MOTIVO OT MEDIDOR POR CODIGO
                        if (row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_COD']")[0] != null && row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_COD']")[0].InnerText != string.Empty)
                        {
                            string subMotivoFiltrado = UtilController.ObtenerAtributoParametricaByCod("P_SubMotivoOT", "Cod", row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_COD']")[0].InnerText, "SubMotivo");
                            fila.Add(subMotivoFiltrado);
                        }
                        fila.Add(@"<a href='" + Url.Action("ResumenTratarCaso", "TratarCaso", new { id = numCaso }) + @"' class='btn btn-default btn-md center-block'>Ver resumen</a>");


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

        [HttpPost]
        public string CasosPendientes(FormCollection collection)
        {
            string datosJSON = string.Empty;
            try
            {

                #region Agregar filtros y buscar
                // Variables
                string txtFechaDesde = collection["txtFechaDesde"];
                string txtFechaHasta = collection["txtFechaHasta"];
                string txtNroCaso = collection["txtNroCaso"];
                string txtMotivoSelect = collection["txtMotivoSelect"];
                string txtSubMotivoSelect = collection["txtSubMotivoSelect"];

                // Fecha inicio
                DateTime? fechaInicio = null;
                if (txtFechaDesde != string.Empty)
                    fechaInicio = DateTime.ParseExact(txtFechaDesde, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Fecha término
                DateTime? fechaTermino = null;
                if (txtFechaHasta != string.Empty)
                    fechaTermino = DateTime.ParseExact(txtFechaHasta, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Motivo
                int? motivo = null;
                if (Convert.ToInt32(txtMotivoSelect) != 0)
                    motivo = int.Parse(txtMotivoSelect);

                // Sub Motivo
                int? subMotivo = null;
                if (Convert.ToInt32(txtSubMotivoSelect) != 0)
                    subMotivo = int.Parse(txtSubMotivoSelect);



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
                          <Internal Name='idWfClass' Include='true'>26</Internal>
                      </Internals>
                      <XPaths>";
                if (Convert.ToInt32(txtMotivoSelect) != 0)
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.MotivoOT.Cod' Include='true'>" + motivo + "@</XPath>";
                }
                else
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.MotivoOT.Motivo' Include='true'></XPath>";
                }
                if (Convert.ToInt32(txtSubMotivoSelect) != 0)
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.SubMotivoOT.Cod' Include='true'>" + subMotivo + "@</XPath>";
                }
                else
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.SubMotivoOT.SubMotivo' Include='true'></XPath>";
                }
                queryCasos += @"                           
                           <XPath Path='OrdendeTrabajoMedidor.NroCaso' Include='true'></XPath>
                          <XPath Path='OrdendeTrabajoMedidor.Fechaasignacion' Include='true'>";
                if (txtFechaDesde != null)
                {
                    queryCasos += @"<From>" + fechaInicio + @"</From>";
                }
                else
                {
                    queryCasos += @"<From>01/01/1900</From>";
                }
                if (txtFechaHasta != null)
                {
                    queryCasos += @"<To>" + fechaTermino + @"</To>";
                }
                else { }
                queryCasos += @"        
                          </XPath>
                      </XPaths>
                  </QueryParams>
                  <Parameters>
                      <Parameter Name ='pag'>1</Parameter>
                       <Parameter Name='PageSize'>" + cantidadCasos + @"</Parameter>
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
                        //var numCaso = "";
                        //if (row.SelectNodes("Column[@Name='IDCASE']")[0] != null && row.SelectNodes("Column[@Name='ORDENDETRAB_FECHASOLICITUD']")[0].InnerText != string.Empty)
                        //{
                        //    numCaso = row.SelectNodes("Column[@Name='IDCASE']")[0].InnerText;
                        //    if (txtNroCaso == string.Empty)
                        //    {
                        //        valido = true;
                        //        fila.Add(numCaso);
                        //    }
                        //    else if (txtNroCaso != string.Empty && txtNroCaso == numCaso)
                        //    {
                        //        valido = true;
                        //        fila.Add(numCaso);
                        //    }
                        //}
                        var numCasoXML = "";
                        if (row.SelectNodes("Column[@Name='ORDENDETRABAJOMEDI_NROCASO']")[0] != null && row.SelectNodes("Column[@Name='ORDENDETRABAJOMEDI_NROCASO']")[0].InnerText != string.Empty)
                        {
                            numCasoXML = row.SelectNodes("Column[@Name='ORDENDETRABAJOMEDI_NROCASO']")[0].InnerText;
                            valido = true;
                            fila.Add(numCasoXML);
                        }

                        ////OBTENER ESTADO DEL CASO
                        //if (row.SelectNodes("Column[@Name='IDCASESTATE']")[0] != null)
                        //{
                        //    var estadoCaso = row.SelectNodes("Column[@Name='IDCASESTATE']")[0].InnerText;
                        //    var estadoTexto = "";
                        //    if (Convert.ToUInt32(estadoCaso) == 2)
                        //    {
                        //        estadoTexto = "En proceso";
                        //    }
                        //    if (Convert.ToInt32(estadoCaso) == 5)
                        //    {
                        //        estadoTexto = "Completado";
                        //    }
                        //    fila.Add(estadoTexto);
                        //}

                        //OBTENER FECHA ASIGNACION
                        if (row.SelectNodes("Column[@Name='ORDENDETRA_FECHAASIGNACION']")[0] != null && row.SelectNodes("Column[@Name='ORDENDETRA_FECHAASIGNACION']")[0].InnerText != string.Empty)
                        {
                            var fechaAsignacion = row.SelectNodes("Column[@Name='ORDENDETRA_FECHAASIGNACION']")[0].InnerText;
                            DateTime fecha = Convert.ToDateTime(fechaAsignacion);
                            var fechaFinal = fecha.ToString("dd-MM-yyyy");
                            fila.Add(fechaFinal);
                        }

                        //OBTENER MOTIVO OT MEDIDOR
                        if (row.SelectNodes("Column[@Name='MOTIVOOT_MOTIVO']")[0] != null && row.SelectNodes("Column[@Name='MOTIVOOT_MOTIVO']")[0].InnerText != string.Empty)
                        {
                            var motivoOT = row.SelectNodes("Column[@Name='MOTIVOOT_MOTIVO']")[0].InnerText;
                            fila.Add(motivoOT);
                        }
                        //OBTENER MOTIVO OT MEDIDOR FILTRADO POR CODIGO
                        if (row.SelectNodes("Column[@Name='MOTIVOOT_COD']")[0] != null && row.SelectNodes("Column[@Name='MOTIVOOT_COD']")[0].InnerText != string.Empty)
                        {
                            string motivoFiltrado = UtilController.ObtenerAtributoParametricaByCod("MotivoOT", "Cod", row.SelectNodes("Column[@Name='MOTIVOOT_COD']")[0].InnerText, "Motivo");
                            fila.Add(motivoFiltrado);
                        }

                        //OBTENER SUB MOTIVO OT MEDIDOR
                        if (row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_SUBMOTIVO']")[0] != null && row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_SUBMOTIVO']")[0].InnerText != string.Empty)
                        {
                            var subMotivoOT = row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_SUBMOTIVO']")[0].InnerText;
                            fila.Add(subMotivoOT);
                        }

                        //OBTENER SUB MOTIVO OT MEDIDOR POR CODIGO
                        if (row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_COD']")[0] != null && row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_COD']")[0].InnerText != string.Empty)
                        {
                            string subMotivoFiltrado = UtilController.ObtenerAtributoParametricaByCod("P_SubMotivoOT", "Cod", row.SelectNodes("Column[@Name='P_SUBMOTIVOOT_COD']")[0].InnerText, "SubMotivo");
                            fila.Add(subMotivoFiltrado);
                        }
                        fila.Add(@"<a href='" + Url.Action("TratarCaso", "TratarCaso", new { id = numCasoXML }) + @"' class='btn btn-default btn-md center-block'>Tratar</a>");


                        // Agregar a lista FORMA CORRECTA
                        if (valido)
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
