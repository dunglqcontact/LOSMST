﻿using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class ExportInventoryParameter
    {
        public string? Id { get; set; } = null!;
        public DateTime? ExportDate { get; set; }
        public int? StoreId { get; set; }

        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
