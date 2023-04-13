using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        #region Model

        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Situacao Situacao { get; private set; }
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }
        public Money TotalGeral { get; private set; }

        #endregion

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string _usuarioSolicitante, string _nomeFornecedor, int _condicaoPagamento)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(_usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(_nomeFornecedor);
            CondicaoPagamento = new CondicaoPagamento(_condicaoPagamento);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void CalculaTotalGeral()
        {
            TotalGeral = new Money(Itens.Sum(s => s.Subtotal.Value));
        }

        public void ValidaSolicitacaoCompra()
        {
            NotificarErroQuandoNaoInformarItensCompra();
            DefinirPrazo30DiasAoComprarMais50mil();
        }

        public void NotificarErroQuandoNaoInformarItensCompra()
        {
            if (TotalGeral.Value.Equals(0))
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
        }

        public void DefinirPrazo30DiasAoComprarMais50mil()
        {
            var _condicaoPagamentoMaior50mil = 30;
            if (TotalGeral.Value > 50000)
            {
                CondicaoPagamento = new CondicaoPagamento(_condicaoPagamentoMaior50mil);
            }
        }

    }
}