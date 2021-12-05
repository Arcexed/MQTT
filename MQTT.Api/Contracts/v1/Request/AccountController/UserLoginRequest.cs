using System.ComponentModel.DataAnnotations;

namespace MQTT.Api.Contracts.v1.Request.AccountController
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email {get;set;}
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
    }
}