using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIARA.Application.Common.Cotacao.Commands.DeleteCotacao;
using TestIARA.Application.Common.Cotacao.Commands.UpdateCotacao;
using TestIARA.Application.Common.Cotacao.Models;
using TestIARA.Application.Common.Cotacao.Queries.GetOneCotacao;
using TestIARA.Application.Verificaciones.Commands.CreateVerifiacion;
using TestIARA.WebApi.Controllers;

namespace TestIARA.WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacaoController : ApiController
    {
        [HttpGet("{numeroCotacao}")]
        public async Task<ActionResult<CotacaoDto>> GetOne(int numeroCotacao)
        {
            return await Mediator.Send(new GetOneCotacaoQuery() { NumeroCotacao = numeroCotacao});
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOne([FromBody] CotacaoDto cotacao)
        {
            return await Mediator.Send(new CreateCotacaoCommand() { data = cotacao});
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateOne([FromBody] CotacaoDto cotacao)
        {
            return await Mediator.Send(new UpdateCotacaoCommand() { data = cotacao });
        }

        [HttpDelete("{numeroCotacao}")]
        public async Task<ActionResult<Unit>> DeleteOne(int numeroCotacao)
        {
            return await Mediator.Send(new DeleteCotacaoCommand() { NumeroCotacao = numeroCotacao});
        }
    }
}
