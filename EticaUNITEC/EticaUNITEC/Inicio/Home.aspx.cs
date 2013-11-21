using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EticaUNITEC.Home
{
    public partial class Home : EticaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (!currentUser.Equals(""))
            {
                
                string [] name = currentUser.UsuarioNombre.Split(' ');
                userSpot.InnerText = name[0];
                userSpot.Attributes.Add("class", "warning");
            }
        }
    }
}