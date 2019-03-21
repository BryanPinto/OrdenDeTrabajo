using System;
using System.Collections.Generic;
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

        //[HttpPost]
        //public string BusquedaCasos(FormCollection collection)
        //{
        //    string datosJSON = string.Empty;
        //    try
        //    {
        //        #region Agregar filtros y buscar
        //        // Variables
        //        string txtFechaDesde = collection["txtFechaDesde"];
        //        string txtFechaHasta = collection["txtFechaHasta"];
        //        string txtNroCaso = collection["txtNroCaso"];
        //        string txtEstado = collection["txtEstado"];

        //        // Fecha inicio
        //        DateTime? fechaInicio = null;
        //        if (txtFechaDesde != string.Empty)
        //            fechaInicio = DateTime.ParseExact(txtFechaDesde, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        //        // Fecha término
        //        DateTime? fechaTermino = null;
        //        if (txtFechaHasta != string.Empty)
        //            fechaTermino = DateTime.ParseExact(txtFechaHasta, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        //        // Turno
        //        int? caso = null;
        //        if (txtNroCaso != "0")
        //            caso = int.Parse(txtNroCaso);

        //        // Estado
        //        int? estado = null;
        //        if (txtEstado != "0")
        //            estado = int.Parse(txtEstado);

                
        //        #endregion

        //        #region Crear JSON
        //        List<List<string>> registros = new List<List<string>>();
        //        foreach (CabeceraDocumento item in cabeceras.OrderBy(o => o.Fecha))
        //        {
        //            List<string> fila = new List<string>();
        //            // Fecha
        //            fila.Add(item.Fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

        //            // Turno
        //            if (item.ID_Turno != null)
        //                fila.Add(item.Turnos.NombreTurno);
        //            else
        //                fila.Add(string.Empty);

        //            // Operación
        //            fila.Add(item.TipoDocumento.Area.Replace("Refineria", "Refinería"));

        //            // Formulario
        //            fila.Add(item.TipoDocumento.NombreVisual);

        //            // Estado
        //            fila.Add(item.Estados.Nombre);

        //            // Botones
        //            string btnEliminar = string.Empty;
        //            string btnReabrir = string.Empty;

        //            // Ver
        //            string btnVer = "<a href='" + Url.Action("Ingresar", "Operacion", new { id = item.ID }) + @"' class='btn btn-secondary btn-md center-block'>Ver</a>";

        //            // Eliminar
        //            string nombrePagina = Helper.GetNombrePagina(item.ID_TipoDocumento);
        //            if (item.Estados.ID != Helper.estadoAprobado && Helper.TienePermiso(nombrePagina, "Eliminar"))
        //                btnEliminar = "<a data-id='" + item.ID + @"' class='btn btn-secondary btn-md center-bloc btnEliminar'>Eliminar</a>";

        //            // Reabrir
        //            int idUsuario = int.Parse(System.Web.HttpContext.Current.Session["idUsuario"].ToString());
        //            if (item.Estados.ID == Helper.estadoAprobado && Helper.TienePermiso(nombrePagina, "Reabrir"))
        //                btnReabrir = "<a data-id='" + item.ID + @"' class='btn btn-secondary btn-md center-bloc btnReabrir'>Reabrir</a>";

        //            fila.Add(btnVer + btnEliminar + btnReabrir);

        //            // Agregar a lista
        //            registros.Add(fila);
        //        }
        //        datosJSON = JsonConvert.SerializeObject(registros);
        //        #endregion


        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.escribirLog(Helper.GetCurrentMethod(), ex.Message);
        //    }
        //    return (datosJSON);
        //}

    }
}
