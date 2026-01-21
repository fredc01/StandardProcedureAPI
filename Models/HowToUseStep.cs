using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace StandardProcedureAPI.Models
{
    public class how_to_use_steps : BaseModel
    {
        [PrimaryKey]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();

        [Column("step_number")]
        public int step_number { get; set; }

        [Column("description")]
        public string description { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTimeOffset created_at { get; set; } = DateTimeOffset.UtcNow;
    }

    public class HowToUseStepDto
    {
        public Guid id { get; set; }
        public int step_number { get; set; }
        public string description { get; set; }
        public DateTimeOffset created_at { get; set; }
    }

}
