using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan dến nhân viên 
    /// </summary>
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns></returns>
        List<Employee> List();
        /// <summary>
        /// Bổ sung một nhân viên, Hàm trả về một nhân viên
        /// nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của nhân viên cần bổ sung</param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// Lấy danh sách nhân viên (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize"> Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo LastName, FirstName, Email (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng nhân viên thỏa điêu kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo LastName, FirstName, Email (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của nhân viên theo mã. Trong trường hợp nhân viên không tồn tại, hàm trả về giá trị null
        /// </summary>
        /// <param name="supplierID">Mã nhân viên cần lấy thông tin</param>
        /// <returns></returns>
        Employee Get(int employeeID);
        /// <summary>
        /// Cập nhật thông tin cảu một nhân viên. Hàm trả về boolean cho biết
        /// việc cập nhật có thành công hay không
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(int employeeID, Employee data);
        /// <summary>
        /// Xóa một nhân viên dựa vào mã. Hàm trả về boolean cho biết
        /// việc xóa có thành công hay không (Lưu ý: Không được xóa nhà cung cấp nếu đang có mặt hàng tham chiếu đến nhân viên
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns></returns>
        bool Delete(int employeeID);
    }
}
