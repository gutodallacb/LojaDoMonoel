using System.Text.Json.Serialization;

public class Dimensoes
{
	[JsonPropertyName("altura")]
	public double Altura { get; set; }

	[JsonPropertyName("largura")]
	public double Largura { get; set; }

	[JsonPropertyName("comprimento")]
	public double Comprimento { get; set; }

	public double Volume() => Altura * Largura * Comprimento;
}
