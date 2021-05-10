using System;
using System.Collections.Generic;
using System.Text;
using TestIARA.Domain.Common;

namespace TestIARA.Domain.Entities
{
    public class TCotacaoItem: AuditableEntity
    {
        public int NumeroCotacao { get; set; }
        public int NumeroCotacaoItem { get; set; }
        public string Descricao { get; set; }
        public int NumeroItem { get; set; }
        public int? Preco { get; set; }
        public int Quantidade { get; set; }
        public string? Marca { get; set; }
        public string? Unidade { get; set; }

        public TCotacao Cotacao { get; set; }
    }
}
