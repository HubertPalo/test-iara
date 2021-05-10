using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestIARA.Application.Common.CotacaoItem.Models;
using TestIARA.Application.Common.Exceptions;
using TestIARA.Application.Common.Interfaces;
using TestIARA.Domain.Entities;

namespace TestIARA.Application.Common.CotacaoItem.Queries
{
    public class GetOneCotacaoItemQuery: IRequest<CotacaoItemDto>
    {
        public int NumeroCotacaoItem { get; set; }

        public class GetOneCotacaoItemQueryHandler : IRequestHandler<GetOneCotacaoItemQuery, CotacaoItemDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetOneCotacaoItemQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CotacaoItemDto> Handle(GetOneCotacaoItemQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cotacaoItem = _context.TCotacaoItem.Where(o => o.State && o.NumeroCotacaoItem == request.NumeroCotacaoItem).FirstOrDefault();

                    if (cotacaoItem == null)
                    {
                        throw new NotFoundException(nameof(TCotacaoItem), request.NumeroCotacaoItem);
                    }

                    var cotacaoItemDto = _mapper.Map<TCotacaoItem, CotacaoItemDto>(cotacaoItem);

                    return cotacaoItemDto;
                } catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
