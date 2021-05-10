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

namespace TestIARA.Application.Common.CotacaoItem.Commands.DeleteCotacaoItem
{
    public class DeleteCotacaoItemCommand: IRequest<Unit>
    {
        public int NumeroCotacaoItem { get; set; }

        public class DeleteCotacaoItemCommandHandler : IRequestHandler<DeleteCotacaoItemCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public DeleteCotacaoItemCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(DeleteCotacaoItemCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var cotacaoItem = _context.TCotacaoItem.Where(o => o.State && o.NumeroCotacaoItem == request.NumeroCotacaoItem).FirstOrDefault();

                    if (cotacaoItem == null)
                    {
                        throw new NotFoundException(nameof(TCotacaoItem), request.NumeroCotacaoItem);
                    }
                    cotacaoItem.State = false;
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
