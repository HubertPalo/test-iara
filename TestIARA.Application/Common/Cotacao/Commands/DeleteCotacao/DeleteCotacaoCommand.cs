using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIARA.Application.Common.Exceptions;
using TestIARA.Application.Common.Interfaces;
using TestIARA.Domain.Entities;

namespace TestIARA.Application.Common.Cotacao.Commands.DeleteCotacao
{
    public class DeleteCotacaoCommand : IRequest<Unit>
    {
        public int NumeroCotacao { get; set; }

        public class DeleteCotacaoCommandHandler : IRequestHandler<DeleteCotacaoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            //private readonly IPlanAccionService _planAccion;
            //private readonly IImagenesService _imagenes;

            public DeleteCotacaoCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteCotacaoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var cotacao = _context.TCotacao.Where(o => o.State && o.NumeroCotacao == request.NumeroCotacao).First();

                    if (cotacao == null)
                    {
                        throw new NotFoundException(nameof(TCotacao), request.NumeroCotacao);
                    }
                    
                    cotacao.State = false;
                    _context.TCotacao.Update(cotacao);
                    
                    if (cotacao.CotacaoItems != null)
                    {
                        foreach (var item in cotacao.CotacaoItems)
                        {
                            item.State = false;
                            _context.TCotacaoItem.Update(item);
                        }
                    }
                    
                    await _context.SaveChangesAsync(cancellationToken);
                    

                    /*var items = _context.TCotacaoITem.Where(o => o.State && o.NumeroCotacao == request.NumeroCotacao);
                    foreach (var item in items)
                    {

                    }*/
                    
                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
