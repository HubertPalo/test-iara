using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIARA.Application.Common.Cotacao.Models;
using TestIARA.Application.Common.CotacaoItem.Models;
using TestIARA.Application.Common.Interfaces;
using TestIARA.Domain.Entities;

namespace TestIARA.Application.Verificaciones.Commands.CreateVerifiacion
{

    public class CreateCotacaoCommand : IRequest<int>
    {
        public CotacaoDto data { get; set; }

        public class CreateCotacaoCommandHandler : IRequestHandler<CreateCotacaoCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ICEPService _CEPService;

            public CreateCotacaoCommandHandler(IApplicationDbContext context, IMapper mapper, ICEPService CEPService)
            {
                _context = context;
                _mapper = mapper;
                _CEPService = CEPService;
            }

            public async Task<int> Handle(CreateCotacaoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var newCotacao = _mapper.Map<CotacaoDto, TCotacao>(request.data);
                    newCotacao.NumeroCotacao = 0;

                    // Rules
                    var noLogradouro = String.IsNullOrWhiteSpace(request.data.Logradouro);
                    var noBairro = String.IsNullOrWhiteSpace(request.data.Bairro);
                    var noUF = String.IsNullOrWhiteSpace(request.data.UF);

                    if (noLogradouro && noBairro && noUF)
                    {
                        var cepData = _CEPService.SearchCEP(request.data.CEP);
                         
                        newCotacao.Logradouro =  cepData.logradouro;
                        newCotacao.Bairro = cepData.bairro;
                        newCotacao.UF = cepData.uf;
                    }

                    _context.TCotacao.Add(newCotacao);
                    await _context.SaveChangesAsync(cancellationToken);
                    return newCotacao.NumeroCotacao;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}