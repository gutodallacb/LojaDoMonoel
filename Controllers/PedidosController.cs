using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CaixaController : ControllerBase
{
	private readonly EmpacotadorService _empacotadorService;

	public CaixaController()
	{
		_empacotadorService = new EmpacotadorService();
	}

	[HttpPost]
	public ActionResult<List<CaixaResultado>> Post([FromBody] RootInputModel input)
	{
		var resultado = _empacotadorService.EmpacotarPedidos(input.Pedidos);
		return Ok(resultado);
	}
}
