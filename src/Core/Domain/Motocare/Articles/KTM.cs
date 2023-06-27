namespace FSH.WebApi.Domain.Motocare.Articles;

public class KtmArticle : AuditableEntity, IAggregateRoot
{
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

    public KtmArticle(string file, string articleNumber, string description, string currency, decimal purchasePriceExVat, decimal retailPriceExVat, string discountGroup, string unitOfMeasure, int salesPriceQuantity, int packageSize, int articleStatus, string productHierarchy, string hierarchyName1, string hierarchyName2, string hierarchyName3, string hierarchyName4, string hierarchyName5, string hierarchyName6, string hierarchyName7, DateTime priceValidFrom, string gtin, string countryOfOrigin, string impCode)
    {
        File = file;
        ArticleNumber = articleNumber;
        Description = description;
        Currency = currency;
        PurchasePriceExVat = purchasePriceExVat;
        RetailPriceExVat = retailPriceExVat;
        DiscountGroup = discountGroup;
        UnitOfMeasure = unitOfMeasure;
        SalesPriceQuantity = salesPriceQuantity;
        PackageSize = packageSize;
        ArticleStatus = articleStatus;
        ProductHierarchy = productHierarchy;
        HierarchyName1 = hierarchyName1;
        HierarchyName2 = hierarchyName2;
        HierarchyName3 = hierarchyName3;
        HierarchyName4 = hierarchyName4;
        HierarchyName5 = hierarchyName5;
        HierarchyName6 = hierarchyName6;
        HierarchyName7 = hierarchyName7;
        PriceValidFrom = priceValidFrom;
        GTIN = gtin;
        CountryOfOrigin = countryOfOrigin;
        ImpCode = impCode;
    }

    public KtmArticle Update(string file, string articleNumber, string description, string currency, decimal? purchasePriceExVat, decimal? retailPriceExVat, string discountGroup, string unitOfMeasure, int? salesPriceQuantity, int? packageSize, int? articleStatus, string productHierarchy, string hierarchyName1, string hierarchyName2, string hierarchyName3, string hierarchyName4, string hierarchyName5, string hierarchyName6, string hierarchyName7, DateTime? priceValidFrom, string gtin, string countryOfOrigin, string impCode)
    {
        if (!string.IsNullOrWhiteSpace(file) && !file.Equals(File, StringComparison.Ordinal)) File = file;
        if (!string.IsNullOrWhiteSpace(articleNumber) && !articleNumber.Equals(ArticleNumber, StringComparison.Ordinal)) ArticleNumber = articleNumber;
        if (!string.IsNullOrWhiteSpace(description) && !description.Equals(ArticleNumber, StringComparison.Ordinal)) Description = description;
        if (!string.IsNullOrWhiteSpace(currency) && !file.Equals(Currency, StringComparison.Ordinal)) Currency = currency;
        if (purchasePriceExVat.HasValue && purchasePriceExVat.Value != PurchasePriceExVat)
            PurchasePriceExVat = purchasePriceExVat.Value;
        if (retailPriceExVat.HasValue && retailPriceExVat.Value != RetailPriceExVat)
            RetailPriceExVat = retailPriceExVat.Value;
        if (!string.IsNullOrWhiteSpace(discountGroup) && !discountGroup.Equals(DiscountGroup, StringComparison.Ordinal)) Currency = discountGroup;
        if (!string.IsNullOrWhiteSpace(unitOfMeasure) && !unitOfMeasure.Equals(UnitOfMeasure, StringComparison.Ordinal)) Currency = unitOfMeasure;
        if (salesPriceQuantity.HasValue && salesPriceQuantity.Value != SalesPriceQuantity)
            SalesPriceQuantity = salesPriceQuantity.Value;
        if (packageSize.HasValue && packageSize.Value != PackageSize)
            PackageSize = packageSize.Value;
        if (articleStatus.HasValue && articleStatus.Value != ArticleStatus)
            ArticleStatus = articleStatus.Value;
        if (!string.IsNullOrWhiteSpace(productHierarchy) && !productHierarchy.Equals(ProductHierarchy, StringComparison.Ordinal)) ProductHierarchy = productHierarchy;
        if (!string.IsNullOrWhiteSpace(hierarchyName1) && !hierarchyName1.Equals(HierarchyName1, StringComparison.Ordinal)) HierarchyName1 = hierarchyName1;
        if (!string.IsNullOrWhiteSpace(hierarchyName2) && !hierarchyName2.Equals(HierarchyName2, StringComparison.Ordinal)) HierarchyName2 = hierarchyName2;
        if (!string.IsNullOrWhiteSpace(hierarchyName3) && !hierarchyName3.Equals(HierarchyName3, StringComparison.Ordinal)) HierarchyName3 = hierarchyName3;
        if (!string.IsNullOrWhiteSpace(hierarchyName4) && !hierarchyName4.Equals(HierarchyName4, StringComparison.Ordinal)) HierarchyName4 = hierarchyName4;
        if (!string.IsNullOrWhiteSpace(hierarchyName5) && !hierarchyName5.Equals(HierarchyName5, StringComparison.Ordinal)) HierarchyName5 = hierarchyName5;
        if (!string.IsNullOrWhiteSpace(hierarchyName6) && !hierarchyName6.Equals(HierarchyName6, StringComparison.Ordinal)) HierarchyName6 = hierarchyName6;
        if (!string.IsNullOrWhiteSpace(hierarchyName7) && !hierarchyName7.Equals(HierarchyName7, StringComparison.Ordinal)) HierarchyName7 = hierarchyName7;
        if (priceValidFrom.HasValue && priceValidFrom.Value != PriceValidFrom) PriceValidFrom = priceValidFrom.Value;
        if (!string.IsNullOrWhiteSpace(gtin) && !gtin.Equals(GTIN, StringComparison.Ordinal)) GTIN = gtin;
        if (!string.IsNullOrWhiteSpace(countryOfOrigin) && !countryOfOrigin.Equals(CountryOfOrigin, StringComparison.Ordinal)) CountryOfOrigin = file;
        if (!string.IsNullOrWhiteSpace(impCode) && !impCode.Equals(ImpCode, StringComparison.Ordinal)) ImpCode = impCode;

        return this;
    }
}