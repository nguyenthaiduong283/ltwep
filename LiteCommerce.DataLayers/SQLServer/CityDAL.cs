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
    public class CityDAL : _BaseDAL, ICityDAL
    {
        public CityDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<City> List()
        {
            return List("");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public List<City> List(string countryName)
        {
            List<City> data = new List<City>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Cities WHERE @CountryName= '' OR CountryName= @CountryName";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CountryName", countryName);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new City()
                        {
                            CityName = Convert.ToString(dbReader["CityName"]),
                            CountryName = Convert.ToString(dbReader["CountryName"]),

                        });

                    }
                }

                cn.Close();
            }
            return data;
        }
    }
}