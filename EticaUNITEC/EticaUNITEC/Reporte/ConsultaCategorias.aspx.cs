using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EticaUNITEC.Reporte
{
    public partial class ConsultaCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //var hola = RadGrid1.MasterTableView.Items["CategoriaId"].Selected;
            ////MasterTableView.Items.FindControl("lblMatricula")
            
            //var hh =RadGrid1.SelectedItems;
            
            //int i = 0;

            foreach (Telerik.Web.UI.GridDataItem dataItem in RadGrid1.MasterTableView.Items)
            {
                if (dataItem.Selected == true)
                {
                    int ID = Convert.ToInt32(dataItem.GetDataKeyValue("CategoriaId"));
                    
                }
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

       
    }
}