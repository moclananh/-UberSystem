using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UberSystem.Domain.Entities;

public partial class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public byte[] CreateAt { get; set; } = null!;

    public long? UserId { get; set; }

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();

    public virtual ICollection<Trip> Trips { get; } = new List<Trip>();
    [JsonIgnore]
    public virtual User? User { get; set; }
}
