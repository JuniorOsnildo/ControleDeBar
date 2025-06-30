
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Repositorio;
using ProjetoBarAcademia.Tela;

namespace ProjetoBarAcademia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Instanciação dos repositórios
            RepositorioMesa repositorioMesa = new RepositorioMesa();
            RepositorioGarcom repositorioGarcom = new RepositorioGarcom();
            RepositorioProduto repositorioProduto = new RepositorioProduto();
            RepositorioConta repositorioConta = new RepositorioConta(repositorioMesa);

            // Carrega dados iniciais para facilitar os testes
            CarregarDadosIniciais(repositorioMesa, repositorioGarcom, repositorioProduto);

            // Instanciação das telas
            TelaMesa telaMesa = new TelaMesa(repositorioMesa, repositorioConta);
            TelaGarcom telaGarcom = new TelaGarcom(repositorioGarcom, repositorioConta);
            TelaProduto telaProduto = new TelaProduto(repositorioProduto, repositorioConta);
            TelaConta telaConta = new TelaConta(repositorioConta, repositorioMesa, repositorioGarcom, repositorioProduto);

            // Inicia a tela principal
            TelaPrincipal telaPrincipal = new TelaPrincipal(telaMesa, telaGarcom, telaProduto, telaConta);
            telaPrincipal.MostrarMenuPrincipal();
        }

        // Método para criar alguns dados para facilitar os testes
        private static void CarregarDadosIniciais(RepositorioMesa repoMesa, RepositorioGarcom repoGarcom, RepositorioProduto repoProduto)
        {
            if (repoMesa.SelecionarTodos().Count == 0)
            {
                repoMesa.Inserir(new Mesa(1, 4));
                repoMesa.Inserir(new Mesa(2, 2));
                repoMesa.Inserir(new Mesa(3, 6));
            }

            if (repoGarcom.SelecionarTodos().Count == 0)
            {
                repoGarcom.Inserir(new Garcom("João Silva", "111.111.111-11"));
                repoGarcom.Inserir(new Garcom("Maria Souza", "222.222.222-22"));
            }

            if (repoProduto.SelecionarTodos().Count == 0)
            {
                repoProduto.Inserir(new Produto("Coca-Cola Lata", 5.00m));
                repoProduto.Inserir(new Produto("X-Salada", 25.50m));
                repoProduto.Inserir(new Produto("Porção de Fritas", 30.00m));
            }
        }
    }
}