using Microsoft.AspNetCore.Mvc;
using Middleware.Models;
using Middleware.Service;

namespace Middleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private IMainService _mainService;

        public MainController(IMainService mainService)
        {
            _mainService = mainService;
        }

        [HttpGet("requestInfo")]
        public IActionResult Get()
        {
            var response = _mainService.GetByIp(Request.HttpContext);
            if (response == null)
               return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                // Adicionamos na fila de processamento pelo Middleware
                //Nesse caso apenas retornamos que recebemos a requisição e o processamento acontecerá em background
                return NoContent();
            }
            catch (Exception ex)
            {
                // Coloquei no comentário no Middleware porém reforçando
                // Aqui vale uma discução se a Eception seria a melhor solução pelo custo de stack e geraçã ode exception
                // Para resolução do teste utilizei essa estratégia porém podemos discutir sobre um partner melhor para tratamento de exceção
                return BadRequest(ex.Message);
            }

        }
    }
}