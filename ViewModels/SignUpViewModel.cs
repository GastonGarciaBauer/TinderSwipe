using System.ComponentModel.DataAnnotations;

namespace TinderSwipe.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="El nombre es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="El Email es obligatorio.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }
        public IFormFile? ImgFile { get; set; }
        [Required(ErrorMessage ="La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debes aceptar los términos y condiciones")]
        public bool Consentimiento { get; set; }
    }
}
