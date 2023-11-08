using Ecom.Domain.Entities;

namespace Ecom.Domain;

public class Product:BaseClass
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureUrl { get; set; }
    public ProductType? ProductType { get; set; }
    public int ProductTypeId { get; set; }
    public ProductBrand? ProductBrand { get; set; }
    public int ProductBrandId { get; set; }
}

public class ProductBrand : BaseClass
{
    public string? Name { get; set; }
}

public class ProductType : BaseClass
{
    public string? Name { get; set; }
}
