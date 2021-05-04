using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class _BaseDAL
    {
        /// <summary>
        /// Chuỗi tham số kết nối đến cơ sở dữu liệu
        /// </summary>
        protected string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Tạo và mở kết nối đến cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        protected SqlConnection GetConnection()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = this.connectionString;
            cn.Open();
            return cn;
        }
    }
}
