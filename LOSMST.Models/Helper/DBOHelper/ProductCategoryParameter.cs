﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class ProductCategoryParameter
    {
        public int? Id { get; set; }
        public string? Name { get; set; } = null;
        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
