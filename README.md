# Projeto Bar e Restaurante - Sistema de Gestão

Este é um sistema de console para gerenciamento de um bar/resutaurante

### Módulo de Contas e Pedidos
- Abrir e Fechar Contas
- Adicionar e Remover Itens
- Cálculo Automático
- Visualização de Contas
- Faturamento Diário

### Módulo de Mesas
- Controle de Status
- Regras de Negócio: Garante que cada mesa tenha um número único e impede a exclusão se houver pedidos vinculados.

### Módulo de Garçons
- Validações: Valida o formato do CPF (`XXX.XXX.XXX-XX`) e garante que não existam CPFs duplicados.
- Regras de Negócio: Impede a exclusão de um garçom se ele estiver associado a algum pedido.

### Módulo de Produtos
- Regras de Negócio: Garante que os nomes dos produtos sejam únicos e impede a exclusão se um produto estiver em algum pedido ativo ou fechado.
