namespace manav.catalog.Settings;

public class DatabaseSettings:IDatabaseSettings
{
    public string CategoryCollectionName { get; set; }
    public string ProductCollectionName { get; set; }
    public string ProductDetailCollectionName { get; set; }
    public string ProductImageCollectionName { get; set; }
    public string HighlightsProductCollectionName { get; set; }
    public string AboutCollectionName { get; set; }
    public string ContactCollectionName { get; set; }
    public string GalleryCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}