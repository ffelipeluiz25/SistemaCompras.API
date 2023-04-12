using MediatR;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System.Threading;
using System.Threading.Tasks;
namespace SistemaCompra.Application.Produto.Command.RegistrarProduto
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;

        public RegistrarCompraCommandHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator, IProdutoRepository produtoRepository) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            if (request.Itens.Count.Equals(0)) throw new BusinessRuleException("Itens não informados");
            foreach (Item item in request.Itens)
            {
                Domain.ProdutoAggregate.Produto produto = _produtoRepository.Obter(item.Produto.Id);
                if (produto == null) throw new BusinessRuleException("Produto não encontrado");
                item.Produto = produto;
            }

            var solicitacaoCompra = new SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor, request.CondicaoPagamento, request.Itens);
            solicitacaoCompra.ValidaSolicitacaoCompra();
            _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }
    }
}