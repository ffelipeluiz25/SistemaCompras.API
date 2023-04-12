using SistemaCompra.Domain.SolicitacaoCompraAggregate;
namespace SistemaCompra.Infra.Data.Produto
{
    public class SolicitacaoCompraRepository : ISolicitacaoCompraRepository
    {
        private readonly SistemaCompraContext context;

        public SolicitacaoCompraRepository(SistemaCompraContext context)
        {
            this.context = context;
        }

        public void RegistrarCompra(SolicitacaoCompra solicitacaoCompra)
        {
            context.Set<SolicitacaoCompra>().Add(solicitacaoCompra);
        }
    }
}