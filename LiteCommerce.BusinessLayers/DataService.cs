using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng nghiệp vụ liên quan đến quản lý dữ liệu chung
    /// </summary>
    public static class DataService
    {
        private static ICountryDAL CountryDB;
        private static ICityDAL CityDB;
        private static ISupplierDAL SupplierDB;
        private static IShipperDAL ShipperDB;
        private static ICategoryDAL CategoryDB;
        private static ICustomerDAL CustomerDB;
        private static IEmployeeDAL EmployeeDB;

        /// <summary>
        /// Khởi tạo tính năng tác nghiệp (Hàm này phải được gọi nếu muốn sử dụng các tính năng của lớp)
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    CountryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
                    CityDB = new DataLayers.SQLServer.CityDAL(connectionString);
                    SupplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
                    CategoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
                    CustomerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
                    EmployeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
                    ShipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database Type is not Supported");
            }
        }

        //------------------------------------------------LIST---------------------------------------------------//

        /// <summary>
        /// Danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListCountries()
        {
            return CountryDB.List();
        }
        /// <summary>
        /// Danh sách thành phố
        /// </summary>
        /// <returns></returns>
        public static List<City> ListCities()
        {
            return CityDB.List();
        }
        /// <summary>
        /// Danh sách thành phố của một quốc gia
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<City> ListCities(string countryName)
        {
            return CityDB.List(countryName);
        }
        /// <summary>
        /// Danh sách Nhà cung cấp (phân trang, tìm kiếm)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Thêm 1 nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(int supplierID, Supplier data)
        {
            return SupplierDB.Update(supplierID, data);
        }

        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return SupplierDB.Delete(supplierID);
        }

        /// <summary>
        /// Hiển thị nhà cung cấp theo ma
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }

        //-------------------------------END SUPPLIER-------------------------------------------------------//


        /// <summary>
        /// Danh sách loai hang (phân trang, tìm kiếm)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Category> ListCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CategoryDB.Count(searchValue);
            return CategoryDB.List(page, pageSize, searchValue);
        }
        public static int AddCategory(Category data)
        {
            return CategoryDB.Add(data);
        }
        /// <summary>
        /// Cập nhật loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(int categoryID, Category data)
        {
            return CategoryDB.Update(categoryID, data);
        }
        /// <summary>
        /// Xóa loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            return CategoryDB.Delete(categoryID);
        }
        /// <summary>
        /// Hiển thị loại hàng theo mã
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return CategoryDB.Get(categoryID);
        }


        //------------------------------------------------END CATEGORY------------------------------//
        /// <summary>
        /// Danh sách khach hang (phân trang, tìm kiếm)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CustomerDB.Count(searchValue);
            return CustomerDB.List(page, pageSize, searchValue);
        }

        public static int AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(int customerID, Customer data)
        {
            return CustomerDB.Update(customerID, data);
        }
        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            return CustomerDB.Delete(customerID);
        }

        /// <summary>
        ///bổ sung khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return CustomerDB.Get(customerID);
        }



        //----------------------------------------------END CUSTOMER------------------------------------//
        /// <summary>
        /// Danh sách nhan vien (phân trang, tìm kiếm)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue);
        }
        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// Cập nhật nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(int employeeID, Employee data)
        {
            return EmployeeDB.Update(employeeID, data);
        }
        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            return EmployeeDB.Delete(employeeID);
        }

        /// <summary>
        ///bổ sung khách hàng 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }



        //----------------------------------------------END EMPLOYEE------------------------------------//
        /// <summary>
        /// Danh sach nha van chuyen
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue);
        }
        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        /// <summary>
        /// Cập nhật nhà vận chuyển
        /// </summary>
        /// <param name="shipperID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(int shipperID, Shipper data)
        {
            return ShipperDB.Update(shipperID, data);
        }
        /// <summary>
        /// Xóa nhà vận chuyển theo mã
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            return ShipperDB.Delete(shipperID);
        }

        /// <summary>
        ///bổ sung nhà vận chuyển 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }

        //---------------------------------------------END SHIPPER-----------------------------------------//


    }
}
