using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            var listCustomers = DataService.ListCustomers(page, pageSize, searchValue, out rowCount);

            var model = new Models.CustomerPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listCustomers
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
            ViewBag.Title = "Thay đổi thông tin khách hàng";
            var model = DataService.GetCustomer(id);
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
            ViewBag.Title = "Thêm thông tin khách hàng";
            Customer model = new Customer()
            {
                CustomerID = 0
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
            ViewBag.Title = "Xóa khách hàng";
            if (Request.HttpMethod == "POST")
            {
                //Xoa Customer co ma la id
                //Quay ve lai trang index
                DataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            else
            {
                //Lay thong tin customer can xoa
                //tra thong tin ve cho view hien thi
                var model = DataService.GetCustomer(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Customer data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Vui long nhap ten khách hàng !");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Ban chua nhap ten lien he cua nha cung cap !");
                if (string.IsNullOrEmpty(data.Address))
                    data.Address = "";
                if (string.IsNullOrEmpty(data.Country))
                    data.Country = "";
                if (string.IsNullOrEmpty(data.City))
                    data.City = "";
                if (string.IsNullOrEmpty(data.PostalCode))
                    data.PostalCode = "";
                if (string.IsNullOrEmpty(data.Email))
                    data.Email = "";
                if (string.IsNullOrEmpty(data.Password))
                    data.Password = "";

                if (!ModelState.IsValid)
                {
                    if (data.CustomerID == 0)
                        ViewBag.Title = "Bổ sung khách hàng";
                    else
                        ViewBag.Title = "Thay đổi thông tin khách hàng";
                    return View("Edit", data);
                }

                if (data.CustomerID == 0)
                    DataService.AddCustomer(data);
                else
                    DataService.UpdateCustomer(data.CustomerID, data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
    }
}