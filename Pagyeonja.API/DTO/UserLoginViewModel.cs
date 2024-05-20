using System.ComponentModel.DataAnnotations;

namespace Pagyeonja.API.DTO
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string? Username { get; set; }   
        
        [Required(ErrorMessage ="Password is required")]
        public string? Password { get; set; }   
    }
}