using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersonalGestionFinance.Entities;


[Table("TA_ADDRESS")]
public class Address
{
    [Key]
    [Column("addr_id")]
    public int AddressId { get; set; }

    [Required]
    [Column("addr_street")]
    public string Street { get; set; }

    [Required]
    [Column("addr_postalCode")]
    public string PostalCode { get; set; }

    [Required]
    [Column("addr_city")]
    public string City { get; set; }

    [Required]
    [Column("addr_country")]
    public string Country { get; set; } = "CH";

    // Relation inverse : une adresse peut appartenir à plusieurs utilisateurs
    public ICollection<User> Users { get; set; } = new List<User>();
}
