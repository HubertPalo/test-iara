using AutoMapper;
using MediatR;
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

namespace TestIARA.Application.Common.Cotacao.Commands.UpdateCotacao
{
    public class UpdateCotacaoCommand: IRequest<Unit>
    {
        public CotacaoDto data { get; set; }

        public class UpdateCotacaoCommandHandler: IRequestHandler<UpdateCotacaoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateCotacaoCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateCotacaoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.data == null)
                    {
                        throw new Exception("Empty request");
                    }

                    var cotacao = _context.TCotacao.Where(o => o.State && o.NumeroCotacao == request.data.NumeroCotacao).First();

                    if (cotacao == null)
                    {
                        throw new NotFoundException(nameof(TCotacao), request.data.NumeroCotacao);
                    }

                    cotacao = _mapper.Map<CotacaoDto, TCotacao>(request.data, cotacao);
                    _context.TCotacao.Update(cotacao);
                    await _context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
