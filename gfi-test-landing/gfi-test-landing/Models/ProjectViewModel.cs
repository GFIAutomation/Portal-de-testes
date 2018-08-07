using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gfi_test_landing.Models
{
    public class ProjectViewModel
    {
       

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public List<ItemComponet> checkboxList { get; set; }
    }

    public class ItemComponet
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}