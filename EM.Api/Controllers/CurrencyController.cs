using EM.Business.Services;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EM.Api.Controllers
{
    
    [Route("api/em")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        [HttpGet("currency")]
        public async Task<IActionResult> GetCurrency()
        {
            var currencyList = await _currencyService.GetCurrency();
            var response = new ResponseDTO<List<CurrencyDTO>>(currencyList, "success", "Currencies returned successfully", null);
            return Ok(response);
        }
    }
}
