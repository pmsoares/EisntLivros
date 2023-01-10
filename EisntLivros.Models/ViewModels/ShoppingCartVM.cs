namespace EisntLivros.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; } = null!;

        public OrderHeader OrderHeader { get; set; } = null!;
    }
}
