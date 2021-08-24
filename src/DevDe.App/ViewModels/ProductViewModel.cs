using DevDe.App.Extensions;
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

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("Fornecedor")]
        public Guid ProviderId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage ="O campo {0} precisa conter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa conter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("Imagem do produto")]
        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Coin]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateRegister { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        public ProviderViewModel Provider { get; set; }

        public IEnumerable<ProviderViewModel> Providers { get; set; }

    }
}
