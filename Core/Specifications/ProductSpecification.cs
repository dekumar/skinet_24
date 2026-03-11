using System;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string? brand, string? type, string? sort) : base(p =>
        (string.IsNullOrEmpty(brand) || p.Brand == brand) && (string.IsNullOrEmpty(type) || p.Type == type)
    )
    {
        switch (sort)
        {
            case "priceAsc":
                AddOrderBy(x=>x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x=>x.Price);
                break;
            default:
                AddOrderBy(x=>x.Name);
                break;
        }
    }
}
