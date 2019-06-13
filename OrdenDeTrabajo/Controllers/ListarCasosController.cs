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

            string listaSubMotivo = UtilController.ListarParametricaConPadre("P_SubMotivoOT", "SubMotivo", "Motivo");//El 3er parametro corresponde a la relacion con la tabla Motivo
            ViewData["txtSubMotivoSelect1"] = listaSubMotivo;
            return View();
        }


        [HttpPost]
        public string BusquedaCasos(FormCollection collection)
        {
            string datosJSON = string.Empty;
            //string usuario = Convert.ToString(IDUsuario);
            string IDUsuario = "";
            
            try
            {
                IDUsuario = System.Web.HttpContext.Current.Session["IDUsuario"].ToString();
                int UsuarioLogueado = Convert.ToInt32(IDUsuario);
                ViewData["IDUsuario"] = UsuarioLogueado;
                //usuario = System.Web.HttpContext.Current.Session["IDUsuario"].ToString(); //Obtener usuario logueado por variable session, definida en el método CasosPendientes
                // Variables
                //string txtFechaDesde = collection["txtFechaDesde"];
                //string txtFechaHasta = collection["txtFechaHasta"];
                string txtNroCaso = collection["txtNroCaso"];
                //string txtEstadoSelect = collection["txtEstadoSelect"];
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
                //int? estado = null;
                //if (Convert.ToInt32(txtEstadoSelect) != 0)
                //    estado = int.Parse(txtEstadoSelect);

                //// Fecha inicio
                //DateTime? fechaInicioResumen = null;
                //if (txtFechaDesde != string.Empty)
                //    fechaInicio = DateTime.ParseExact(txtFechaDesde, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                //// Fecha término
                //DateTime? fechaTerminoResumen = null;
                //if (txtFechaHasta != string.Empty)
                //    fechaTermino = DateTime.ParseExact(txtFechaHasta, "yyyy-MM-dd", CultureInfo.InvariantCulture);


                
                BPMSQuery.QueryFormSOASoapClient servicioQuery = new BPMSQuery.QueryFormSOASoapClient();

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
                ////if (Convert.ToInt32(txtEstadoSelect) == 2)
                ////{
                ////    queryCasos += @"<Internal Name='ProcessState' Include='true'>Running</Internal>";
                ////}
                //if (Convert.ToInt32(txtEstadoSelect) == 3)
                //{
                //    queryCasos += @"<Internal Name='ProcessState' Include='true'>Suspended</Internal>";
                //}
                //if (Convert.ToInt32(txtEstadoSelect) == 4)
                //{
                //    queryCasos += @"<Internal Name='ProcessState' Include='true'>Aborted</Internal>";
                //}
                //if (Convert.ToInt32(txtEstadoSelect) == 5)
                //{
                //    queryCasos += @"<Internal Name='ProcessState' Include='true'>Completed</Internal>";
                //}
                //else
                //{
                //    queryCasos += @"<Internal Name='ProcessState' Include='true'>Completed</Internal>
                //                        <Internal Name='ProcessState' Include='true'>Aborted</Internal>";
                //}
                queryCasos += @"<Internal Name='ProcessState' Include='true'>Completed</Internal>
                          <Internal Name='idWfClass' Include='true'>26</Internal>
                      </Internals>
                      <XPaths>
                        <XPath Path= 'OrdendeTrabajoMedidor.ContratistasOTMedidor' Include='true'>" + UsuarioLogueado + "</XPath>";
                //ESTA LINEA CORRESPONDE AL FILTRO POR USUARIO
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

                //Escribir log CSV
                UtilController.EscribirLog("Listar casos historicos", "BusquedaCasos", queryCasos);
                //Fin CSV

                respuestaCasos = servicioQuery.QueryCasesAsString(queryCasos);
                respuestaCasos = respuestaCasos.Replace("\n", "");
                respuestaCasos = respuestaCasos.Replace("\t", "");
                respuestaCasos = respuestaCasos.Replace("\r", "");

                //Escribir log CSV
                UtilController.EscribirLog("Respuesta", "BusquedaCasos", respuestaCasos);
                //Fin CSV

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
                            //if (Convert.ToUInt32(estadoCaso) == 2)
                            //{
                            //    estadoTexto = "En proceso";
                            //}
                            //if (Convert.ToUInt32(estadoCaso) == 3)
                            //{
                            //    estadoTexto = "Suspendido";
                            //}
                            //if (Convert.ToInt32(estadoCaso) == 4)
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

                    //Escribir log CSV
                    UtilController.EscribirLog("Caso rescatado", "BusquedaCasos", datosJSON);
                    //Fin CSV
                }
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "BusquedaCasos", ex.Message);
                //Fin CSV
            }
            return (datosJSON);
        }

        [HttpPost]
        public string CasosPendientes(FormCollection collection)
        {
            string datosJSON = string.Empty;
            //string txtVacio = collection["txtVacio"];
            string IDUsuario = "";
            try
            {
                IDUsuario = System.Web.HttpContext.Current.Session["IDUsuario"].ToString();
                int UsuarioLogueado = Convert.ToInt32(IDUsuario);
                ViewData["IDUsuario"] = UsuarioLogueado;
                //string IDUsuario = System.Web.HttpContext.Current.Session["IDUsuario"].ToString();
                //if (estado == 1)
                //    ViewData["estado"] = "1";
                //else if (estado == 0)
                //{
                //    ViewData["estado"] = "0";
                //}

                

                #region Agregar filtros y buscar
                // Variables
                string txtFechaDesde = collection["txtFechaDesde"];
                string txtFechaHasta = collection["txtFechaHasta"];
                string txtNroCaso = collection["txtNroCaso"];
                string txtMotivoSelect = collection["txtMotivoSelect"];
                string txtSubMotivoSelect = collection["txtSubMotivoSelect"];

                // Fecha inicio
                DateTime fechaInicio = DateTime.MinValue;
                if (txtFechaDesde != string.Empty)
                    DateTime.TryParse(collection["txtFechaDesde"], out fechaInicio);
                //fechaInicio = DateTime.ParseExact(txtFechaDesde, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Fecha término
                DateTime fechaTermino = DateTime.MinValue;
                if (txtFechaHasta != string.Empty)
                    DateTime.TryParse(collection["txtFechaHasta"], out fechaTermino);
                //fechaTermino = DateTime.ParseExact(txtFechaHasta, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                //// Número caso
                //int? numCaso = null;
                //if (Convert.ToInt32(txtNroCaso) != 0)
                //    numCaso = int.Parse(txtNroCaso);

                // NumCaso
                int caso;
                int? numeroCaso;
                bool conversionOK = Int32.TryParse(txtNroCaso, out caso);
                if (conversionOK)
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



                #endregion

                #region Queries
                
                BPMSQuery.QueryFormSOASoapClient servicioQuery = new BPMSQuery.QueryFormSOASoapClient();

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
                      <XPaths>
                        <XPath Path= 'OrdendeTrabajoMedidor.ContratistasOTMedidor' Include='true'>" + UsuarioLogueado + "</XPath>";//ESTA LINEA CORRESPONDE AL FILTRO POR USUARIO
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
                if (numeroCaso != 0)
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.NroCaso' Include='true'>"+ numeroCaso + "</XPath>";
                }
                else
                {
                    queryCasos += @"<XPath Path='OrdendeTrabajoMedidor.NroCaso' Include='true'></XPath>";
                }
                queryCasos += @"                    
                          <XPath Path='OrdendeTrabajoMedidor.Fechaasignacion' Include='true'>";
                if (fechaInicio != DateTime.MinValue)
                {
                    queryCasos += @"<From>" + fechaInicio.Day + '/' + fechaInicio.Month + '/' + fechaInicio.Year + "</From>";
                }
                else
                {
                    queryCasos += @"<From>01/01/1900</From>";
                }
                if (fechaTermino != DateTime.MinValue)
                {
                    queryCasos += @"<To>" + fechaTermino.Day + '/' + fechaTermino.Month + '/' + fechaTermino.Year + "</To>";
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

                //Escribir log CSV
                UtilController.EscribirLog("Listar casos pendientes", "CasosPendientes", queryCasos);
                //Fin CSV

                respuestaCasos = servicioQuery.QueryCasesAsString(queryCasos);
                respuestaCasos = respuestaCasos.Replace("\n", "");
                respuestaCasos = respuestaCasos.Replace("\t", "");
                respuestaCasos = respuestaCasos.Replace("\r", "");

                //Escribir log CSV
                UtilController.EscribirLog("Respuesta", "CasosPendientes", respuestaCasos);
                //Fin CSV

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

                    //Escribir log CSV
                    UtilController.EscribirLog("Caso rescatado", "CasosPendientes", datosJSON);
                    //Fin CSV
                }
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "CasosPendientes", ex.Message);
                //Fin CSV
            }
            return (datosJSON);
        }

    }
}
