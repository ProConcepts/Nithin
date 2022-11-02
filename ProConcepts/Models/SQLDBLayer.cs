using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProConcepts.Models
{
    public class SQLDBLayer : IEmployeeRepository
    {
        public static List<Employee> getEmployeesData = new List<Employee>();
        public IConfiguration Configuration { get; }
        public SQLDBLayer(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<Employee> GetEmployees()
        {
            //if (getEmployeesData.Count > 0)
            //    return getEmployeesData;
            List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("EmployeeContext")))
            {
                string query = "SELECT ID, NAME, SALARY, A.DEPARTMENT, DEPT_NAME FROM TBLEMPLOYEE A " +
                    "LEFT JOIN TBLDEPARTMENT B ON A.DEPARTMENT = B.DEPT_ID;";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Name = sdr["name"].ToString(),
                                Salary = Convert.ToInt32(sdr["Salary"]),
                                Department = Convert.ToInt32(sdr["Department"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            getEmployeesData = employees;//Local caching :)
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("EmployeeContext")))
            {

                string query = string.Empty;
                query = "INSERT INTO tblEmployee VALUES (@name,@salary,@department);";
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@department", employee.Department);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public bool EditEmployee(Employee employee)
        {
            bool flag = false;
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("EmployeeContext")))
            {
                con.Open();
                string query = string.Empty;
                query = "UPDATE tblEmployee SET name=@name,salary=@salary,department=@department WHERE ID=@id;";
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@department", employee.Department);
                    cmd.Parameters.AddWithValue("@id", employee.Id);

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    flag = true;
                    con.Close();                    
                }
            }
            return flag;
        }

        public bool DeleteEmployee(int id)
        {
            bool flag = false;
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("EmployeeContext")))
            {
                con.Open();
                string query = string.Empty;
                query = "Delete from tblEmployee WHERE ID=@id;";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", id);  
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    flag = true;
                    con.Close();
                }
            }
            return flag;
        }

        public List<Department> GetDepartments()
        {
            //if (getEmployeesData.Count > 0)
            //    return getEmployeesData;
            List<Department> departments = new List<Department>();
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("EmployeeContext")))
            {
                string query = "SELECT dept_id, dept_name FROM tbldepartment;";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            departments.Add(new Department
                            {
                                Id = Convert.ToInt32(sdr["dept_id"]),
                                Name = sdr["dept_name"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return departments;
        }
    }
}
