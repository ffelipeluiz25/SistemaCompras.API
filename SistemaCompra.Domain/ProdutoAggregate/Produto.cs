using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate.Events;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace SistemaCompra.Domain.ProdutoAggregate
{
    public class Produto : Entity
    {
        #region Model

        public Categoria Categoria { get; private set; }
        public string Descricao { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; set; }
        public Situacao Situacao { get; private set; }

        #endregion

        #region PropertiesNotMapped

        [NotMapped]
        public Money PrecoFormatado { get; private set; }

        #endregion

        private Produto() { }

        public Produto(string nome, string descricao, string categoria, decimal preco)
        {
            Id = Guid.NewGuid();
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
            PrecoFormatado = new Money(preco);
            Categoria = (Categoria)Enum.Parse(typeof(Categoria), categoria);
            Situacao = Situacao.Ativo;
        }

        public void Ativar()
        {
            Situacao = Situacao.Ativo;
        }

        public void Suspender()
        {
            Situacao = Situacao.Suspenso;
        }

        public void AtualizarPreco(decimal preco)
        {
            if (Situacao != Situacao.Ativo) throw new BusinessRuleException("Produto deve estar ativo!");

            PrecoFormatado = new Money(preco);

            AddEvent(new PrecoAtualizadoEvent(Id, PrecoFormatado.Value));
        }
    }
}
