using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EticaUNITEC.Clases;

namespace EticaUNITEC
{
    public class EticaPage : System.Web.UI.Page
    {
        protected Usuario currentUser;
        protected string privilegio = "";

        public EticaPage():base()
        {
            
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            currentUser = Usuario.Current;
            if (currentUser == null)
                Response.Redirect("~/Seguridad/Login.aspx?status=1");
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (currentUser == null)
            {
                Response.Redirect("~/Seguridad/Login.aspx?status=1");
            }
            if (privilegio!="" && !currentUser.TienePrivilegio(privilegio))
            {
                Session["error"] = "Usted no tiene privilegios para accesar esta pagina";
                Response.Redirect("~/Seguridad/Error.aspx");
            }
        }

    }
}