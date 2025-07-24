using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philbor.Domain.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }

        public Guid? InvitationId { get; set; }

        public int? CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser? CreatedByUser { get; set; }
        [ForeignKey(nameof(LastModifiedBy))]
        public ApplicationUser? LastModifiedByUser { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
