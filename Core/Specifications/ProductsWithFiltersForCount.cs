using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCount : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCount(ProductSpecParams productParams) :
            base(
                x => 
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
                (!productParams.SubCategoryId.HasValue || x.SubCategoryId == productParams.SubCategoryId)
            )
        {
        }
    }
}