using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverageAttribute]
    public class LoginModelIn
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
