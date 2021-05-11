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
                /*int rowCount = 0;
                int pageSize = 10;
                var model = ProductService.List(page, pageSize, CategoryID, SupplierID, searchValue, out rowCount);
                ViewBag.RowCount = rowCount;
                ViewBag.Page = page;

                int pageCount = rowCount /pageSize;
                if (rowCount % pageSize > 0)
                    pageCount++;
                ViewBag.PageCount = pageCount;

                return View(model);*/

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
            ViewBag.Title = "Thay đổi thông tin hàng hóa";
            var model = ProductService.Get(id);
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

            return View("Edit", model);
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
                    return View("Edit", data);
                }

                if (data.ProductID == 0)
                    ProductService.Add(data);
                else
                    ProductService.Update(data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
    }
}