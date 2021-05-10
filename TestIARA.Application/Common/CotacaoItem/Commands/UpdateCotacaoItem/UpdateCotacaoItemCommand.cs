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

namespace TestIARA.Application.Common.CotacaoItem.Commands.UpdateCotacaoItem
{
    public class UpdateCotacaoItemCommand: IRequest<Unit>
    {
        public CotacaoItemDto data { get; set; }

        public class UpdateCotacaoItemCommandHandler: IRequestHandler<UpdateCotacaoItemCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateCotacaoItemCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(UpdateCotacaoItemCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.data == null)
                    {
                        throw new Exception("Empty request");
                    }

                    var cotacaoItem = _context.TCotacaoItem.Where(o => o.State && o.NumeroCotacaoItem == request.data.NumeroCotacaoItem).FirstOrDefault();

                    if (cotacaoItem == null)
                    {
                        throw new NotFoundException(nameof(TCotacaoItem), request.data.NumeroCotacaoItem);
                    }

                    cotacaoItem = _mapper.Map<CotacaoItemDto, TCotacaoItem>(request.data, cotacaoItem);
                    _context.TCotacaoItem.Update(cotacaoItem);
                    await _context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                } catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
