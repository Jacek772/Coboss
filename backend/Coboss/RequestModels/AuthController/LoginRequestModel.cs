using System.ComponentModel.DataAnnotations;

namespace Coboss.RequestModels.AuthController
{
    public class LoginRequestModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
