using System;
using System.Collections.Generic;
using System.Text;

namespace LOSMST.Models.Helper.UpdatePassword
{
    public class UpdatePassword
    {
        public int accountId { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
