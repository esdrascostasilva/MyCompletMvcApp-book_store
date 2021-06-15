using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevDe.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayName("Provider")]
        public Guid ProviderId { get; set; }

        [Required(ErrorMessage ="The field {0} is mandatory")]
        [StringLength(200, ErrorMessage ="The field {0} need have between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(1000, ErrorMessage = "The field {0} need have between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateRegister { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }

        public ProviderViewModel Provider { get; set; }

        public IEnumerable<ProviderViewModel> Providers { get; set; }

    }
}
