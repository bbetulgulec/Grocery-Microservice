namespace manav.webUI.Models;

public class JwtResponseModel
{
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
}