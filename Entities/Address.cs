using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersonalGestionFinance.Entities;

/// <summary>
/// Entity of an address
/// </summary>
public class Address
{
    public int AddressId { get; set; }
    public string Street { get; set; }    
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}
