using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ItemAggregate;
using System.Collections.Generic;
namespace SistemaCompra.Infra.Data.Item
{
    public class ItemRepository : IItemRepository
    {
        public ItemRepository() { }

        public void ValidaQuantidadeItens(IList<Domain.SolicitacaoCompraAggregate.Item> Itens)
        {
            if (Itens.Count.Equals(0)) throw new BusinessRuleException("Itens não informados");
        }
    }
}