using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        #region Model

        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Situacao Situacao { get; private set; }

        public string UsuarioSolicitante { get; private set; }
        public string NomeFornecedor { get; private set; }
        public int? CondicaoPagamento { get; private set; }
        public decimal TotalGeral { get; private set; }

        #endregion

        #region PropertiesNotMapped

        [NotMapped]
        public UsuarioSolicitante usuarioSolicitante { get; private set; }
        [NotMapped]
        public NomeFornecedor nomeFornecedor { get; private set; }
        [NotMapped]
        public CondicaoPagamento condicaoPagamento { get; private set; }
        [NotMapped]
        public Money totalGeral { get; private set; }

        #endregion


        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string _usuarioSolicitante, string _nomeFornecedor, int _condicaoPagamento, IList<Item> _itens)
        {
            Id = Guid.NewGuid();
            usuarioSolicitante = new UsuarioSolicitante(_usuarioSolicitante);
            nomeFornecedor = new NomeFornecedor(_nomeFornecedor);
            condicaoPagamento = new CondicaoPagamento(_condicaoPagamento);
            UsuarioSolicitante = _usuarioSolicitante;
            NomeFornecedor = _nomeFornecedor;
            CondicaoPagamento = _condicaoPagamento;
            Itens = _itens;
            TotalGeral = _itens.Sum(s => s.Subtotal.Value);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void ValidaSolicitacaoCompra()
        {
            NotificarErroQuandoNaoInformarItensCompra();
            DefinirPrazo30DiasAoComprarMais50mil();
        }

        public void NotificarErroQuandoNaoInformarItensCompra()
        {
            if (TotalGeral.Equals(0))
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
        }

        public void DefinirPrazo30DiasAoComprarMais50mil()
        {
            var _condicaoPagamentoMaior50mil = 30;
            if (TotalGeral > 50000)
            {
                condicaoPagamento = new CondicaoPagamento(_condicaoPagamentoMaior50mil);
                CondicaoPagamento = _condicaoPagamentoMaior50mil;
            }
        }

    }
}