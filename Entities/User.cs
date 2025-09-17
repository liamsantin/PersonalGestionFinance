using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersonalGestionFinance.Entities;

/// <summary>
/// Entity of an user 
/// </summary>
public class User
{
    public int UserId { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Iban { get; set; } = ""; 
    public string Phone { get; set; }
    // address
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    // country
    public string? Country { get; set; }
    public string ISO { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
}