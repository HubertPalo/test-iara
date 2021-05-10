using System.Collections.Generic;
using TestIARA.Domain.Common;

namespace TestIARA.Domain.Entities
{
    public class TCotacao: AuditableEntity
    {
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
        public List<TCotacaoItem> CotacaoItems { get; set; }
    }
}
