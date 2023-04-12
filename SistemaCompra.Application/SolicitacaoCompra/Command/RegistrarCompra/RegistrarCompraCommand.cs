using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;
namespace SistemaCompra.Application.Produto.Command.RegistrarProduto
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string Situacao { get; set; }
        public string UsuarioSolicitante { get;  set; }
        public string NomeFornecedor { get;  set; }
        public int CondicaoPagamento { get;  set; }
        public IList<Item> Itens { get; set; }
    }
}