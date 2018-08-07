using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gfi_test_landing.Models
{
    public class UserRoleProjectModel
    {
        public string Id { get; set; }
        public IEnumerable<RoleProjectByUserModel> RoleProjetByUser { get; set; }
       // public IEnumerable<DropRoleProjectByUserModel> DropRoleProjetByUser { get; set; }

    }

    public class RoleProjectByUserModel
    {
        public string IdUser { get; set; }
        public string IdRole { get; set; }
        public int IdProject { get; set; }
        public string NameRole { get; set; }
        public string NameProject { get; set; }
        public string DescriptionProject { get; set; }
    }

    public class DropRoleProjectByUserModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int IdProject { get; set; }
        public string NameRole { get; set; }
        public string ImageUser { get; set; }
        public string IdUser { get; set; }
        public string IdRole { get; set; }
        public string NameProject { get; set; }

    }

    
}