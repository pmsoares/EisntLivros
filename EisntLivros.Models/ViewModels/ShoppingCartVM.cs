namespace EisntLivros.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; } = null!;

        public double CartTotal { get; set; }
    }
}
