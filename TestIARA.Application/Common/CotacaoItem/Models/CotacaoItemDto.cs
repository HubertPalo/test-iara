using AutoMapper;
using System.Collections.Generic;
using TestIARA.Application.Common.CotacaoItem.Models;
using TestIARA.Application.Common.Mappings;
using TestIARA.Domain.Entities;

namespace TestIARA.Application.Common.CotacaoItem.Models
{
    public class CotacaoItemDto : IMapFrom<TCotacaoItem>
    {
        public int NumeroCotacao { get; set; }

        public int NumeroCotacaoItem { get; set; }
        public string Descricao { get; set; }
        public int NumeroItem { get; set; }
        public int? Preco { get; set; }
        public int Quantidade { get; set; }
        public string? Marca { get; set; }
        public string? Unidade { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CotacaoItemDto, TCotacaoItem>();
            profile.CreateMap<TCotacaoItem, CotacaoItemDto>();
        }
    }
}
