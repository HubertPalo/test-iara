using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestIARA.Application.Common.CotacaoItem.Models;
using TestIARA.Application.Common.Interfaces;
using TestIARA.Application.Common.Models;

namespace TestIARA.Application.Common.CotacaoItem.Queries
{
    public class GetItemsFromCotacaoQuery: IRequest<ResultList<CotacaoItemDto>>
    {
        public int NumeroCotacao { get; set; }

        public class GetItemsFromCotacaoQueryHandler: IRequestHandler<GetItemsFromCotacaoQuery, ResultList<CotacaoItemDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetItemsFromCotacaoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ResultList<CotacaoItemDto>> Handle(GetItemsFromCotacaoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = new ResultList<CotacaoItemDto>();
                    var cotacaoItems = _context.TCotacaoItem
                        .Where(o => o.State && o.NumeroCotacaoItem == request.NumeroCotacao)
                        .ProjectTo<CotacaoItemDto>(_mapper.ConfigurationProvider)
                        .ToList();
                    
                    result.data = cotacaoItems;
                    result.count = cotacaoItems.Count;

                    return result;
                } catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
