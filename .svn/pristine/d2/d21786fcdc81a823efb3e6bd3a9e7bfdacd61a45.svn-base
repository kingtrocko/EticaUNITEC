using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EticaUNITEC.Seguridad
{
    public partial class Logout : EticaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

        }

        protected void Timer1_Tick1(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Write("<script>window.open('../Seguridad/Login.aspx','_parent');</script>");
            //Response.Redirect("../Seguridad/Login.aspx");
        }
    }
}