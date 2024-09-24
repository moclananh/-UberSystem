using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UberSystem.Domain.Entities;

public partial class User
{
    public long Id { get; set; }

    public int? Role { get; set; }

    public string? UserName { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    [Column("emailVerified")]
    public bool EmailVerified { get; set; }
    
    [Column("emailVerifiedToken")]
    public string? EmailVerificationToken { get; set; }
    [JsonIgnore]
    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
    [JsonIgnore]
    public virtual ICollection<Driver> Drivers { get; } = new List<Driver>();
}
