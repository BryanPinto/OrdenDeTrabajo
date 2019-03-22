using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

                //// Turno
                //int? caso = null;
                //if (txtNroCaso != "0")
                //    caso = int.Parse(txtNroCaso);

                //// Estado
                //int? estado = null;
                //if (txtEstado != "0")
                //    estado = int.Parse(txtEstado);


                #endregion

                #region Queries

                LipigasQuery.QueryFormSOASoapClient servicioQuery = new LipigasQuery.QueryFormSOASoapClient();

                int cantidadCasos = 20;
                string respuestaCasos = "";

                string queryCasos = @"
            <BizAgiWSParam>
                  <QueryParams>
                      <Internals>
                          <Internal Name='ProcessState' Include='true'>Running</Internal>
                          <Internal Name='ProcessState' Include='true'>Completed</Internal>
						  <Internal Name='ProcessState' Include='true'>Suspended</Internal>
						  <Internal Name='ProcessState' Include='true'>NotInitiated</Internal>
                      </Internals>
                      <XPaths>
                          <XPath Path='OrdendeTrabajoMedidor'></XPath>
                      </XPaths>
                  </QueryParams>
                  <Parameters>
                      <Parameter Name='pag'>1</Parameter>                        
                      <Parameter Name='PageSize'> "+ cantidadCasos+ @" </Parameter>    
                      <Parameter Name='idEnt'>10135</Parameter>
                  </Parameters>
              </BizAgiWSParam>";

                respuestaCasos = servicioQuery.QueryCasesAsString(queryCasos);
                //AHORA TRANSFORMAR RESPUESTACASOS A XML y LUEGO RECORRRER LA VARIABLE CONVERTIDA

                #endregion

                //#region Crear JSON
                List<List<string>> registros = new List<List<string>>();
                //foreach (CabeceraDocumento item in Rows)//rows del xml
                //{
                //    List<string> fila = new List<string>();
                //    // Fecha
                //    fila.Add(item.Fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

                //    // Turno
                //    if (item.ID_Turno != null)
                //        fila.Add(item.Turnos.NombreTurno);
                //    else
                //        fila.Add(string.Empty);

                //    // Operación
                //    fila.Add(item.TipoDocumento.Area.Replace("Refineria", "Refinería"));

                //    // Formulario
                //    fila.Add(item.TipoDocumento.NombreVisual);

                //    // Estado
                //    fila.Add(item.Estados.Nombre);
         
                //    // Agregar a lista
                //    registros.Add(fila);
                //}
                //datosJSON = JsonConvert.SerializeObject(registros);
                //#endregion


            }
            catch (Exception ex)
            {
                //Helper.escribirLog(Helper.GetCurrentMethod(), ex.Message);
            }
            return (datosJSON);
        }

    }
}
