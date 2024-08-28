using System.ComponentModel.DataAnnotations;

namespace TechStore.User.DTOs
{
    public class NewProductDTO
    {
        [Required(ErrorMessage = "The product Id is missing. Please try again.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The product quantity is missing. Please try again.")]
        public int Quantity { get; set; }   
    }
}
