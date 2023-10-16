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
                //Nesse caso apenas retornamos que recebemos a requisi��o e o processamento acontecer� em background
                return NoContent();
            }
            catch (Exception ex)
            {
                // Coloquei no coment�rio no Middleware por�m refor�ando
                // Aqui vale uma discu��o se a Eception seria a melhor solu��o pelo custo de stack e gera�� ode exception
                // Para resolu��o do teste utilizei essa estrat�gia por�m podemos discutir sobre um partner melhor para tratamento de exce��o
                return BadRequest(ex.Message);
            }

        }
    }
}