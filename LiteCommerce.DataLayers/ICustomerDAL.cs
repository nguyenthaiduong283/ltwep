using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến khách hàng
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Lấy danh sách toàn bộ khách hàng
        /// </summary>
        /// <returns></returns>
        List<Customer> List();
        /// <summary>
        /// Bổ sung một khách hàng, Hàm trả về một khách hàng
        /// nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của khách hàng cần bổ sung</param>
        /// <returns></returns>
        int Add(Customer data);
        /// <summary>
        /// Lấy danh sách khách hàng (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize"> Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CustomerName, ContactName, Address, Email (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Customer> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng khách hàng thỏa điêu kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CustomerName, ContactName, Address, Email (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của khách hàng theo mã. Trong trường hợp khách hàng không tồn tại, hàm trả về giá trị null
        /// </summary>
        /// <param name="customerID">Mã khách hàng cần lấy thông tin</param>
        /// <returns></returns>
        Customer Get(int customerID);
        /// <summary>
        /// Cập nhật thông tin cảu một khách hàng. Hàm trả về boolean cho biết
        /// việc cập nhật có thành công hay không
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(int customerID, Customer data);
        /// <summary>
        /// Xóa một khách hàng dựa vào mã. Hàm trả về boolean cho biết
        /// việc xóa có thành công hay không (Lưu ý: Không được xóa khách hàng nếu đang có mặt hàng tham chiếu đến khách hàng
        /// </summary>
        /// <param name="customerID">Mã khách hàng cần xóa</param>
        /// <returns></returns>
        bool Delete(int customerID);
    }
}
