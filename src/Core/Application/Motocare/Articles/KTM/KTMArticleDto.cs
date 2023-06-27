namespace FSH.WebApi.Application.Motocare.Articles.Ktm;

public class KtmArticleDto : IDto
{
    public Guid Id { get; set; }
    public string File { get; set; }
    public string ArticleNumber { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public decimal PurchasePriceExVat { get; set; }
    public decimal RetailPriceExVat { get; set; }
    public string DiscountGroup { get; set; }
    public string UnitOfMeasure { get; set; }
    public int SalesPriceQuantity { get; set; }
    public int PackageSize { get; set; }
    public int ArticleStatus { get; set; }
    public string ProductHierarchy { get; set; }
    public string HierarchyName1 { get; set; }
    public string HierarchyName2 { get; set; }
    public string HierarchyName3 { get; set; }
    public string HierarchyName4 { get; set; }
    public string HierarchyName5 { get; set; }
    public string HierarchyName6 { get; set; }
    public string HierarchyName7 { get; set; }
    public DateTime PriceValidFrom { get; set; }
    public string GTIN { get; set; }
    public string CountryOfOrigin { get; set; }
    public string ImpCode { get; set; }
}