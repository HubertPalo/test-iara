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

namespace TestIARA.Application.Common.CotacaoItem.Commands.CreateCotacaoItem
{
    public class CreateCotacaoItemCommand: IRequest<int>
    {
        public CotacaoItemDto data { get; set; }

        public class CreateCotacaoItemCommandHandler : IRequestHandler<CreateCotacaoItemCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateCotacaoItemCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateCotacaoItemCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.data == null)
                    {
                        throw new Exception("Empty request");
                    }

                    var cotacao = _context.TCotacao.Where(o => o.State && o.NumeroCotacao == request.data.NumeroCotacao).FirstOrDefault();

                    if (cotacao == null)
                    {
                        throw new NotFoundException(nameof(TCotacao), request.data.NumeroCotacao);
                    }

                    var newCotacaoItem = _mapper.Map<CotacaoItemDto, TCotacaoItem>(request.data);
                    newCotacaoItem.NumeroCotacaoItem = 0;
                    _context.TCotacaoItem.Add(newCotacaoItem);
                    await _context.SaveChangesAsync(cancellationToken);
                    return newCotacaoItem.NumeroCotacaoItem;
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
