using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSolicitudes.Controllers
{
    public class TratarCasoController : Controller
    {
        //
        // GET: /TratarCaso/

        public ActionResult TratarCaso()
        {
            return View();
        }

        //public string ObtenerCaso(int numCaso)
        //{
        //    LipigasEntityManager.EntityManagerSOASoapClient servicioQuery = new LipigasEntityManager.EntityManagerSOASoapClient();

        //    string respuestaBizagi = "";

        //    //Crear XML para obtener la información del caso seleccionado para trabajar
        //    string queryObtenerCaso = @"
        //        <BizAgiWSParam>
	       //         <EntityData>
		      //          <EntityName>OrdendeTrabajoMedidor</EntityName>
	       //         </EntityData>
        //        </BizAgiWSParam>";

        //    //CAMBIAR CUANDO SE INCLUYA NUMERO DE CASO A BIZAGI
        //    //string queryObtenerCaso = @"
        //    //    <BizAgiWSParam>
	       //    //     <EntityData>
		      //    //      <EntityName>OrdendeTrabajoMedidor</EntityName>
		      //    //      <Filters>
			     //    //       <![NCaso = ]>
		      //    //      </Filters>
	       //    //     </EntityData>
        //    //    </BizAgiWSParam>";

        //    respuestaBizagi = servicioQuery.getEntitiesAsString(queryObtenerCaso);
        //    respuestaBizagi = respuestaBizagi.Replace("\n", "");
        //    respuestaBizagi = respuestaBizagi.Replace("\t", "");
        //    respuestaBizagi = respuestaBizagi.Replace("\r", "");
        //}

    }
}
