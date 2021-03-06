﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace EticaUNITEC.Faltas
{
    /// <summary>
    /// Summary description for Archivo
    /// </summary>
    public class Archivo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            

            int id = context.Request.Params["cod"] != null ? int.Parse(context.Request.Params["cod"]) : 0;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = @"SELECT EvidenciaContenido,EvidenciaMime,EvidenciaNombreArchivo,EvidenciaDescripcion
                               FROM Evidencias
                               WHERE EvidenciaId=@id";
                con.Open();

               

                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable datos = new DataTable();
                adp.SelectCommand.Parameters.Add(new SqlParameter("id", id));
                adp.Fill(datos);

                if (datos.Rows.Count > 0)
                {
                    context.Response.AddHeader("content-disposition", "attachment; filename=" + datos.Rows[0]["EvidenciaNombreArchivo"].ToString());


                    context.Response.ContentType = datos.Rows[0]["EvidenciaMime"].ToString();
                    byte[] buffer = (byte[])datos.Rows[0]["EvidenciaContenido"];
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Output.Write("El archivo no existe");
                }

            }
            catch (Exception)
            {
                
            }
            finally
            {
                con.Close();
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}