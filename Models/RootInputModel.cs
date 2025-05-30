using System.Text.Json.Serialization;

public class RootInputModel
{
	[JsonPropertyName("pedidos")]
	public List<Pedido> Pedidos { get; set; }
}