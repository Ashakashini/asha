using System.ComponentModel.DataAnnotations;

namespace EmailSending.Models
{
    public class Contact
    {
        [Required(ErrorMessage ="This is a required field")]
        public string To {  get; set; }

        [Required(ErrorMessage = "This is a required field")]
        public string From { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        public string Body { get; set; }
    }
}
