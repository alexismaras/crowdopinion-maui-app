using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdOpinion.Models
{
    [Table("profiles")] // Maps to the PostgreSQL table name
    public class ProfileObject : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        // User reference (must match database column name exactly)
        [Column("user_id")]
        public string UserId { get; set; } 

        [Column("created_at", ignoreOnInsert: true)]
        public DateTime CreatedAt { get; set; }

        [Column("username")]
        public string UserName { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("bio")]
        public string Bio { get; set; }

        [Column("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}