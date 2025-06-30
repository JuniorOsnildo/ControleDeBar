// Entidade/Garcom.cs
namespace ProjetoBarAcademia.Entidade
{
    public class Garcom
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public Garcom(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Nome: {Nome} | CPF: {Cpf}";
        }
    }
}