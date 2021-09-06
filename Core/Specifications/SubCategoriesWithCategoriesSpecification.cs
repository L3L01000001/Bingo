using Core.Entities;

namespace Core.Specifications
{
    public class SubCategoriesWithCategoriesSpecification : BaseSpecification<SubCategory>
    {
        public SubCategoriesWithCategoriesSpecification()
        {
            AddInclude(x => x.Category);
        }
    }
}