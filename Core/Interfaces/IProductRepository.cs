using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    //Normal method
    //Task<IReadOnlyList<Product>> GetProductsAsync();
    //adding filtering by brand and type 
    //Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type);
    //adding ordering by price as well
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? orderByPrice);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();

}
