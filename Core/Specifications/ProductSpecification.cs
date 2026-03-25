using System;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProdSpecParamCls prodSpecParamClassObj) :
        base(p =>
            (string.IsNullOrEmpty(prodSpecParamClassObj.Search) || p.Name.ToLower().Contains(prodSpecParamClassObj.Search))
            && (!prodSpecParamClassObj.Brands.Any() || prodSpecParamClassObj.Brands.Contains(p.Brand))
            && (!prodSpecParamClassObj.Types.Any() || prodSpecParamClassObj.Types.Contains(p.Type))
    )
    {

        ApplyPaging(prodSpecParamClassObj.PageSize * (prodSpecParamClassObj.PageIndex - 1), prodSpecParamClassObj.PageSize);

        switch (prodSpecParamClassObj.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}
