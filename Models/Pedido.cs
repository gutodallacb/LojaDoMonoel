using System.Text.Json.Serialization;

public class Pedido
{
	[JsonPropertyName("pedido_id")]
	public int PedidoId { get; set; }

	[JsonPropertyName("produtos")]
	public List<Produto> Produtos { get; set; }
}