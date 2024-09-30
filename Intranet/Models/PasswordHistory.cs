using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class PasswordHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PasswordHash { get; set; }
        public int CountPassword { get; set; }
        public bool ResetPassword { get; set; }
        public bool PasswordLock { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool Active { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

    }
}
