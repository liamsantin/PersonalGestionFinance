namespace ApiPersonalGestionFinance.Models;

/// <summary>
/// Modèle qui reçoit les paramètres d'authentification
/// </summary>
public class AuthRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
