// Entidade/ItemPedido.cs
namespace ProjetoBarAcademia.Entidade
{
    public class ItemPedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;

        public ItemPedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = produto.Preco;
        }

        public override string ToString()
        {
            return $"{Quantidade}x {Produto.Nome} (R$ {PrecoUnitario:F2} un.) - Subtotal: R$ {Subtotal:F2}";
        }
    }
}