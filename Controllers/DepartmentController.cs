using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Web.Http;
using WebAPI_Project.Models;
using System.Windows;

namespace WebAPI_Project.Controllers
{
    public class DepartmentController : ApiController
    {
        // GET: Department
        public HttpResponseMessage Get()
        {
            string query = @"
                select DepartmentId, DepartmentName
                FROM dbo.Department
                ";
            DataTable table = new DataTable();

            //EmployeeConnection

            using (var con = new SqlConnection
                (ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString))

            using (var cmd = new SqlCommand(query, con)) 

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

        }
        
        //Post
        public string Post(Department Dep)
        {
            
            try
            {
                
                string query = "Insert Into  dbo.Department "
                    + "Values ('" + Dep.DepartmentName + "')";

                DataTable table = new DataTable();

                //EmployeeConnection

                using (var con = new SqlConnection
                    (ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))

                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Successfully Added";

            } catch (Exception)
            {
                return "Somethinhg went wrong adding";
            }          

        }

        //Put
        public string Put(Department Dep)
        {

            try
            {
                string query = "Update dbo.Department Set departmentName = '" + Dep.DepartmentName.ToString() + 
                    "' WHERE DepartmentId = " + Dep.DepartmentId.ToString();

                DataTable table = new DataTable();

                //EmployeeConnection

                using (var con = new SqlConnection
                    (ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))

                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Successfully Updated";

            }
            catch (Exception)
            {
                return "Something went wrong adding";
            }

        }

        //Delete
        public string Delete(int id)
        {

            try
            {
                string query = @"
                DELETE FROM dbo.Department
                WHERE DepartmentId = " + id;

                DataTable table = new DataTable();

                //EmployeeConnection

                using (var con = new SqlConnection
                    (ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))

                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Successfully deleted";

            }
            catch (Exception)
            {
                return "Something went wrong deleting";
            }

        }
    }
}