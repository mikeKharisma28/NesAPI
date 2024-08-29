using System.ComponentModel.DataAnnotations;

namespace Mike.Common.DataAccess.Models
{
    public abstract class DbBaseEntity
    {
        [Key]
        public string Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
