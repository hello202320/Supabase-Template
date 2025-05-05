namespace MYAPI.Models{
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;


[Table("messages")]
public class Message : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("content")]
    public string Content { get; set; }
}
}


