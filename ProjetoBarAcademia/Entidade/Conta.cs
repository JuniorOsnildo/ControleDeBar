// Entidade/Conta.cs
using ProjetoBarAcademia.Enum;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ProjetoBarAcademia.Entidade
{
    public class Conta
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public Mesa Mesa { get; set; }
        public Garcom Garcom { get; set; }
        public List<ItemPedido> Itens { get; private set; }
        public StatusConta Status { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public decimal ValorTotal => Itens.Sum(item => item.Subtotal);
        public decimal Gorjeta => ValorTotal * 0.10m; // Exemplo de gorjeta de 10%
        public decimal TotalAPagar => ValorTotal + Gorjeta;

        public Conta(string nomeCliente, Mesa mesa, Garcom garcom)
        {
            NomeCliente = nomeCliente;
            Mesa = mesa;
            Garcom = garcom;
            Itens = new List<ItemPedido>();
            Status = StatusConta.Aberta;
            DataAbertura = DateTime.Now;
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            var itemExistente = Itens.FirstOrDefault(i => i.Produto.Id == produto.Id);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                Itens.Add(new ItemPedido(produto, quantidade));
            }
        }

        public bool RemoverItem(Produto produto, int quantidade)
        {
            var itemExistente = Itens.FirstOrDefault(i => i.Produto.Id == produto.Id);
            if (itemExistente == null || itemExistente.Quantidade < quantidade)
            {
                return false;
            }

            itemExistente.Quantidade -= quantidade;
            if (itemExistente.Quantidade == 0)
            {
                Itens.Remove(itemExistente);
            }
            return true;
        }

        public void FecharConta()
        {
            Status = StatusConta.Fechada;
            DataFechamento = DateTime.Now;
            Mesa.Status = StatusMesa.Livre;
        }
    }
}