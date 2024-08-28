using System.ComponentModel.DataAnnotations;
using TechStore.User.DTOs;

namespace TechStore.User.Payload
{
    public class CreateOrderRequest
    {
        [Required (ErrorMessage = "One of the items in the product list is missing. Please try again.")]
        public List<NewProductDTO> Products {  get; set; }
        [Required(ErrorMessage = "The user token is missing. Have you added it yet?")]
        public string Token { get; set; }   
    }
}
