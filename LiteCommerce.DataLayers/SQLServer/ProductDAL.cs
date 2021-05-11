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
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        
        public ProductDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Product data)
        {
            int productID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Products( ProductName,SupplierID,CategoryID, Unit, Price, Photo)
                                    VALUES ( @ProductName,@SupplierID,@CategoryID,@Unit, @Price, @Photo);
                                    SELECT @@IDENTITY;
                                    ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                

                productID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return productID;
        }

        public long AddAttribute(ProductAttribute data)
        {
            throw new NotImplementedException();
        }

        public long AddGallery(ProductGallery data)
        {
            throw new NotImplementedException();
        }

        public int Count(int categoryId, int supplierId, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*)
                                    FROM Products
                                    WHERE (@searchValue = '')
                                       OR (
                                                ProductName LIKE @searchValue 
                                            OR  Unit LIKE @searchValue 
                                            OR  Price LIKE @searchValue   
                                          )";
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);

                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }

        public bool Delete(int productId)
        {
            bool result = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Products
                                    WHERE ProductID = @productId
                                        AND NOT EXISTS( SELECT *
                                                        FROM OrderDetails
                                                        WHERE ProductID = Products.productId)
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", productId);
                result = cmd.ExecuteNonQuery() > 0;

            }
            return result;
        }

        public bool DeleteAttribute(long attributeId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGallery(long galleryId)
        {
            throw new NotImplementedException();
        }

        public Product Get(int productId)
        {
            Product data = null;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID = @productId
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@productId", productId);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            
                        };
                    }
                }
                cn.Close();
            }

            return data;
        }

        public ProductAttribute GetAttribute(long attributeId)
        {
            throw new NotImplementedException();
        }

        public Product GetEx(int productId)
        {
            throw new NotImplementedException();
        }

        public ProductGallery GetGallery(long galleryId)
        {
            throw new NotImplementedException();
        }

        public List<Product> List(int page, int pageSize, int categoryId, int supplierId, string searchValue)
        {

            {
                List<Product> data = new List<Product>();
                if (searchValue != "")
                    searchValue = "%" + searchValue + "%";
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = cn.CreateCommand();

                    cmd.CommandText = @"SELECT  *
                                    FROM
                                    (
                                        SELECT  *, ROW_NUMBER() OVER(ORDER BY ProductName) AS RowNumber
                                        FROM    Products 
                                        WHERE   (@categoryId = 0 OR CategoryId = @categoryId)
                                            AND  (@supplierId = 0 OR SupplierId = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    cmd.Parameters.AddWithValue("@supplierID", supplierId);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Product()
                            {
                                ProductID = Convert.ToInt32(dbReader["ProductID"]),
                                ProductName = Convert.ToString(dbReader["ProductName"]),
                                SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                                CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                                Unit = Convert.ToString(dbReader["Unit"]),
                                Price = Convert.ToDecimal(dbReader["Price"]),
                                Photo = Convert.ToString(dbReader["Photo"])

                            });
                        }
                    }
                    cn.Close();
                }
                return data;
            }
        }

        public List<ProductAttribute> ListAttributes(int productId)
        {
            throw new NotImplementedException();
        }

        public List<ProductGallery> ListGalleries(int productId)
        {
            throw new NotImplementedException();
        }

        public bool Update( Product data)
        {
            bool result = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Products
                                    SET ProductName = @ProductName,
                                        CategoryID = @CategoryID,
                                        SupplierID = @SupplierID,
                                        Photo = @Photo,
                                        Price = @Price,
                                        Unit = @Unit
                                    WHERE ProductID = @productId
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@productId", data.ProductID);

                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();

            }
            return result;
        }

        public long UpdateAttribute(ProductAttribute data)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGallery(ProductGallery data)
        {
            throw new NotImplementedException();
        }

        bool IProductDAL.UpdateAttribute(ProductAttribute data)
        {
            throw new NotImplementedException();
        }
    }
}
