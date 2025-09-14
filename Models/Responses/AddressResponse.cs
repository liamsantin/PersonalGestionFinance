namespace ApiPersonalGestionFinance.Models.Responses;

public class AddressResponse
{
    // ta_address
    public int AddressId { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }

    // ta_country
    public string Country { get; set; }
    public string ISO { get; set; }
}
