using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SupplierPaginationQueryResult : BasePaginationQueryResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}