using Customer.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
	private readonly ICustomerService _service;

	public CustomerController(ICustomerService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		return Ok(await _service.GetCustomers());
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		return Ok(await _service.GetCustomer(id));
	}


}
