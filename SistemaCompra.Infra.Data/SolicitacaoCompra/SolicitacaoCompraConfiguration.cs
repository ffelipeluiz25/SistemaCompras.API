using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
namespace SistemaCompra.Infra.Data.Produto
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");
            builder.Property(x => x.CondicaoPagamento).HasColumnType("int").HasColumnName("CondicaoPagamento");
            builder.Property(x => x.TotalGeral).HasColumnType("decimal(18,2)").HasColumnName("TotalGeral").IsRequired();
        }
    }
}