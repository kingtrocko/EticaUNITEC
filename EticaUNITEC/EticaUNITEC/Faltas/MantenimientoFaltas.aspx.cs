using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EticaUNITEC;

namespace EticaUNITEC
{
    public partial class MantenimientoFaltas : EticaPage
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCuenta.Focus();
                cmbCategoria.SelectedIndex = 0;
                
                txtFecha.Text = DateTime.Now.ToShortDateString();
                cargarNumeroArticulo();

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                try
                {
                    CargarCombos(con);
                    CargarIncisoDescripcion(con);
                }
                catch (Exception ex)
                {
                    Session["error"] = ex.StackTrace;
                    Response.Redirect("~/Seguridad/Error.aspx");
                }
                con.Close();
            }
            if (Request.Params["id"] != null && ViewState["FaltaId"]==null)
            {
                ViewState["FaltaId"] = Request.Params["id"].ToString();
                CargarDatos();
            }
            CargarEvidencias();
            SetPrivilegiosToActualUsuer();
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Insertar Faltas"))
            {
                btnGuardar.Visible = false;
            }

        }

        void CargarEvidencias()
        {
            SqlConnection con = new SqlConnection(connectionString);
           
            try
            {
                string sql = @"SELECT EvidenciaId,EvidenciaDescripcion
                               FROM Evidencias
                               WHERE FaltaId=@falta";
                con.Open();

                int faltaid = (ViewState["FaltaId"]!=null)?int.Parse(ViewState["FaltaId"].ToString()):0;
                
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable datos = new DataTable();
                adp.SelectCommand.Parameters.Add(new SqlParameter("falta",faltaid));
                adp.Fill(datos);
                evidencias.DataSource = datos;
                evidencias.DataBind();

                ViewState["Evidencias"] = datos;

            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            finally
            {
                con.Close();
            }
        }

        void CargarCombos(SqlConnection con)
        {
            string sql = @"SELECT * FROM Carreras";

            SqlDataAdapter adpCarrera = new SqlDataAdapter(sql, con);
            DataTable dtCarrera = new DataTable();
            adpCarrera.Fill(dtCarrera);
            cmbCarrera.DataSource = dtCarrera;
            cmbCarrera.DataValueField = "CarreraId";
            cmbCarrera.DataTextField = "CarreraNombre";
            cmbCarrera.DataBind();

            sql = @"SELECT * FROM Categorias";
            SqlDataAdapter adpCategorias = new SqlDataAdapter(sql, con);
            DataTable dtCategorias = new DataTable();
            adpCategorias.Fill(dtCategorias);
            cmbCategoria.DataSource = dtCategorias;
            cmbCategoria.DataValueField = "CategoriaId";
            cmbCategoria.DataTextField = "CategoriaNombre";
            cmbCategoria.DataBind();

            sql = @"SELECT * FROM Sanciones";
            SqlDataAdapter adpSancion = new SqlDataAdapter(sql, con);
            DataTable dtSancion = new DataTable();
            adpSancion.Fill(dtSancion);
            cmbTipoSancion.DataSource = dtSancion;
            cmbTipoSancion.DataValueField = "SancionId";
            cmbTipoSancion.DataTextField = "SancionNombre";
            cmbTipoSancion.DataBind();

        }
        void CargarDatos()
        {
            string faltaId = ViewState["FaltaId"].ToString();
            
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();            
            try
            {
                string sql = @"SELECT *
                               FROM Faltas
                               WHERE FaltaId='" + faltaId + "'";

                SqlDataAdapter adpFaltas = new SqlDataAdapter(sql, con);
                DataTable dtFaltas = new DataTable();
                adpFaltas.Fill(dtFaltas);
                txtFecha.Text = ((DateTime)dtFaltas.Rows[0]["FaltaFecha"]).ToString("dd/MM/yyyy");
                txtNumeroActa.Text = dtFaltas.Rows[0]["FaltaNumeroActa"].ToString();
                txtFaltaTitulo.Text = dtFaltas.Rows[0]["FaltaTitulo"].ToString();
                cmbCategoria.SelectedValue = dtFaltas.Rows[0]["CategoriaId"].ToString();
                txtDecripcionFalta.Text = dtFaltas.Rows[0]["FaltaDescripcion"].ToString();

                CargarIncisoDescripcion(con);

                string incisoId = dtFaltas.Rows[0]["IncisoId"].ToString();
                sql = @"SELECT *
                               FROM Incisos
                               WHERE IncisoId='" + incisoId + "'";

                SqlDataAdapter adpIncisos = new SqlDataAdapter(sql, con);
                DataTable dtIncisos = new DataTable();
                adpIncisos.Fill(dtIncisos);
                cmbTipoIncisoDescripcion.SelectedValue = dtIncisos.Rows[0]["IncisoId"].ToString();
                txtIncisoLetra.Text = dtIncisos.Rows[0]["IncisoLetra"].ToString();
                txtIncisoDescripcion.Text = dtIncisos.Rows[0]["IncisoContenido"].ToString();
                


                string articuloId = dtIncisos.Rows[0]["ArticuloId"].ToString();
                sql = @"SELECT *
                               FROM Articulos
                               WHERE ArticuloId='" + articuloId + "'";

                SqlDataAdapter adpArticulos = new SqlDataAdapter(sql, con);
                DataTable dtArticulos = new DataTable();
                adpArticulos.Fill(dtArticulos);
                txtArticuloNumero.Text = dtArticulos.Rows[0]["ArticuloNumero"].ToString();

                cmbTipoSancion.SelectedValue = dtFaltas.Rows[0]["SancionId"].ToString();
                txtTiempoSancion.Text = dtFaltas.Rows[0]["FaltaSancionTiempo"].ToString();
                txtDescripcionSancion.Text = dtFaltas.Rows[0]["FaltaSancionDescripcion"].ToString();


                string alumnoId = dtFaltas.Rows[0]["AlumnoId"].ToString();
                ViewState["AlumnoId"] = alumnoId;
                sql = @"SELECT *
                        FROM Alumnos
                        WHERE AlumnoId='" + alumnoId + "'";
                SqlDataAdapter adpAlumno = new SqlDataAdapter(sql, con);
                DataTable dtAlumno = new DataTable();
                adpAlumno.Fill(dtAlumno);
                txtCuenta.Text = dtAlumno.Rows[0]["AlumnoCuenta"].ToString();
                txtNombre.Text = dtAlumno.Rows[0]["AlumnoNombre"].ToString();
                cmbCarrera.SelectedValue = dtAlumno.Rows[0]["CarreraId"].ToString();
                string genero = dtAlumno.Rows[0]["AlumnoGenero"].ToString();
                if (genero == "F")
                    radioBtnGenero.SelectedIndex = 0;
                else
                    radioBtnGenero.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }

        void cargarNumeroArticulo()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = @"SELECT MAX(FaltaId)
                               FROM Faltas";
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string acta = dt.Rows[0][0].ToString();
                if (acta == "")
                    txtNumeroActa.Text = "1";
                else
                    txtNumeroActa.Text = acta;
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }

        void buscarAlumno()
        {
            if (txtCuenta.Text == "")
                return;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Alumnos 
                               WHERE AlumnoCuenta ='" + txtCuenta.Text + "'";
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);

                if (dt.Rows.Count > 0) //existe
                {
                    ViewState["AlumnoId"] = dt.Rows[0]["AlumnoId"].ToString();
                    txtNombre.Text = dt.Rows[0]["AlumnoNombre"].ToString();
                    string algo = dt.Rows[0]["CarreraId"].ToString(); 
                    cmbCarrera.SelectedValue = dt.Rows[0]["CarreraId"].ToString();
                    string genero = dt.Rows[0]["AlumnoGenero"].ToString();
                    if (genero == "F")
                        radioBtnGenero.SelectedIndex = 0;
                    else
                        radioBtnGenero.SelectedIndex = 1;
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }

        void CargarIncisoDescripcion(SqlConnection con)
        {

                string cat = cmbCategoria.SelectedValue;

                string sql = @"SELECT * 
	                            FROM Incisos i
		                            INNER JOIN Articulos a
	                            on i.ArticuloId = a.ArticuloId 
                               WHERE CategoriaId ='" + cat + "'"; //TiposIncisos
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                //adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);
                cmbTipoIncisoDescripcion.DataSource = dt;
                cmbTipoIncisoDescripcion.DataValueField = "IncisoId";
                cmbTipoIncisoDescripcion.DataTextField = "IncisoDescripcion";
                cmbTipoIncisoDescripcion.DataBind();

            if (dt.Rows.Count > 0)
            {
                txtArticuloNumero.Text = dt.Rows[0]["ArticuloNumero"].ToString();
                txtIncisoLetra.Text = dt.Rows[0]["IncisoLetra"].ToString();
                txtIncisoDescripcion.Text = dt.Rows[0]["IncisoContenido"].ToString();
            }
        }

        void cargarIncisos()
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string inciso = cmbTipoIncisoDescripcion.SelectedValue;
                string sql = @"SELECT * 
	                            FROM Incisos i
		                            INNER JOIN Articulos a
	                            on i.ArticuloId = a.ArticuloId 
                               WHERE IncisoId ='" + inciso + "'";
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);
                
                txtArticuloNumero.Text = dt.Rows[0]["ArticuloNumero"].ToString();
                txtIncisoLetra.Text = dt.Rows[0]["IncisoLetra"].ToString();
                txtIncisoDescripcion.Text = dt.Rows[0]["IncisoContenido"].ToString();

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }

        protected void cmbTipoIncisoDescripcion_TextChanged(object sender, EventArgs e)
        {
            int a = cmbTipoIncisoDescripcion.SelectedIndex;
            string z = cmbTipoIncisoDescripcion.SelectedValue;
            string w = cmbTipoIncisoDescripcion.SelectedItem.Text;
            cargarIncisos();
        }

        protected void cmbCategoria_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                CargarIncisoDescripcion(con);
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!currentUser.ValidarPrivilegio("Insertar Faltas"))
                return;

            bool exitoooooooo = true;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                guardarAlumno(con,trans);
                guargarFalta(con,trans);
                guargarEvidencias(con, trans);
                trans.Commit();
                limpiar();   
            }
            catch (Exception ex)
            {
                exitoooooooo = false;
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                
            }
            con.Close();

            if(!exitoooooooo)
                Response.Redirect("~/Seguridad/Error.aspx");
            else
                Response.Redirect("ListaFaltas.aspx");
        }

        void guardarAlumno(SqlConnection con, SqlTransaction trans)
        {
            if (ViewState["AlumnoId"] == null) //hacer un insert
            {
                string sql = @"INSERT INTO Alumnos(AlumnoNombre,AlumnoCuenta,AlumnoGenero,CarreraId) 
                               VALUES (@nombre, @cuenta, @genero, @carreraId)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("nombre", txtNombre.Text));
                cmd.Parameters.Add(new SqlParameter("cuenta", txtCuenta.Text));
                cmd.Parameters.Add(new SqlParameter("genero", radioBtnGenero.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("carreraId", cmbCarrera.SelectedValue));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                string lol = "SELECT @@identity as Codigo";
                SqlDataAdapter adp = new SqlDataAdapter(lol, con);
                DataTable tab = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(tab);

                ViewState["AlumnoId"] = tab.Rows[0]["Codigo"].ToString();
            }
            else //hacer update
            {
                string alumnoID = ViewState["AlumnoId"].ToString();
                string sql = @"UPDATE Alumnos
                               SET AlumnoNombre=@nombre,AlumnoCuenta=@cuenta,AlumnoGenero=@genero, CarreraId=@carreraId
                               WHERE AlumnoId=@alumnoId ";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("nombre", txtNombre.Text));
                cmd.Parameters.Add(new SqlParameter("cuenta", txtCuenta.Text));
                cmd.Parameters.Add(new SqlParameter("genero", radioBtnGenero.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("carreraId", cmbCarrera.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("alumnoId", alumnoID));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }

        }

        void guargarFalta(SqlConnection con, SqlTransaction trans)
        {
            
            string alumnoID = ViewState["AlumnoId"].ToString();
            
            bool faltaArchivada = false;

            if (ViewState["FaltaId"] == null) //hacer un insert
            {
                string sql = @"INSERT INTO Faltas(AlumnoId,CategoriaId,IncisoId,SancionId,FaltaSancionTiempo,FaltaTitulo,
                                              FaltaSancionDescripcion,UsuarioId,FaltaFecha,FaltaArchivada,FaltaNumeroActa,FaltaDescripcion) 
                               VALUES (@AlumnoId,@CategoriaId,@IncisoId,@SancionId,@FaltaSancionTiempo,@FaltaTitulo,
                                      @FaltaSancionDescripcion,@UsuarioId,@FaltaFecha,@FaltaArchivada,@FaltaNumeroActa,@FaltaDescripcion)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("AlumnoId", alumnoID));
                cmd.Parameters.Add(new SqlParameter("CategoriaId", cmbCategoria.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("IncisoId", cmbTipoIncisoDescripcion.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("SancionId", cmbTipoSancion.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("FaltaSancionTiempo", txtTiempoSancion.Text));
                cmd.Parameters.Add(new SqlParameter("FaltaSancionDescripcion", txtDescripcionSancion.Text));
                cmd.Parameters.Add(new SqlParameter("UsuarioId", currentUser.UsuarioID));
                cmd.Parameters.Add(new SqlParameter("FaltaFecha", DateTime.Now)); //Convert.ToDateTime(txtFecha.Text)
                cmd.Parameters.Add(new SqlParameter("FaltaArchivada", faltaArchivada));
                cmd.Parameters.Add(new SqlParameter("FaltaNumeroActa", Convert.ToInt32(txtNumeroActa.Text)));
                cmd.Parameters.Add(new SqlParameter("FaltaTitulo", txtFaltaTitulo.Text));
                cmd.Parameters.Add(new SqlParameter("FaltaDescripcion", txtDecripcionFalta.Text));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                string lol = "SELECT @@identity as Codigo";
                SqlDataAdapter adp = new SqlDataAdapter(lol, con);
                DataTable tab = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(tab);
                ViewState["FaltaId"] = tab.Rows[0][0];
                
            }
            else //hacer update
            {
                string faltaID = ViewState["FaltaId"].ToString();
                string fechaActual = DateTime.Now.ToShortDateString();

                string sql = @"UPDATE Faltas
                               SET AlumnoId=@AlumnoId,CategoriaId=@CategoriaId,IncisoId=@IncisoId,SancionId=@SancionId,
                                   FaltaSancionTiempo=@FaltaSancionTiempo,FaltaTitulo=@FaltaTitulo,
                                   FaltaSancionDescripcion=@FaltaSancionDescripcion,UsuarioId=@UsuarioId,FaltaFecha=@FaltaFecha,
                                   FaltaArchivada=@FaltaArchivada,FaltaNumeroActa=@FaltaNumeroActa,
                                   FaltaDescripcion=@FaltaDescripcion 
                               WHERE FaltaId=@FaltaId";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("AlumnoId", alumnoID));
                cmd.Parameters.Add(new SqlParameter("CategoriaId", cmbCategoria.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("IncisoId", cmbTipoIncisoDescripcion.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("SancionId", cmbTipoSancion.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("FaltaSancionTiempo", txtTiempoSancion.Text));
                cmd.Parameters.Add(new SqlParameter("FaltaTitulo", txtFaltaTitulo.Text));
                cmd.Parameters.Add(new SqlParameter("FaltaSancionDescripcion", txtDescripcionSancion.Text));
                cmd.Parameters.Add(new SqlParameter("UsuarioId", currentUser.UsuarioID));
                cmd.Parameters.Add(new SqlParameter("FaltaFecha", Convert.ToDateTime(fechaActual)));
                cmd.Parameters.Add(new SqlParameter("FaltaArchivada", faltaArchivada));
                cmd.Parameters.Add(new SqlParameter("FaltaNumeroActa", txtNumeroActa.Text));                
                cmd.Parameters.Add(new SqlParameter("FaltaId", faltaID));
                cmd.Parameters.Add(new SqlParameter("FaltaDescripcion", txtDecripcionFalta.Text));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
        }
        
        void guargarEvidencias(SqlConnection con, SqlTransaction trans)
        {
            DataTable dtEvidencias = (DataTable)ViewState["Evidencias"];
            
            int count = 2;
            if (dtEvidencias.Rows.Count >= 0)
                count += dtEvidencias.Rows.Count;
            while (true)
            {
                string fileName = "evidencias_itemPlaceholderContainer:File" + count;
                //Reviso que exita evidecias:File{count} en el request
                //Si no esta es porque no hay mas archivos para subir
                if (Request.Files[fileName] != null)
                {
                    //Instanciar buffer para guardar en base de datod
                    byte[] buffer = new byte[Request.Files[fileName].ContentLength];
                    if (buffer.Length != 0)
                    {
                        //Leo el archivo al buffer
                        Request.Files[fileName].InputStream.Read(buffer, 0, buffer.Length);
                        //Obtengo la descripcion posteada
                        string descr = Request.Params[fileName + ":Text"];
                        if (descr == null)
                            descr = "Archivo" + count;
                        //Obtengo el nombre del archivo
                        string[] path = Request.Files[fileName].FileName.Split(new char[] { '\\' });

                        string sql = @"INSERT INTO Evidencias (FaltaId,EvidenciaContenido,EvidenciaNombreArchivo,EvidenciaMime,EvidenciaDescripcion)
                                       VALUES(@id,@contenido,@nombre,@mime,@descripcion)";
                        SqlCommand ins = new SqlCommand(sql, con, trans);
                        ins.Parameters.Add(new SqlParameter("id",ViewState["FaltaId"]));
                        ins.Parameters.Add(new SqlParameter("contenido", buffer));
                        ins.Parameters.Add(new SqlParameter("nombre", path[path.Length-1]));
                        ins.Parameters.Add(new SqlParameter("mime", Request.Files[fileName].ContentType));
                        ins.Parameters.Add(new SqlParameter("descripcion", descr));
                        ins.ExecuteNonQuery();
                    }
                }
                else
                    break;

                count++;
            }
            EliminarAEvidencias(con, trans);
        }
        
        void EliminarAEvidencias(SqlConnection con, SqlTransaction trans)
        {
            DataTable dtEvidencias = (DataTable)ViewState["Evidencias"];

            List<int> archivosBorrados = new List<int>();
            for (int x = 0; x < dtEvidencias.Rows.Count; x++)
            {
                int num = int.Parse(dtEvidencias.Rows[x]["EvidenciaId"].ToString());
                if (Request.Params["Archivo" + num] == null)
                {
                    archivosBorrados.Add(num);
                }
            }
            for (int i = 0; i < archivosBorrados.Count; i++)
            {
                string sql = @"DELETE Evidencias
                           WHERE EvidenciaId='" + archivosBorrados[i] + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        void limpiar()
        {
            ViewState["AlumnoId"] = null;
            ViewState["FaltaId"] = null;
            txtNumeroActa.Text = "";
            txtCuenta.Text = "";
            txtNombre.Text = "";
            cmbCarrera.SelectedIndex = 0;
            radioBtnGenero.SelectedIndex = 0;
            cmbCategoria.SelectedIndex = 0;
            cmbTipoIncisoDescripcion.SelectedIndex = 0;
            txtArticuloNumero.Text = "";
            txtIncisoLetra.Text = "";
            txtIncisoDescripcion.Text = "";
            txtDecripcionFalta.Text = "";
            cmbTipoSancion.SelectedIndex = 0;
            txtTiempoSancion.Text = "";
            txtDescripcionSancion.Text = "";
            txtFaltaTitulo.Text = "";
            cargarNumeroArticulo();
            cargarIncisos();
        }

        protected void txtCuenta_TextChanged(object sender, EventArgs e)
        {
            buscarAlumno();
            cmbCategoria.SelectedIndex = 0;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                CargarIncisoDescripcion(con);
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
            cmbTipoIncisoDescripcion.SelectedIndex = 0;
            cmbTipoSancion.SelectedIndex = 0;
        }

        protected void txtIncisoDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDecripcionFalta_TextChanged(object sender, EventArgs e)
        {

        }


       
    }
}