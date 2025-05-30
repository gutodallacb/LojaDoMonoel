public class EmpacotadorService
{
	private readonly List<Caixa> _caixasDisponiveis;

	public EmpacotadorService()
	{
		_caixasDisponiveis = new List<Caixa>
		{
			new Caixa { CaixaId = "Caixa 1", Dimensoes = new Dimensoes { Altura = 30, Largura = 40, Comprimento = 80 } },
			new Caixa { CaixaId = "Caixa 2", Dimensoes = new Dimensoes { Altura = 80, Largura = 50, Comprimento = 40 } },
			new Caixa { CaixaId = "Caixa 3", Dimensoes = new Dimensoes { Altura = 50, Largura = 80, Comprimento = 60 } }
		};
	}

	public List<CaixaResultado> EmpacotarPedidos(List<Pedido> pedidos)
	{
		var resultados = new List<CaixaResultado>();

		foreach (var pedido in pedidos)
		{
			var produtosNaoEmpacotados = new List<Produto>(pedido.Produtos);
			produtosNaoEmpacotados.Sort((a, b) => b.Dimensoes.Volume().CompareTo(a.Dimensoes.Volume())); // Do maior pro menor

			while (produtosNaoEmpacotados.Any())
			{
				bool produtoEmpacotado = false;

				foreach (var caixa in _caixasDisponiveis)
				{
					var produtosNaCaixa = new List<Produto>();
					double volumeDisponivel = caixa.Dimensoes.Volume();

					foreach (var produto in produtosNaoEmpacotados.ToList())
					{
						if (ProdutoCabeNaCaixa(produto, caixa) && produto.Dimensoes.Volume() <= volumeDisponivel)
						{
							produtosNaCaixa.Add(produto);
							volumeDisponivel -= produto.Dimensoes.Volume();
							produtosNaoEmpacotados.Remove(produto);
						}
					}

					if (produtosNaCaixa.Any())
					{
						resultados.Add(new CaixaResultado
						{
							CaixaId = caixa.CaixaId,
							Produtos = produtosNaCaixa.Select(p => p.ProdutoId).ToList(),
							Observacao = $"Produtos do Pedido {pedido.PedidoId} alocados na {caixa.CaixaId}"
						});
						produtoEmpacotado = true;
						break;
					}
				}

				if (!produtoEmpacotado)
				{
					// Nenhuma caixa disponível consegue armazenar o produto
					var produto = produtosNaoEmpacotados.First();
					resultados.Add(new CaixaResultado
					{
						CaixaId = null,
						Produtos = new List<string> { produto.ProdutoId },
						Observacao = $"Produto {produto.ProdutoId} do Pedido {pedido.PedidoId} não cabe em nenhuma caixa."
					});
					produtosNaoEmpacotados.Remove(produto);
				}
			}
		}

		return resultados;
	}

	private bool ProdutoCabeNaCaixa(Produto produto, Caixa caixa)
	{
		return produto.Dimensoes.Altura <= caixa.Dimensoes.Altura &&
			   produto.Dimensoes.Largura <= caixa.Dimensoes.Largura &&
			   produto.Dimensoes.Comprimento <= caixa.Dimensoes.Comprimento;
	}
}
