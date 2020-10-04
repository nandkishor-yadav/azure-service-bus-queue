using System.ComponentModel.DataAnnotations;

namespace AzureBusServiceMVC.Models
{
    public class PayloadForServiceBus
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
