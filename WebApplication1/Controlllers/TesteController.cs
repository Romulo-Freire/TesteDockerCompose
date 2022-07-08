using Domain.DomainUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controlllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class TesteController : ControllerBase
    {
        IDomainUser _domainUser;
        public TesteController(
             IDomainUser domainUser
            )
        {
            _domainUser = domainUser;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            /*
            var retorno = await _domainUser.Listar();
            if (retorno.Value != null)
                return Ok(retorno.Value);

           
            return BadRequest();  */

            
            var retorno = await _domainUser.Criar();
            return Ok(retorno.Value); 

            /*
            await _domainUser.Apagar();
            return Ok(); */

            /*
            var serv = new TokenService();
            return Ok(serv.Authenticate()); */
        }
    }
}
