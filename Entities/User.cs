using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersonalGestionFinance.Entities;

[Table("TA_USER")]
public class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [Column("user_nom")]
    public string Nom { get; set; }

    [Required]
    [Column("user_prenom")]
    public string Prenom { get; set; }

    [Required]
    [Column("user_email")]
    public string Email { get; set; }

    [Required]
    [Column("user_password")]
    public string Password { get; set; }

    [Required]
    [Column("user_iban")]
    [StringLength(21, MinimumLength = 21)]
    public string Iban { get; set; }

    [Column("user_phone")]
    public string? Phone { get; set; }

    // Clé étrangère vers TA_ADDRESS
    [Column("addr_id")]
    public int? AddressId { get; set; }

    [ForeignKey("AddressId")]
    public Address? Address { get; set; }

    [Required]
    [Column("user_createAt")]
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("user_updateAt")]
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
}