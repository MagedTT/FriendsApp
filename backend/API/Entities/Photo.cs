using System.Text.Json.Serialization;

namespace API.Entities;

public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? PublicId { get; set; }
    public string MemberId { get; set; } = string.Empty;

    [JsonIgnore]
    public Member Member { get; set; } = default!;
}