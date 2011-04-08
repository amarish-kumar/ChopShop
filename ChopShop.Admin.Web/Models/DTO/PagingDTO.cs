using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChopShop.Admin.Web.Models.DTO
{
    [Serializable]
    public class PagingDTO
    {
        public int PerPage { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
        public int TotalPages { get; set; }
    }
}