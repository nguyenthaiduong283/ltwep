using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int CategoryID = 0, int SupplierID = 0, string searchValue ="", int page =1)
        {
            try
            {
                

                int rowCount = 0;
                int pageSize = 10;
                var listOfProduct = ProductService.List(page, pageSize, CategoryID, SupplierID, searchValue, out rowCount);
                var model = new Models.ProductPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfProduct
                };
                return View(model);

            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin mặt hàng";
            var model = ProductService.GetEx(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }

        public ActionResult Add()
        {
            ViewBag.Title = "Thêm thông tin hàng hóa";
            Product model = new Product()
            {
                ProductID = 0
            };

            return View(model);
        }


        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Xóa hàng";
            if (Request.HttpMethod == "POST")
            {
                //Xoa product co ma la id
                //Quay ve lai trang index
                ProductService.Delete(id);
                return RedirectToAction("Index");
            }
            else
            {
                //Lay thong tin product can xoa
                //tra thong tin ve cho view hien thi
                var model = ProductService.Get(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);

            }
        }


        public ActionResult Save(Product data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.ProductName))
                    ModelState.AddModelError("ProductName", "Vui lòng nhập tên hàng !");
                if (string.IsNullOrWhiteSpace(data.Unit))
                    ModelState.AddModelError("Unit", "Bạn chưa nhập đơn vị tính !");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.Price)))
                    ModelState.AddModelError("Price", "Bạn chưa nhập giá hàng !");
                if (string.IsNullOrWhiteSpace(data.Photo))
                    ModelState.AddModelError("Photo", "Bạn chưa nhập ảnh !");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.CategoryID)))
                    ModelState.AddModelError("CategoryID", "Vui lòng nhập loại hàng !");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.SupplierID)))
                    ModelState.AddModelError("SupplierID", "Bạn chưa nhập nhà cung cấp!");


                if (!ModelState.IsValid)
                {
                    if (data.ProductID == 0)
                        ViewBag.Title = "Bổ sung hàng hóa";
                    else
                        ViewBag.Title = "Thay đổi thông tin hàng hóa";
                    return View("Add", data);
                }

                if (data.ProductID == 0)
                {
                    
                    var id=ProductService.Add(data);
                    var model = ProductService.GetEx(id);
                    return View("Edit", model);
                }
                    
                else
                    ProductService.Update(data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
        //-----------------------------------Attribute------------------------------------------------------------//
        
        public ActionResult AddAttribute(int id )
        {
            ViewBag.Title = "Thêm thông tin thuộc tính";
            ProductAttribute model = new ProductAttribute()
            {
                AttributeID = 0,
                ProductID = id
            };
            return View("EditAttribute", model);
        }
        public ActionResult EditAttribute(int id)
        {
            ViewBag.Title = "Cập nhật thông tin thuộc tính";
            var model = ProductService.GetAttribute(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }

        public ActionResult DeleteAttributes(int id, long[] attributeIds)
        {
            if (attributeIds == null)
                return RedirectToAction("Edit", new { id = id });
            ProductService.DeleteAttribute(attributeIds);
            return RedirectToAction("Edit", new { id = id });
        }

        public ActionResult SaveAttributes(ProductAttribute data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.ProductID)))
                    ModelState.AddModelError("ProductID", "Vui lòng nhập tên hàng !");
                if (string.IsNullOrWhiteSpace(data.AttributeName))
                    ModelState.AddModelError("AttributeName", "Bạn chưa nhập tên thuộc tính !");
                if (string.IsNullOrWhiteSpace(data.AttributeValue))
                    ModelState.AddModelError("AttributeValue", "Bạn chưa nhập giá trị thuộc tính !");
                
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.DisplayOrder)))
                    ModelState.AddModelError("DisplayOrder", "Bạn chưa nhập thứ tự hiển thị ");


                if (!ModelState.IsValid)
                {
                    if (data.AttributeID == 0)
                        ViewBag.Title = "Bổ sung thuộc tính";
                    else
                        ViewBag.Title = "Thay đổi thông tin thuộc tính";
                    return View("EditAttribute", data);
                }

                if (data.AttributeID == 0)
                    ProductService.AddAttribute(data);
                else
                    ProductService.UpdateAttribute(data);

                return RedirectToAction("Edit", new { id = data.ProductID });
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }


        //-------------------------------------Gallery----------------------------------------------------------------//
        public ActionResult AddGallery(int id)
        {
            ViewBag.Title = "Thêm thông tin thư viện sản phẩm";
            ProductGallery model = new ProductGallery()
            {
                GalleryID = 0,
                ProductID = id
            };
            return View("EditGallery", model);
        }
        public ActionResult EditGallery(int id)
        {
            ViewBag.Title = "Cập nhật thông tin thư viện sản phẩm";
            var model = ProductService.GetGallery(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }

        public ActionResult DeleteGalleries(int id, long[]galleryId)
        {
            if (galleryId == null)
                return RedirectToAction("Edit", new { id = id });
            ProductService.DeleteGalleries(galleryId);
            return RedirectToAction("Edit", new { id = id });
        }

        public ActionResult SaveGallery(ProductGallery data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.ProductID)))
                    ModelState.AddModelError("ProductID", "Vui lòng nhập tên hàng !");
                if (string.IsNullOrWhiteSpace(data.Photo))
                    ModelState.AddModelError("Photo", "Bạn chưa nhập ảnh !");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Bạn chưa nhập mô tả!");

                if (string.IsNullOrWhiteSpace(Convert.ToString(data.DisplayOrder)))
                    ModelState.AddModelError("DisplayOrder", "Bạn chưa nhập thứ tự hiển thị ");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.IsHidden)))
                    ModelState.AddModelError("IsHidden", "Bạn chưa nhập..... ");

                if (!ModelState.IsValid)
                {
                    if (data.GalleryID == 0)
                        ViewBag.Title = "Bổ sung thư viện của sản phẩm";
                    else
                        ViewBag.Title = "Thay đổi thông tin thư viện sản phẩm";
                    return View("EditGallery", data);
                }

                if (data.GalleryID == 0)
                    ProductService.AddGallery(data);
                else
                    ProductService.UpdateGallery(data);

                return RedirectToAction("Edit", new { id = data.ProductID });
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }

        public ActionResult Details(int id)
        {
            ViewBag.Title = "Chi tiết thông tin mặt hàng";
            var model = ProductService.GetEx(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
    }

}