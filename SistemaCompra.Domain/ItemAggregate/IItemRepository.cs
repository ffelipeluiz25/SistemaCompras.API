using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;
namespace SistemaCompra.Domain.ItemAggregate
{
    public interface IItemRepository
    {
        void ValidaQuantidadeItens(IList<Item> Itens);
    }
}