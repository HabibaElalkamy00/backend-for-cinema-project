using System.ComponentModel.DataAnnotations;

namespace MoviesReservation.ViewModel
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression(@"^01[0-2]{1}[0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter an email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a card number.")]
        public int CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter a CVV.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 digits.")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Please enter an expiry date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [ExpiryDateValidation(ErrorMessage = "Please enter a valid expiry date.")]
        public DateTime ExpiryDate { get; set; }
    }
    public class ExpiryDateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime expiryDate)
            {
                return expiryDate > DateTime.Now;
            }

            return false;
        }
    }
}
