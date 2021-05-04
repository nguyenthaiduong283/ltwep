using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến loại hàng
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        /// Lấy danh sách toàn bộ loại hàng
        /// </summary>
        /// <returns></returns>
        List<Category> List();
        /// <summary>
        /// Bổ sung một loại hàng, Hàm trả về một loại hàng
        /// nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của loại hàng cần bổ sung</param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// Lấy danh sách loại hàng (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize"> Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CategoryName (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Category> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng loại hàng thỏa điêu kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CategoryName (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của loại hàng theo mã. Trong trường hợp loại hàng không tồn tại, hàm trả về giá trị null
        /// </summary>
        /// <param name="categoryID">Mã loại hàng cần lấy thông tin</param>
        /// <returns></returns>
        Category Get(int categoryID);
        /// <summary>
        /// Cập nhật thông tin cảu một loại hàng. Hàm trả về boolean cho biết
        /// việc cập nhật có thành công hay không
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(int categoryID, Category data);
        /// <summary>
        /// Xóa một loại hàng dựa vào mã. Hàm trả về boolean cho biết
        /// việc xóa có thành công hay không (Lưu ý: Không được xóa loại hàng nếu đang có mặt hàng tham chiếu đến loại hàng
        /// </summary>
        /// <param name="categoryID">Mã loại hàng cần xóa</param>
        /// <returns></returns>
        bool Delete(int categoryID);
    }
}
