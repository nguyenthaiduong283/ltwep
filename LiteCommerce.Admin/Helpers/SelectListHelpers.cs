using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// Cung cap ham tien ich lien quan den SelectListItem
    /// </summary>
    public static class SelectListHelpers
    {
        /// <summary>
        /// Danh sach cac quoc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            
            foreach (var item in DataService.ListCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }

            return list;
        }

        public static List<SelectListItem> Cities()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in DataService.ListCities())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CityName,
                    Text = item.CityName
                });
            }

            return list;
        }

        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in DataService.ListCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value =Convert.ToString(item.CategoryID),
                    Text = item.CategoryName
                });
            }

            return list;
        }

        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in DataService.ListSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.SupplierID),
                    Text = item.SupplierName
                });
            }

            return list;
        }
    }
}