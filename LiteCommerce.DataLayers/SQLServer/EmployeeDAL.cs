using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các tính năng xử lý dữ liệu nhân viên trong CSDL SQL Server
    /// </summary>
    public class EmployeeDAL : _BaseDAL, IEmployeeDAL
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
            
        }

        public int Add(Employee data)
        {
            int employeeID = 0;

            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Employees (
                                                            LastName,
                                                            FirstName,
                                                            BirthDate,
                                                            Photo,
                                                            Notes,
                                                            Email,
                                                            Password
                                                            )
                                    VALUES (
                                            @LastName,
                                            @FirstName,
                                            @BirthDate,
                                            @Photo,
                                            @Notes,
                                            @Email,
                                            @Password
                                            );
                                    SELECT @@IDENTITY;
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);

                employeeID = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return employeeID;
        }

        public int Count(string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            int result = 0;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) FROM Employees
                                    WHERE (@searchValue = '')
                                       OR ( LastName LIKE @searchValue
                                            OR FirstName LIKE @searchValue
                                            OR Email LIKE @searchValue
                                            )     
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        public bool Delete(int employeeID)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Employees
                                    WHERE EmployeeID= @employeeID
                                        AND NOT EXISTS (SELECT * FROM Orders WHERE EmployeeID = Employees.EmployeeID)
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@employeeID", employeeID);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }

            return result;
        }

        public Employee Get(int employeeID)
        {
            Employee data = null;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE EmployeeID = @employeeID
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@employeeID", employeeID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"]),
                        };
                    }
                }
                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Employee> List()
        {
            List<Employee> data = new List<Employee>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = this.connectionString;
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Employees";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;


                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"]),
                        });
                    }
                }


                cn.Close();
            }

            return data;
        }

        public List<Employee> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            List<Employee> data = new List<Employee>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                    FROM( 
                        SELECT * , ROW_NUMBER() OVER(ORDER BY EmployeeID) AS RowNumber 
                        FROM Employees WHERE(@searchValue = '')
                            OR( FirstName LIKE @searchValue 
                            OR  LastName LIKE @searchValue 
                            OR  BirthDate LIKE @searchValue 
                            OR  Email LIKE @searchValue)) AS s
                            WHERE s.RowNumber BETWEEN(@page -1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {

                        data.Add(new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"]),
                        }
                        );
                    }
                }

                cn.Close();
            }
            return data;
        }

        public bool Update(int employeeID, Employee data)
        {
            
            bool result = false;

            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Employees
                                    SET LastName = @LastName,
                                        FirstName = @FirstName,
                                        BirthDate = @BirthDate,
                                        Photo = @Photo,
                                        Notes = @Notes,
                                        Email = @Email,
                                        Password = @Password
                                    WHERE EmployeeID = @employeeID;
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@employeeID", employeeID);

                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
    }
}
