using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            //int rowCount = 0;
            //int pageSize = 5
            //var listOfSupplier = DataService.ListSuppliers(page, pageSize, searchValue, out rowCount);
            //int pageCount = rowCount / pageSize;
            //if (rowCount % pageSize > 0)
            //    pageCount += 1;
            //ViewBag.Page = page;
            //ViewBag.RowCount = rowCount;
            //ViewBag.PageCount = pageCount;
            //ViewBag.SearchValue = searchValue;
            //var model = HRService.Supplier_List();
            //return View(model);
            //return View(listOfSupplier);
            int rowCount = 0;
            int pageSize = 5;
            var listOfSupplier = DataService.ListSuppliers(page, pageSize, searchValue, out rowCount);

            var model = new Models.SupplierPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfSupplier
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
            ViewBag.Title = "Thay đổi thông tin nhà cung cấp";

            var model = DataService.GetSupplier(id);
            if(model == null)
                RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Thêm thông tin nhà cung cấp";

            Supplier model = new Supplier()
            {
                SupplierID = 0
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
            ViewBag.Title = "Xóa nhà cung cấp";

            if (Request.HttpMethod == "POST")
            {
                //Xoa supplier co ma la id
                //Quay ve lai trang index
                DataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            else
            {
                //Lay thong tin supplier can xoa
                //tra thong tin ve cho view hien thi
                var model = DataService.GetSupplier(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Supplier data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("SupplierName", "Vui lòng nhập tên nhà cung cấp !");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Bạn chưa nhập tên giao dịch nhà cung cấp !");
                if (string.IsNullOrEmpty(data.Address))
                    data.Address = "";
                if (string.IsNullOrEmpty(data.Country))
                    data.Country = "";
                if (string.IsNullOrEmpty(data.City))
                    data.City = "";
                if (string.IsNullOrEmpty(data.PostalCode))
                    data.PostalCode = "";
                if (string.IsNullOrEmpty(data.Phone))
                    data.Phone = "";

                if (!ModelState.IsValid)
                {
                    if (data.SupplierID == 0)
                        ViewBag.Title = "Bổ sung nhà cung cấp";
                    else
                        ViewBag.Title = "Thay đổi nhà cung cấp";
                    return View("Edit", data);
                }

                if (data.SupplierID == 0)
                    DataService.AddSupplier(data);
                else
                    DataService.UpdateSupplier(data.SupplierID, data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
    }
}