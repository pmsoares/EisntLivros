namespace EisntLivros.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        ICoverTypeRepository CoverType { get; }

        IProductRepository Product { get; }

        ICompanyRepository Company { get; }

        IShoppingCartRepository ShoppingCart { get; }

        IApplicationUserRepository ApplicationUser { get; }

        void Save();
    }
}
