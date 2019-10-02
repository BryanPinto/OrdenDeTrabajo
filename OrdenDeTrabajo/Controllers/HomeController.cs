using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Xml;

namespace WebSolicitudes.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index(int? IDUsuario, int? estado)
        {
            string usuario = "";
            if (IDUsuario.HasValue)
            {
                System.Web.HttpContext.Current.Session["IDUsuario"] = IDUsuario;
            }
            else
            {
                usuario = System.Web.HttpContext.Current.Session["IDUsuario"].ToString();
            }

            string listaMotivo = UtilController.ListarParametrica("MotivoOT", "Motivo");
            ViewData["txtMotivoSelect1"] = listaMotivo;

            string listaSubMotivo = UtilController.ListarParametricaConPadre("P_SubMotivoOT", "SubMotivo","Motivo");//El 3er parametro corresponde a la relacion con la tabla Motivo
            ViewData["txtSubMotivoSelect1"] = listaSubMotivo;

            if (estado == 1)
                ViewData["estado"] = "1";
            else if (estado == 0)
            {
                ViewData["estado"] = "0";
            }
            return View();
        }

        //
        // GET: /Home/Login

        public ActionResult Login(int? estado)
        {

            if (estado == 1)
                ViewData["estado"] = "1";
            else if (estado == 0)
            {
                ViewData["estado"] = "0";
            }
            return View();
        }

        //
        // GET: /Home/Login
        [HttpPost]
        public ActionResult Login(FormCollection collection, int? estado)
        {
            string datosJSON = string.Empty;
            string respuestaCasos = "";
            string idUsuario = "";
            string txtVacio = collection["txtVacio"];
            FormCollection nulo = new FormCollection();
            try
            {
                //Obtener valores ingresados en formulario de login
                string txtCorreo = collection["txtCorreo"];
                string txtPass = collection["txtPass"];

                if(txtCorreo != null)
                {
                    txtCorreo.Trim();
                }
                if (txtPass != null)
                {
                    txtPass.Trim();
                }

                //if (txtCorreo == "1" && txtPass == "2")
                //{
                //    idUsuario = "1";
                //    Convert.ToInt32(idUsuario);
                //}
                //else
                //{

                    //Escribir log CSV
                    UtilController.EscribirLog("Credenciales ingresadas", "Login", "Correo: " + txtCorreo + ", Clave: " + txtPass);
                    //Fin CSV

                    //Consultar a tabla si correo y password coinciden
                    string queryLogin = @"<BizAgiWSParam>
	                                    <EntityData>
		                                    <EntityName>ContratistasOTMedidor</EntityName>
		                                    <Filters>
			                                    <![CDATA[CorreoElectronico = '" + txtCorreo + @"' AND Pass = '" + txtPass + @"']]>
		                                    </Filters>
	                                    </EntityData>
                                    </BizAgiWSParam>";

                    //Escribir log CSV
                    UtilController.EscribirLog("Consultar credenciales", "Login", queryLogin);
                    //Fin CSV

                    //LipigasEntityManagerSoa.EntityManagerSOASoapClient servicioQuery = new LipigasEntityManagerSoa.EntityManagerSOASoapClient();
                    //DemoLipiEntity.EntityManagerSOASoapClient servicioQuery = new DemoLipiEntity.EntityManagerSOASoapClient();
                    BPMSEntity.EntityManagerSOASoapClient servicioQuery = new BPMSEntity.EntityManagerSOASoapClient();

                    respuestaCasos = servicioQuery.getEntitiesAsString(queryLogin);
                    respuestaCasos = respuestaCasos.Replace("\n", "");
                    respuestaCasos = respuestaCasos.Replace("\t", "");
                    respuestaCasos = respuestaCasos.Replace("\r", "");

                    //Escribir log CSV
                    UtilController.EscribirLog("Respuesta", "Login", respuestaCasos);
                    //Fin CSV

                    //Transformar respuesta STRING de Bizagi a XML para poder recorrer los nodos
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(respuestaCasos);

                    string correo = "";
                    string password = "";
                    //Obtener correo y pass de respuesta
                    if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/ContratistasOTMedidor/CorreoElectronico").InnerText != null)
                    {
                        correo = doc.SelectSingleNode("/BizAgiWSResponse/Entities/ContratistasOTMedidor/CorreoElectronico").InnerText;
                    }
                    if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/ContratistasOTMedidor/Pass").InnerText != null)
                    {
                        password = doc.SelectSingleNode("/BizAgiWSResponse/Entities/ContratistasOTMedidor/Pass").InnerText;
                    }

                    //Escribir log CSV
                    UtilController.EscribirLog("Credenciales rescatadas", "Login", "Correo: " + correo + ", Clave: " + password);
                    //Fin CSV

                    if (txtCorreo == correo && txtPass == password)
                    {
                        // Recorrer los resultados
                        foreach (XmlNode item in doc.SelectNodes("/BizAgiWSResponse/Entities/ContratistasOTMedidor"))
                        {
                            // Obtener campos
                            idUsuario = item.Attributes["key"].Value;
                            Convert.ToInt32(idUsuario);
                        }
                        //Url.Action("ListarCasos", "CasosPendientes", new { collection = txtVacio, IDUsuario = idUsuario });
                    }
                //}
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "Login", ex.Message);
                //Fin CSV                

                return RedirectToAction("Login", "Home", new { estado = 0 });
            }

            //return Index(Convert.ToInt32(idUsuario), 1);
            return RedirectToAction("Index", "Home", new { IDUsuario = idUsuario, estado = 1 });
        }
        //
        // GET: /Home/CerrarSesion

        public ActionResult CerrarSesion()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home"); ;
        }

        //
        // GET: /Home/Error404

        public ActionResult Error404()
        {
            return View();
        }

    }
}
