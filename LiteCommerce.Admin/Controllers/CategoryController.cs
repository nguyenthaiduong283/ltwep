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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page =1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 5;
            var listCategories = DataService.ListCategories(page, pageSize, searchValue, out rowCount);

            var model = new Models.CategoryPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listCategories
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin loại hàng";
            var model = DataService.GetCategory(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Thêm thông tin loại hàng";
            Category model = new Category()
            {
                CategoryID = 0
            };

            return View("Edit", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Xóa nhà loại hàng";
            if (Request.HttpMethod == "POST")
            {
                DataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetCategory(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Category data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Vui long nhap ten loai hang !");
                if (string.IsNullOrEmpty(data.Description))
                    data.Description = "";

                if (!ModelState.IsValid)
                {
                    if (data.CategoryID == 0)
                        ViewBag.Title = "Bo sung nha cung cap";
                    else
                        ViewBag.Title = "Thay doi thong tin nha cung cap";
                    return View("Edit", data);
                }

                if (data.CategoryID == 0)
                    DataService.AddCategory(data);
                else
                    DataService.UpdateCategory(data.CategoryID, data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
    }
}