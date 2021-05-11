using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt chức năng liên quan đến tài
    /// khoản của nhân viên
    /// </summary>
    public class EmployeeAccountDAL : _BaseDAL, IAccountDAL
    {
        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        {

        }

        public Account Authorize(string loginName, string password)
        {
            Account data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT EmployeeID, FirstName, LastName, Email, Photo
                                    FROM Employees
                                    Where Email= @loginName AND Password= @password";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@loginName", loginName);
                cmd.Parameters.AddWithValue("@password", password);

                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Account()
                        {
                            UserName = dbReader["EmployeeID"].ToString(),
                            FullName = $"{dbReader["FirstName"]} { dbReader["LastName"]}",
                            Email = dbReader["Email"].ToString(),
                            Photo= dbReader["Photo"].ToString()
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool ChangePassword(string accountId, string oldpassword, string newpassword)
        {
            throw new NotImplementedException();
        }

        public Account Get(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}
