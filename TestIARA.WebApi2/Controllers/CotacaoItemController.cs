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
using TestIARA.Application.Common.CotacaoItem.Commands.CreateCotacaoItem;
using TestIARA.Application.Common.CotacaoItem.Commands.DeleteCotacaoItem;
using TestIARA.Application.Common.CotacaoItem.Commands.UpdateCotacaoItem;
using TestIARA.Application.Common.CotacaoItem.Models;
using TestIARA.Application.Common.CotacaoItem.Queries;
using TestIARA.Application.Common.Models;
using TestIARA.Application.Verificaciones.Commands.CreateVerifiacion;
using TestIARA.WebApi.Controllers;

namespace TestIARA.WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacaoItemController : ApiController
    {
        [HttpGet("specific_item/{numeroCotacaoItem}")]
        public async Task<ActionResult<CotacaoItemDto>> GetOne(int numeroCotacaoItem)
        {
            return await Mediator.Send(new GetOneCotacaoItemQuery() { NumeroCotacaoItem = numeroCotacaoItem });
        }

        [HttpGet("items_from_cotacao/{numeroCotacao}")]
        public async Task<ActionResult<ResultList<CotacaoItemDto>>> GetAllFromCotacao(int numeroCotacao)
        {
            return await Mediator.Send(new GetItemsFromCotacaoQuery() { NumeroCotacao = numeroCotacao });
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOne([FromBody] CotacaoItemDto cotacaoItem)
        {
            return await Mediator.Send(new CreateCotacaoItemCommand() { data = cotacaoItem });
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateOne([FromBody] CotacaoItemDto cotacaoItem)
        {
            return await Mediator.Send(new UpdateCotacaoItemCommand() { data = cotacaoItem });
        }

        [HttpDelete("{numeroCotacaoItem}")]
        public async Task<ActionResult<Unit>> DeleteOne(int numeroCotacaoItem)
        {
            return await Mediator.Send(new DeleteCotacaoItemCommand() { NumeroCotacaoItem = numeroCotacaoItem });
        }
    }
}
