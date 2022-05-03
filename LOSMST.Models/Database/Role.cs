using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOSMST.Models.Database
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        [InverseProperty(nameof(Account.Role))]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
