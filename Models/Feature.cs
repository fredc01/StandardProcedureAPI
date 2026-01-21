using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace StandardProcedureAPI.Models
{
    public class features : BaseModel
    {
        [PrimaryKey]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }

    public class FeatureDto
    {
        public Guid id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
