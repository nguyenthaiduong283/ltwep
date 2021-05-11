using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductDAL
    {
        /// <summary>
        /// Lấy danh sách các mặt hàng (phân trang tìm kiếm, lọc dữ liệu)
        /// </summary>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="categoryId">Mã loại hàng(0 nếu ko lọc theo mã loại hàng)</param>
        /// <param name="supplierId">mã nhà cung cấp(0 nếu không lọc theo nhà cung cấp</param>
        /// <param name="searchValue">tên amwtj hàng cần tìm kiếm ( chuỗi rỗng nếu ko tìm thấy)</param>
        /// <returns></returns>
        List<Product> List(int page, int pageSize, int categoryId, int supplierId, string searchValue);

        /// <summary>
        /// Đếm số lượng thỏa điêu kiện tìm kiếm
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int categoryId, int supplierId, string searchValue);

        /// <summary>
        /// Lấy thông tin mặt hàng theo mã
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product Get(int productId);
        /// <summary>
        /// Lấy thông tin mặt hàng và các thông tin liên quan theo mã hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product GetEx(int productId);
        /// <summary>
        /// Bổ sung 1 mặt hàng mới
        /// (hàm trả vể mã hàng đc bổ sung nếu thành công, trả về 0 nếu ko bổ sung đc)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);
        /// <summary>
        /// cập nhật thông tin mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// xóa 1 mặt hàng 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool Delete(int productId);
        /// <summary>
        /// Lấy danh sách các thuộc tính của 1 product(sắp xếp theo displayOrder)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productId);
        /// <summary>
        /// Lấy thông tin chi tiết của 1 thuộc tính 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(long attributeId);
        /// <summary>
        /// Bổ sung thuộc tính cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddAttribute(ProductAttribute data);
        /// <summary>
        /// Cập nhật thuôc tính
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);
        /// <summary>
        /// xóa thuộc tính
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        bool DeleteAttribute(long attributeId);


        /// <summary>
        /// lấy danh sashc hình ảnh của 1 mặt hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductGallery> ListGalleries(int productId);
        /// <summary>
        /// lấy thông tin 1 hình ảnh
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        ProductGallery GetGallery(long galleryId);
        /// <summary>
        /// bổ sung 1 ảnh 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddGallery(ProductGallery data);
        /// <summary>
        /// cập nhật thông tin ảnh
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateGallery(ProductGallery data);
        /// <summary>
        /// xóa 1 ảnh
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        bool DeleteGallery(long galleryId);
    }
}
