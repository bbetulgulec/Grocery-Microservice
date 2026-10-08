namespace manav.catalog.Settings;

public interface IDatabaseSettings
{

    public String CategoryCollectionName { get; set; }
    public String ProductCollectionName { get; set; }
    public String ProductDetailCollectionName { get; set; }
    public String ProductImageCollectionName { get; set; }
    public String HighlightsProductCollectionName { get; set; }
    public String AboutCollectionName { get; set; }
    public String ContactCollectionName { get; set; }
    public String GalleryCollectionName { get; set; }
    public String ConnectionString { get; set; }
    public String DatabaseName { get; set; }



}