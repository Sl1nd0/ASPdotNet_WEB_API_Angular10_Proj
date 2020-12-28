using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net; 
using WebAPI_Project.Models;
using System.Windows;

namespace WebAPI_Project.Controllers
{
    public class EmployeeController : ApiController
    {

        // GET: Department
        public HttpResponseMessage Get()
        {
            /*EmployeeId EmployeeName    Department DateOfJoining   photoFileName
1   Sam IT  2020 - 01 - 01  anonymous.png*/
            string query = @"
                select EmployeeId, EmployeeName, Department, convert(varchar(20), 
                DateOfJoining, 120) as DateOfJoining
                FROM dbo.Employee
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
        public string Post(Employee emp)
        {

            try
            {

                string query = "Insert Into  dbo.Employee "
                    + "Values ('" + emp.EmployeeName + @"'," +
                                "'" + emp.Department + @"'," +
                                "'" + emp.DateOfJoining + @"'," +
                                "'" + emp.PhotoFileName + @"'"
                    + @")";

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

            }
            catch (Exception)
            {
                return "Something went wrong adding";
            }

        }

        //Put
        public string Put(Employee emp)
        {
            string photo = "anonymous.png";
            if (!String.IsNullOrEmpty(emp.PhotoFileName))
            {
                photo = emp.PhotoFileName;
            }

            string query = "Update dbo.Employee " +
                   " Set EmployeeName = '" + emp.EmployeeName + "', "
                   + "Department = '" + emp.Department + "', "
                   + "DateOfJoining = '" + emp.DateOfJoining + "', "
                   + "photoFileName = '" + photo + "' "
                   + " WHERE EmployeeId = " + emp.EmployeeId;

            try
            {
               

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
                return query;
            }

        }

        //Delete
        public string Delete(int id)
        {

            try
            {
                string query = @"
                DELETE FROM dbo.Employee
                WHERE EmployeeId = " + id;

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

        [System.Web.Http.Route("api/Employee/GetAllDepartmentNames")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"
                SELECT DepartmentName FROM dbo.Department ";

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

        [System.Web.Http.Route("api/Employee/saveFile")]
        public string saveFile()
        {
            try
            {
                var HttpRequest = HttpContext.Current.Request;
                var postedFile = HttpRequest.Files[0];
                string filename = postedFile.FileName;

                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                postedFile.SaveAs(physicalPath);

                return filename;
            }
            catch (Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
