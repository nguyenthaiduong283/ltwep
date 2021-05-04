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
    public class CategoryPaginationQueryResult : BasePaginationQueryResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Category> Data { get; set; }
    }
}