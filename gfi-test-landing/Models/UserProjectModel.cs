using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gfi_test_landing.Models
{
    public class UserProjectModel
    {
        public string id_user { get; set; }
        public int id_project { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
       

        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Project Project { get; set; }
    }
}