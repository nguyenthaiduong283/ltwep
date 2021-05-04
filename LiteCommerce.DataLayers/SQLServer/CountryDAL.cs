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
    /// 
    /// </summary>
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> List()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from Countries";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        /*Country country = new Country();
                        country.CountryName = Convert.ToString(dbReader["CountryName"]);
                        data.Add(country);*/

                        //Country country = new Country()
                        //{
                        //    CountryName = Convert.ToString(dbReader["CountryName"])
                        //};
                        //data.Add(country);

                        data.Add(new Country()
                        {
                            CountryName = Convert.ToString(dbReader["CountryName"])
                        }
                        );
                    }
                }

                cn.Close();
            }
            return data;
        }
    }
}
