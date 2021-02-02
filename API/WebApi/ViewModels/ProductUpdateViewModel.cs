using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels
{
    public class ProductUpdateViewModel
    {
        [Required(ErrorMessage = "O campo Id é obrigatório")]        
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Name é obrigatório")]
        [MaxLength(200, ErrorMessage = "O campo Name não pode ter mais do que 200 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo SalePrice é obrigatório")]
        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}
