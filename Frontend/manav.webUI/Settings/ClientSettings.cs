namespace manav.webUI.Settings;

public class ClientSettings
{
    public Client manavVisitorClient { get; set; }
    public Client manavManagerClient { get; set; }
    public Client manavAdminClient { get; set; }
    
}
public class Client
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}