using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestIARA.Application.Common.Cotacao.Models;
using TestIARA.Application.Common.Exceptions;
using TestIARA.Application.Common.Interfaces;
using TestIARA.Domain.Entities;

namespace TestIARA.Application.Common.Cotacao.Queries.GetOneCotacao
{
    public class GetOneCotacaoQuery: IRequest<CotacaoDto>
    {
        public int NumeroCotacao { get; set; }

        public class GetOneCotacaoQueryHandler : IRequestHandler<GetOneCotacaoQuery, CotacaoDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetOneCotacaoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CotacaoDto> Handle(GetOneCotacaoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cotacao = _context.TCotacao.Where(o => o.State && o.NumeroCotacao == request.NumeroCotacao).FirstOrDefault();

                    if (cotacao == null)
                    {
                        throw new NotFoundException(nameof(TCotacao), request.NumeroCotacao);
                    }

                    var cotacaoDto = _mapper.Map<TCotacao, CotacaoDto>(cotacao);

                    return cotacaoDto;
                } catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
