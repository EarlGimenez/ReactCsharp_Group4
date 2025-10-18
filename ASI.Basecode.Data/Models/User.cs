using System;

namespace ASI.Basecode.Data.Models
{
    public partial class User
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        
        // Keeping old properties for backward compatibility
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
    }
}
