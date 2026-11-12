using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.DTOs
{
    public class UpdateUserRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(254)]
        public string Email { get; set; } = string.Empty;

        [Range(1, 120)]
        public int Age { get; set; }
    }
}