using MediatR;
using SistemaCompra.Domain.ItemAggregate;
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
        private readonly IItemRepository _itemRepository;

        public RegistrarCompraCommandHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator, IProdutoRepository produtoRepository) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            //Não foi solicitado no teste, mas achei importante validar
            _itemRepository.ValidaQuantidadeItens(request.Itens);

            var solicitacaoCompra = new SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor, request.CondicaoPagamento);
            foreach (var item in request.Itens)
                solicitacaoCompra.AdicionarItem(item.Produto, item.Qtde);

            solicitacaoCompra.ValidaSolicitacaoCompra();
            solicitacaoCompra.CalculaTotalGeral();
            _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }
    }
}