// Entidade/Mesa.cs
using ProjetoBarAcademia.Enum;

namespace ProjetoBarAcademia.Entidade
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int QuantidadeLugares { get; set; }
        public StatusMesa Status { get; set; }

        public Mesa(int numero, int quantidadeLugares)
        {
            Numero = numero;
            QuantidadeLugares = quantidadeLugares;
            Status = StatusMesa.Livre; // Status padrão
        }

        public override string ToString()
        {
            return $"ID: {Id} | Número: {Numero} | Lugares: {QuantidadeLugares} | Status: {Status}";
        }
    }
}