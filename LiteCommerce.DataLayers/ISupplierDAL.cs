using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến nhà cung cấp
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Lấy danh sách toàn bộ nhà cung cấp
        /// </summary>
        /// <returns></returns>
        List<Supplier> List();
        /// <summary>
        /// Bổ sung một nhà cung cấp, Hàm trả về một nhà cung cấp
        /// nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của nhà cung cấp cần bổ sung</param>
        /// <returns></returns>
        int Add(Supplier data);
        //int[] Add(Supplier[] data);// Bổ sung nhiều nhà cung cấp
        /// <summary>
        /// Lấy danh sách nhà cung cấp (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize"> Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo SupplierName, ContactName, Address, Phone (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Supplier> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng nhà cùg cấp thỏa điêu kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo SupplierName, ContactName, Address, Phone (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của nhà cung cấp theo mã. Trong trường hợp nhà cung cấp không tồn tại, hàm trả về giá trị null
        /// </summary>
        /// <param name="supplierID">Mã nhà cung cấp cần lấy thông tin</param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// Cập nhật thông tin cảu một nhà cung cấp. Hàm trả về boolean cho biết
        /// việc cập nhật có thành công hay không
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(int supplierID, Supplier data);
        /// <summary>
        /// Xóa một nhà cung cấp dựa vào mã. Hàm trả về boolean cho biết
        /// việc xóa có thành công hay không (Lưu ý: Không được xóa nhà cung cấp nếu đang có mặt hàng tham chiếu đến nhà cung cấp
        /// </summary>
        /// <param name="supplierID">Mã nhà cung cấp cần xóa</param>
        /// <returns></returns>
        bool Delete(int supplierID);
    }
}
