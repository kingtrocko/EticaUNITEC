using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EticaUNITEC.Seguridad
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["error"]!=null)
            {
                lbl_error.Text = Session["error"].ToString();
            }
        }
    }
}