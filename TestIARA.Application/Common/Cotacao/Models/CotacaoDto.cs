using AutoMapper;
using System.Collections.Generic;
using TestIARA.Application.Common.CotacaoItem.Models;
using TestIARA.Application.Common.Mappings;
using TestIARA.Domain.Entities;

namespace TestIARA.Application.Common.Cotacao.Models
{
    public class CotacaoDto : IMapFrom<TCotacao>
    {
        public CotacaoDto()
        {
            CotacaoItems = new List<CotacaoItemDto>();
        }
        public string CNPJComprador { get; set; }
        public string CNPJFornecedor { get; set; }
        public int NumeroCotacao { get; set; }
        public string DataCotacao { get; set; }
        public string DataEntregaCotacao { get; set; }
        public string CEP { get; set; }
        public string? Logradouro { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? UF { get; set; }
        public string? Observacao { get; set; }
        public List<CotacaoItemDto> CotacaoItems { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CotacaoDto, TCotacao>();
            profile.CreateMap<TCotacao, CotacaoDto>();
        }
    }
}
