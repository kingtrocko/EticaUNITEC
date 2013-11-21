using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EticaUNITEC.Reporte
{
    public partial class ReporteAlumnos : EticaPage
    {
        string localId;
        protected void Page_Load(object sender, EventArgs e)
        {
            localId = Request["CarreraId"];
            
        }
    }
}