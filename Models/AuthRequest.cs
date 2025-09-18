namespace ApiPersonalGestionFinance.Models;

/// <summary>
/// Modèle qui reçoit les paramètres d'authentification
/// </summary>
public class AuthRequest
{
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public string Username { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public int? AddressId { get; set; }
}
