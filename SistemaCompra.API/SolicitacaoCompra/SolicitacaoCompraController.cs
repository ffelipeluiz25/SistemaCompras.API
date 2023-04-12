using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.Produto.Command.RegistrarProduto;
namespace SistemaCompra.API.Produto
{
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost, Route("solicitacao-compra")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult SolicitacaoCompra([FromBody] RegistrarCompraCommand registrarProdutoCommand)
        {
            _mediator.Send(registrarProdutoCommand);
            return StatusCode(201);
        }
    }
}