// Entidade/Produto.cs
namespace ProjetoBarAcademia.Entidade
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public Produto(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Nome: {Nome} | Preço: R$ {Preco:F2}";
        }
    }
}